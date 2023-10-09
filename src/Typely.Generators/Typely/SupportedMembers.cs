namespace Typely.Generators.Typely;

public class SupportedMembers
{
    public static IEnumerable<string> All = new[]
    {
        TypelyBuilder.OfBool, TypelyBuilder.OfByte, TypelyBuilder.OfChar, TypelyBuilder.OfDateTime,
        TypelyBuilder.OfDateTimeOffset, TypelyBuilder.OfDecimal, TypelyBuilder.OfDouble, TypelyBuilder.OfFloat,
        TypelyBuilder.OfGuid, TypelyBuilder.OfInt, TypelyBuilder.OfLong, TypelyBuilder.OfSByte,
        TypelyBuilder.OfShort, TypelyBuilder.OfString, TypelyBuilder.OfTimeSpan, TypelyBuilder.OfUInt,
        TypelyBuilder.OfULong, TypelyBuilder.OfUShort, TypelyBuilder.OfDateOnly, TypelyBuilder.OfTimeOnly,
        TypelyBuilderOf.ForMethodName, TypelyBuilderOf.AsClassMethodName, TypelyBuilderOf.AsStructMethodName,
        TypelyBuilderOf.WithNameMethodName, TypelyBuilderOf.WithNamespaceMethodName,
        TypelyBuilderOf.NormalizeMethodName, TypelyBuilderOf.AsFactoryMethodName,
        TypelyBuilderOf.NotEmptyMethodName, TypelyBuilderOf.NotEqualMethodName, TypelyBuilderOf.MustMethodName,
        TypelyBuilderOf.MinLengthMethodName, TypelyBuilderOf.MaxLengthMethodName, TypelyBuilderOf.MatchesMethodName,
        TypelyBuilderOf.LengthMethodName, TypelyBuilderOf.GreaterThanMethodName,
        TypelyBuilderOf.GreaterThanOrEqualToMethodName, TypelyBuilderOf.LessThanMethodName,
        TypelyBuilderOf.LessThanOrEqualToMethodName, TypelyBuilderOf.WithMessageMethodName,
        TypelyBuilderOf.WithErrorCodeMethodName,
    };
}