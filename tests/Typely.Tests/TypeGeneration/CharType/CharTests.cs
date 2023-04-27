using CsCheck;
using Typely.Core;

namespace Typely.Tests.TypeGeneration.CharType;

public class charTests
{
    [Fact] public void Equals_ShouldBe_True() => GenTrueEquals.Sample((x, y) => x.Equals(y));
    [Fact] public void Equals_ShouldBe_False() => GenFalseEquals.Sample((x, y) => !x.Equals(y));
    [Fact] public void OperatorEqual_ShouldBe_True() => GenTrueEquals.Sample((x, y) => x == y);
    [Fact] public void OperatorEqual_ShouldBe_False() => GenFalseEquals.Sample((x, y) => !(x == y));
    [Fact] public void OperatorNotEqual_ShouldBe_True() => GenTrueEquals.Sample((x, y) => !(x != y));
    [Fact] public void OperatorNotEqual_ShouldBe_False() => GenFalseEquals.Sample((x, y) => x != y);
    [Fact] public void CompareTo() => GenComparable.Sample((x) => x.primitive.CompareTo(x.randomObj.Value) == x.valueObject.CompareTo(x.randomObj));
    [Fact] public void CompareToObject() => GenComparable.Sample((x) => x.primitive.CompareTo((object)x.randomObj.Value) == x.valueObject.CompareTo((object)x.randomObj));
    [Fact] public void NotEmpty() => Assert.Throws<ValidationException>(() => NotEmptyType.From(default));
    [Fact] public void NotEqual() => Asserts.ValidationMatchPredicate<NotEqualType, char>(Gen.Char, (s) => s.Equals('A'), 'A');
    [Fact] public void GreaterThan() => Asserts.ValidationMatchPredicate<GreaterThanType, char>(Gen.Char, (s) => s <= 'B', 'A');
    [Fact] public void GreaterThanOrEqual() => Asserts.ValidationMatchPredicate<GreaterThanOrEqualType, char>(Gen.Char, (s) => s < 'B', 'A');
    [Fact] public void LessThan() => Asserts.ValidationMatchPredicate<LessThanType, char>(Gen.Char, (s) => s >= 'A', 'A');
    [Fact] public void LessThanOrEqual() => Asserts.ValidationMatchPredicate<LessThanOrEqualType, char>(Gen.Char, (s) => s > 'A', 'B');
    [Fact] public void Must() => Asserts.ValidationMatchPredicate<MustType, char>(Gen.Char, (s) => s.Equals('A'), 'A');

    private Gen<(BasicType, BasicType)> GenTrueEquals => Gen.Char.Select(x => (BasicType.From(x), BasicType.From(x)));
    private Gen<(BasicType, BasicType)> GenFalseEquals => Gen.Char.Select(x => (BasicType.From(x), BasicType.From((char)(x + 1))));
    private Gen<(char primitive, BasicType valueObject, BasicType randomObj)> GenComparable => 
        Gen.Select(Gen.Char, Gen.Char, (x, y) => (x, BasicType.From(x), BasicType.From(y)));
}