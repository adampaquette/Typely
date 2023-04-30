using Microsoft.AspNetCore.Http;
using System.Net;
using Typely.Core;

namespace Typely.AspNetCore.Http;

public class HttpValidationTemplatedProblemDetails : HttpValidationProblemDetails
{
    /// <summary>
    /// Gets the templated validation errors associated with this instance of <see cref="HttpValidationTemplatedProblemDetails"/>.
    /// </summary>
    public IDictionary<string, TemplatedProblemDetail[]> ErrorTemplates { get; } =
        new Dictionary<string, TemplatedProblemDetail[]>(StringComparer.Ordinal);

    public HttpValidationTemplatedProblemDetails()
    {
        Title = "One or more validation errors occurred.";
    }

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

        templatedProblemDetails.ErrorTemplates.Add(error.TypeName,
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