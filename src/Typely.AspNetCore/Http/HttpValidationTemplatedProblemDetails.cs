using Microsoft.AspNetCore.Http;
using System.Net;
using Typely.Core;

namespace Typely.AspNetCore.Http;

/// <summary>
/// An <see cref="HttpValidationProblemDetails"/> for validation errors with message templates.
/// </summary>
public class HttpValidationTemplatedProblemDetails : HttpValidationProblemDetails
{
    /// <summary>
    /// Gets the templates used to create the error messages.
    /// </summary>
    public IDictionary<string, TemplatedProblemDetail[]> Templates { get; } =
        new Dictionary<string, TemplatedProblemDetail[]>(StringComparer.Ordinal);

    public HttpValidationTemplatedProblemDetails()
    {
        Title = "One or more validation errors occurred.";
    }

    /// <summary>
    /// Creates a new instance of <see cref="HttpValidationTemplatedProblemDetails"/> from the specified <paramref name="exception"/>.
    /// </summary>
    /// <param name="exception">A <see cref="ValidationException"/>.</param>
    /// <returns>An ASP.NET compatible problem details.</returns>
    public static HttpValidationTemplatedProblemDetails From(ValidationException exception)
    {
        var error = exception.ValidationError;
        var templatedProblemDetails = new HttpValidationTemplatedProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1", Status = (int)HttpStatusCode.BadRequest,
        };

        templatedProblemDetails.Errors.Add(error.TypeName, new[] { error.ErrorMessage });

        var placeholderValues = error.PlaceholderValues
            .Select(x => (x.Key, x.Value?.ToString() ?? "null"))
            .ToDictionary(x => x.Key, y => y.Item2.ToString());

        templatedProblemDetails.Templates.Add(error.TypeName,
            new[]
            {
                new TemplatedProblemDetail
                {
                    Code = error.ErrorCode,
                    Message = error.ErrorMessageWithPlaceholders,
                    AttemptedValue = error.AttemptedValue?.ToString(),
                    TypeName = error.TypeName,
                    PlaceholderValues = placeholderValues!
                }
            });

        return templatedProblemDetails;
    }
}