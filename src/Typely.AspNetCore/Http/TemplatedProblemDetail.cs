namespace Typely.AspNetCore.Http;

/// <summary>
/// A single problem detail with placeholders.
/// </summary>
public record TemplatedProblemDetail
{
    /// <summary>
    /// A unique identifier for the error.
    /// </summary>
    /// <remarks>The value is used for translations.</remarks>
    public required string Code { get; init; }

    /// <summary>
    /// The error message localized with placeholders.
    /// </summary>
    public required string Message { get; init; }

    /// <summary>
    /// The value that caused the error.
    /// </summary>
    public string? AttemptedValue { get; init; }

    /// <summary>
    /// Type's name that generated the error.
    /// </summary>
    public required string TypeName { get; init; }

    /// <summary>
    /// List of placeholders with their values.
    /// </summary>
    public required Dictionary<string, string?> PlaceholderValues { get; init; }
}