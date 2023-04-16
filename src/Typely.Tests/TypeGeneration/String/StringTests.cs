using CsCheck;
using System.Text.RegularExpressions;
using Typely.Core;

namespace Typely.Tests.TypeGeneration.String;

public class StringTests
{
    [Fact] public void Equals_ShouldBe_True() => GenTrueEquals.Sample((x, y) => x.Equals(y));
    [Fact] public void Equals_ShouldBe_False() => GenFalseEquals.Sample((x, y) => !x.Equals(y));
    [Fact] public void OperatorEqual_ShouldBe_True() => GenTrueEquals.Sample((x, y) => x == y);
    [Fact] public void OperatorEqual_ShouldBe_False() => GenFalseEquals.Sample((x, y) => !(x == y));
    [Fact] public void OperatorNotEqual_ShouldBe_True() => GenTrueEquals.Sample((x, y) => !(x != y));
    [Fact] public void OperatorNotEqual_ShouldBe_False() => GenFalseEquals.Sample((x, y) => x != y);
    [Fact] public void CompareTo() => GenComparable.Sample((x) => x.primitive.CompareTo(x.randomObj.Value) == x.valueObject.CompareTo(x.randomObj));
    [Fact] public void CompareToObject() => GenComparable.Sample((x) => x.primitive.CompareTo((object)x.randomObj.Value) == x.valueObject.CompareTo((object)x.randomObj));
    [Fact] public void NotEmpty() => Assert.Throws<ValidationException>(() => NotEmptyType.From(""));
    [Fact] public void NotEqual() => Asserts.ValidationMatchPredicate<NotEqualType, string>(Gen.String, (s) => s.Equals("value"), "value");
    [Fact] public void GreaterThan() => Asserts.ValidationMatchPredicate<GreaterThanType, string>(Gen.String, (s) => string.Compare(s, "value", StringComparison.Ordinal) <= 0, "value");
    [Fact] public void GreaterThanOrEqual() => Asserts.ValidationMatchPredicate<GreaterThanOrEqualType, string>(Gen.String, (s) => string.Compare(s, "value", StringComparison.Ordinal) < 0, "valu");
    [Fact] public void LessThan() => Asserts.ValidationMatchPredicate<LessThanType, string>(Gen.String, (s) => string.Compare(s, "value", StringComparison.Ordinal) >= 0, "value");
    [Fact] public void LessThanOrEqual() => Asserts.ValidationMatchPredicate<LessThanOrEqualType, string>(Gen.String, (s) => string.Compare(s, "value", StringComparison.Ordinal) > 0, "valuee");
    [Fact] public void Matches() => Asserts.ValidationMatchPredicate<MatchesType, string>(Gen.String, (s) => !Regex.IsMatch(s, "[0-9]{1}"), "value");
    [Fact] public void Must() => Asserts.ValidationMatchPredicate<MustType, string>(Gen.String, (s) => s.Equals("A"), "A");
    [Fact] public void MinLength() => Asserts.ValidationMatchPredicate<MinLengthType, string>(Gen.String, (s) => s.Length < 5, "B");
    [Fact] public void MaxLength() => Asserts.ValidationMatchPredicate<MaxLengthType, string>(Gen.String, (s) => s.Length > 5, "ABCDEF");
    [Fact] public void Length() => Asserts.ValidationMatchPredicate<LengthType, string>(Gen.String, (s) => s.Length < 5 || s.Length > 10, "A");

    private Gen<(BasicType, BasicType)> GenTrueEquals => Gen.String.Select(x => (BasicType.From(x), BasicType.From(x)));
    private Gen<(BasicType, BasicType)> GenFalseEquals => Gen.String.Select(x => (BasicType.From(x), BasicType.From(x + "1")));
    private Gen<(string primitive, BasicType valueObject, BasicType randomObj)> GenComparable => Gen.Select(Gen.String, Gen.String, (x, y) => (x, BasicType.From(x), BasicType.From(y)));
}