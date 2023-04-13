﻿using AgileObjects.ReadableExpressions;
using Microsoft.CodeAnalysis;
using System.Linq.Expressions;
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

    public string Emit(EmittableType t)
    {
        if (t.Name == null)
        {
            //TODO DIAGNOSTIC
            return string.Empty;
        }

        if (t.TypeName == null)
        {
            //TODO DIAGNOSTIC
            return string.Empty;
        }

        var typeName = t.TypeName;
        var namespaces = BuildNamespaces(t.Rules, t.AdditionalNamespaces);
        var underlyingType = GetUnderlyingType(t.UnderlyingType);
        var constructType = GetConstructType(t.ConstructTypeKind);
        var validationBlock = GenerateValidations(t.Rules, t.Name, t.UnderlyingType, typeName);

        return $$"""
            // <auto-generated>This file was generated by Typely.</auto-generated>
            {{namespaces}}

            #nullable enable

            namespace {{t.Namespace}}
            {
                [JsonConverter(typeof(TypelyJsonConverter<{{underlyingType}}, {{typeName}}>))]
                public partial {{constructType}} {{typeName}} : ITypelyValue<{{underlyingType}}, {{typeName}}>, IEquatable<{{typeName}}>, IComparable<{{typeName}}>, IComparable
                {
                    public {{underlyingType}} Value { get; private set; }

                    public {{typeName}}() => throw new Exception("Parameterless constructor is not allowed.");

                    public {{typeName}}({{underlyingType}} value)
                    {
                        TypelyValue.ValidateAndThrow<{{underlyingType}}, {{typeName}}>(value);
                        Value = value;
                    }

                    public static ValidationError? Validate({{underlyingType}} value){{validationBlock}}

                    public static {{typeName}} From({{underlyingType}} value) => new(value);

                    public static bool TryFrom({{underlyingType}} value, [MaybeNullWhen(false)] out {{typeName}} typelyType, out ValidationError? validationError)
                    {
                        validationError = Validate(value);
                        var isValid = validationError == null;
                        typelyType = default;
                        if (isValid)
                        {
                            typelyType.Value = value;
                        }
                        return isValid;
                    }

                    public override string ToString() => Value.ToString();

                    public static bool operator !=({{typeName}} left, {{typeName}} right) => !(left == right);

                    public static bool operator ==({{typeName}} left, {{typeName}} right) => left.Equals(right);

                    public override int GetHashCode() => Value.GetHashCode();

                    public bool Equals({{typeName}} other) => Value.Equals(other.Value);

                    public override bool Equals([NotNullWhen(true)] object? obj) => obj is {{typeName}} && Equals(({{typeName}})obj);

                    public int CompareTo({{typeName}} other) => Value.CompareTo(other.Value);

                    public int CompareTo(object? obj) => obj is not {{typeName}} ? 1 : CompareTo(({{typeName}})obj!);

                    public static explicit operator {{underlyingType}}({{typeName}} value) => value.Value;
                }
            }
            """;
    }

    private string GetUnderlyingType(Type type)
    {
        if (type == typeof(bool)) return "bool";
        if (type == typeof(byte)) return "byte";
        if (type == typeof(char)) return "char";
        if (type == typeof(decimal)) return "decimal";
        if (type == typeof(double)) return "double";
        if (type == typeof(float)) return "float";
        if (type == typeof(int)) return "int";
        if (type == typeof(long)) return "long";
        if (type == typeof(sbyte)) return "sbyte";
        if (type == typeof(short)) return "short";
        if (type == typeof(string)) return "string";
        if (type == typeof(uint)) return "uint";
        if (type == typeof(ulong)) return "ulong";
        if (type == typeof(ushort)) return "ushort";
        return type.Name;
    }

    private string BuildNamespaces(IList<EmittableRule> rules, IEnumerable<string> additionalNamespaces)
    {
        var namespaces = new List<string>
        {
            "System",
            "Typely.Core",
            "Typely.Core.Converters",
            "System.Diagnostics.CodeAnalysis",
            "System.Text.Json.Serialization"
        };

        namespaces.AddRange(additionalNamespaces);

        if (rules.Any())
        {
            namespaces.Add("System.Collections.Generic");
        }

        if (rules.Any((x) => x.ErrorCode == ErrorCodes.Matches))
        {
            namespaces.Add("System.Text.RegularExpressions");
        }

        return string.Join(Environment.NewLine, namespaces.Distinct().OrderBy(x => x).Select(x => $"using {x};"));
    }

    private string GetConstructType(ConstructTypeKind objectType) => objectType switch
    {
        ConstructTypeKind.Struct => "struct",
        _ => "class"
    };

    private string GenerateValidations(List<EmittableRule> emittableValidations, string name, Type underlyingType,
        string typeName)
    {
        if (!emittableValidations.Any() && underlyingType.IsValueType)
        {
            return " => null;";
        }

        var builder = new StringBuilder(Environment.NewLine);
        builder.AppendLine("        {");

        if (!underlyingType.IsValueType)
        {
            builder.AppendLine($"            if (value == null) throw new ArgumentNullException(nameof({typeName}));")
                .AppendLine();
        }

        foreach (var emittableValidation in emittableValidations)
        {
            _cancellationToken.ThrowIfCancellationRequested();
            var errorCode = emittableValidation.ErrorCode;
            var placeholders = GenerateValidationPlaceholders(emittableValidation.PlaceholderValues);

            builder.AppendLine($$"""
                            if ({{emittableValidation.Rule}})
                            {
                                return ValidationErrorFactory.Create(value, "{{errorCode}}", {{emittableValidation.Message}}, {{name}}{{placeholders}}
                            }
                """)
                .AppendLine();
        }

        builder
            .AppendLine("            return null;")
            .Append("        }");

        return builder.ToString();
    }

    private static string GenerateValidationPlaceholders(Dictionary<string, object?> placeholders)
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
            var value = Expression.Constant(placeholder.Value).ToReadableString();
            builder.AppendLine($$"""                        { "{{placeholder.Key}}", {{value}} },""");
        }

        return builder.Append("                    });")
            .ToString();
    }
}