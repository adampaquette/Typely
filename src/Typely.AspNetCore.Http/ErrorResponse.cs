using Typely.Core;

namespace Typely.AspNetCore.Http;

public record ErrorResponse
{
    public required int StatusCode { get; init; }
    public required ValidationError ValidationError { get; init; }
    public string? StackTrace { get; init; }
}