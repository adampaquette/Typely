namespace Typely.Generators.Typely;

/// <summary>
/// Unique error codes.
/// </summary>
internal static class ErrorCodes
{
    public const string NotEmpty = nameof(NotEmpty);
    public const string NotEqual = nameof(NotEqual);
    public const string Length = nameof(Length);
    public const string ExactLength = nameof(ExactLength);
    public const string MinLength = nameof(MinLength);
    public const string MaxLength = nameof(MaxLength);
    public const string LessThan = nameof(LessThan);
    public const string LessThanOrEqualTo = nameof(LessThanOrEqualTo);
    public const string GreaterThan = nameof(GreaterThan);
    public const string GreaterThanOrEqualTo = nameof(GreaterThanOrEqualTo);
    public const string InclusiveBetween = nameof(InclusiveBetween);
    public const string ExclusiveBetween = nameof(ExclusiveBetween);
    public const string Must = nameof(Must);
    public const string Matches = nameof(Matches);
}