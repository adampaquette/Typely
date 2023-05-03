﻿using Microsoft.CodeAnalysis;
using System.Text;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Typely.Emitting;

/// <summary>
/// Generate the C# code for a value object.
/// </summary>
internal class Emitter
{
    public const string ValueParameterName = "value";

    private readonly Action<Diagnostic> _reportDiagnostic;
    private readonly CancellationToken _cancellationToken;

    public Emitter(Action<Diagnostic> reportDiagnostic, CancellationToken cancellationToken)
    {
        _reportDiagnostic = reportDiagnostic;
        _cancellationToken = cancellationToken;
    }

    public string? Emit(EmittableType t)
    {
        if (t.Name == null)
        {
            _reportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.NameMissing, Location.None, t.Namespace));
            return null;
        }

        if (t.TypeName == null)
        {
            _reportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.TypeNameMissing, Location.None, t.Namespace));
            return null;
        }

        var typeName = t.TypeName;
        var namespaces = BuildNamespaces(t.Rules, t.AdditionalNamespaces);
        var underlyingType = t.UnderlyingType;
        var constructType = GetConstructType(t.ConstructTypeKind);
        var validationBlock = GenerateValidations(t);
        var tryParseIfPresent = GenerateTryParseIfSupported(typeName, underlyingType);
        var safeNormalize = GenerateSafeNormalize(t);
        var maxLengthInterface = GenerateMaxLengthInterface(t);
        var properties = GenerateProperties(t);
        var interrogationPoint = t.IsValueType ? string.Empty : "?";
        var falseCoalescing = t.IsValueType ? string.Empty : " ?? false";
        var oneCoalescing = t.IsValueType ? string.Empty : " ?? 1";

        return $$"""
            // <auto-generated>This file was generated by Typely.</auto-generated>
            {{namespaces}}

            #nullable enable

            namespace {{t.Namespace}}
            {
                [TypeConverter(typeof(TypelyTypeConverter<{{underlyingType}}, {{typeName}}>))]
                [JsonConverter(typeof(TypelyJsonConverter<{{underlyingType}}, {{typeName}}>))]
                public partial {{constructType}} {{typeName}} : ITypelyValue<{{underlyingType}}, {{typeName}}>, IEquatable<{{typeName}}>, IComparable<{{typeName}}>, IComparable{{maxLengthInterface}}
                {{{properties}}
                    public {{underlyingType}} Value { get; private set; }                    

                    public {{typeName}}({{underlyingType}} value)
                    {
                        {{safeNormalize}}TypelyValue.ValidateAndThrow<{{underlyingType}}, {{typeName}}>(value);
                        Value = value;
                    }

                    public static ValidationError? Validate({{underlyingType}} value){{validationBlock}}

                    public static {{typeName}} From({{underlyingType}} value) => new(value);

                    public static bool TryFrom({{underlyingType}} value, out {{typeName}} typelyType, out ValidationError? validationError)
                    {
                        {{safeNormalize}}validationError = Validate(value);
                        var isValid = validationError == null;
                        typelyType = default;
                        if (isValid)
                        {
                            typelyType.Value = value;
                        }
                        return isValid;
                    }
                    {{tryParseIfPresent}}
                    public override string ToString() => Value.ToString();

                    public static bool operator !=({{typeName}} left, {{typeName}} right) => !(left == right);

                    public static bool operator ==({{typeName}} left, {{typeName}} right) => left.Equals(right);

                    public override int GetHashCode() => Value.GetHashCode();

                    public bool Equals({{typeName}} other) => Value{{interrogationPoint}}.Equals(other.Value){{falseCoalescing}};

                    public override bool Equals([NotNullWhen(true)] object? obj) => obj is {{typeName}} && Equals(({{typeName}})obj);

                    public int CompareTo({{typeName}} other) => Value{{interrogationPoint}}.CompareTo(other.Value){{oneCoalescing}};

                    public int CompareTo(object? obj) => obj is not {{typeName}} ? 1 : CompareTo(({{typeName}})obj!);

                    public static explicit operator {{underlyingType}}({{typeName}} value) => value.Value;
                }
            }
            """;
    }

    private string GenerateProperties(EmittableType emittableType) =>
        emittableType.Properties.ContainsMaxLength()
            ? $"\n        public static int MaxLength => {emittableType.Properties.GetMaxLength()};\n"
            : String.Empty;

    private string GenerateMaxLengthInterface(EmittableType emittableType) =>
        emittableType.Properties.ContainsMaxLength()
            ? ", IMaxLength"
            : string.Empty;

    /// <summary>
    /// Support for Minimal API model binding.
    /// </summary>
    private string GenerateTryParseIfSupported(string typeName, string underlyingType) => underlyingType == "string"
        ? string.Empty
        : $$"""

                public static bool TryParse(string? value, IFormatProvider? provider, out {{typeName}} valueObject)
                {
                    if({{underlyingType}}.TryParse(value, out var underlyingValue))
                    {
                        valueObject = From(underlyingValue);
                        return true;
                    }
                        
                    valueObject = default;
                    return false;
                }

        """;

    private string BuildNamespaces(IReadOnlyList<EmittableRule> rules, IReadOnlyList<string> additionalNamespaces)
    {
        var namespaces = new List<string>
        {
            "System",
            "System.ComponentModel",
            "System.Diagnostics.CodeAnalysis",
            "System.Text.Json.Serialization",
            "Typely.Core",
            "Typely.Core.Converters",
        };

        namespaces.AddRange(additionalNamespaces);

        if (rules.Any())
        {
            namespaces.Add("System.Collections.Generic");
        }

        return string.Join("\n", namespaces.Distinct().OrderBy(x => x).Select(x => $"using {x};"));
    }

    private string GetConstructType(ConstructTypeKind objectType) => objectType switch
    {
        ConstructTypeKind.Struct => "struct",
        _ => "class"
    };

    private string GenerateSafeNormalize(EmittableType emittableType)
    {
        var builder = new StringBuilder();

        if (!emittableType.IsValueType)
        {
            builder.AppendLine($"if (value == null) throw new ArgumentNullException(nameof({emittableType.TypeName}));")
                .Append("            ");
        }

        if (emittableType.NormalizeFunctionBody != null)
        {
            builder.AppendLine($"value = {emittableType.NormalizeFunctionBody};")
                .Append("            ");
        }

        return builder.ToString();
    }

    private string GenerateValidations(EmittableType emittableType)
    {
        if (!emittableType.Rules.Any() && emittableType.IsValueType)
        {
            return " => null;";
        }

        var builder = new StringBuilder("\n");
        builder.AppendLine("        {");

        foreach (var rule in emittableType.Rules)
        {
            _cancellationToken.ThrowIfCancellationRequested();
            var errorCode = rule.ErrorCode;
            var placeholders = GenerateValidationPlaceholders(rule.PlaceholderValues);

            builder.AppendLine($$"""
                            if ({{rule.Rule}})
                            {
                                return ValidationErrorFactory.Create(value, "{{errorCode}}", {{rule.Message}}, {{emittableType.Name}}{{placeholders}}
                            }
                """)
                .AppendLine();
        }

        builder
            .AppendLine("            return null;")
            .Append("        }");

        return builder.ToString();
    }

    private static string GenerateValidationPlaceholders(IReadOnlyDictionary<string, object?> placeholders)
    {
        if (!placeholders.Any())
        {
            return ");";
        }

        var builder = new StringBuilder("""
            ,
                                new Dictionary<string, object?>
                                {
            """)
            .AppendLine();

        foreach (var placeholder in placeholders)
        {
            builder.AppendLine($$"""                        { "{{placeholder.Key}}", {{placeholder.Value}} },""");
        }

        return builder.Append("                    });")
            .ToString();
    }
}