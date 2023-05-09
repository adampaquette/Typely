using System.Text;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Typely.Emitting;

/// <summary>
/// Generate the C# code for a value object.
/// </summary>
internal static partial class Emitter
{
    public const string ValueParameterName = "value";

    /// <summary>
    /// Emit the source code for a value object.
    /// </summary>
    /// <param name="emittableType">The type to generate.</param>
    /// <param name="cancellationToken">A token to notify the operation should be cancelled.</param>
    /// <returns></returns>
    public static string Emit(EmittableType emittableType) =>
        emittableType.ConstructTypeKind == ConstructTypeKind.Struct
            ? EmitStruct(emittableType)
            : EmitClass(emittableType);

    private static string GenerateProperties(EmittableType emittableType) =>
        emittableType.Properties.ContainsMaxLength()
            ? $"\n        public static int MaxLength => {emittableType.Properties.GetMaxLength()};\n"
            : String.Empty;

    private static string GenerateMaxLengthInterface(EmittableType emittableType) =>
        emittableType.Properties.ContainsMaxLength()
            ? ", IMaxLength"
            : string.Empty;

    /// <summary>
    /// Support for Minimal API model binding.
    /// </summary>
    private static string GenerateTryParseIfSupported(EmittableType emittableType)
    {
        var defaultValue = emittableType.ConstructTypeKind == ConstructTypeKind.Struct ? "default" : "null";
        var interrogationPoint = emittableType.ConstructTypeKind == ConstructTypeKind.Struct ? string.Empty : "?";
        var underlyingType = emittableType.UnderlyingType;
        var typeName = emittableType.TypeName;
        
        return underlyingType == "string"
            ? string.Empty
            : $$"""

                public static bool TryParse(string? value, IFormatProvider? provider, out {{typeName}}{{interrogationPoint}} valueObject)
                {
                    if({{underlyingType}}.TryParse(value, out var underlyingValue))
                    {
                        valueObject = From(underlyingValue);
                        return true;
                    }
                        
                    valueObject = {{defaultValue}};
                    return false;
                }

        """;
    }

    private static string BuildNamespaces(EmittableType emittableType)
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

        namespaces.AddRange(emittableType.AdditionalNamespaces);

        if (emittableType.Rules.Any())
        {
            namespaces.Add("System.Collections.Generic");
        }

        return string.Join("\n", namespaces.Distinct().OrderBy(x => x).Select(x => $"using {x};"));
    }

    private static string GetConstructType(ConstructTypeKind objectType) => objectType switch
    {
        ConstructTypeKind.Struct => "struct",
        _ => "class"
    };

    private static string GenerateSafeNormalize(EmittableType emittableType)
    {
        var builder = new StringBuilder();

        if (!emittableType.IsValueType)
        {
            builder.AppendLine($"ArgumentNullException.ThrowIfNull(value, nameof({emittableType.TypeName}));")
                .Append("            ");
        }

        if (emittableType.NormalizeFunctionBody != null)
        {
            builder.AppendLine($"value = {emittableType.NormalizeFunctionBody};")
                .Append("            ");
        }

        return builder.ToString();
    }

    private static string GenerateValidations(EmittableType emittableType)
    {
        if (!emittableType.Rules.Any() && emittableType.IsValueType)
        {
            return " => null;";
        }

        var builder = new StringBuilder("\n");
        builder.AppendLine("        {");

        foreach (var rule in emittableType.Rules)
        {
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