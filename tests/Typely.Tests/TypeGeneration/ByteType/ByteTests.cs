using CsCheck;
using Typely.Core;

namespace Typely.Tests.TypeGeneration.ByteType;

public class ByteTests
{
    [Fact] public void Equals_ShouldBe_True() => GenTrueEquals.Sample((x, y) => x.Equals(y));
    //[Fact] public void Equals_ShouldBe_False() => GenFalseEquals.Sample((x, y) => !x.Equals(y));
    [Fact] public void OperatorEqual_ShouldBe_True() => GenTrueEquals.Sample((x, y) => x == y);
    //[Fact] public void OperatorEqual_ShouldBe_False() => GenFalseEquals.Sample((x, y) => !(x == y));
    [Fact] public void OperatorNotEqual_ShouldBe_True() => GenTrueEquals.Sample((x, y) => !(x != y));
    //[Fact] public void OperatorNotEqual_ShouldBe_False() => GenFalseEquals.Sample((x, y) => x != y);
    [Fact] public void CompareTo() => GenComparable.Sample((x) => x.primitive.CompareTo(x.randomObj.Value) == x.valueObject.CompareTo(x.randomObj));
    [Fact] public void CompareToObject() => GenComparable.Sample((x) => x.primitive.CompareTo((object)x.randomObj.Value) == x.valueObject.CompareTo((object)x.randomObj));
    [Fact] public void NotEmpty() => Assert.Throws<ValidationException>(() => NotEmptyType.From(default));
    [Fact] public void NotEqual() => Asserts.ValidationMatchPredicate<NotEqualType, byte>(Gen.Byte, (s) => s.Equals(10), 10);
    [Fact] public void GreaterThan() => Asserts.ValidationMatchPredicate<GreaterThanType, byte>(Gen.Byte, (s) => s <= 10, 9);
    [Fact] public void GreaterThanOrEqual() => Asserts.ValidationMatchPredicate<GreaterThanOrEqualType, byte>(Gen.Byte, (s) => s < 10, 9);
    [Fact] public void LessThan() => Asserts.ValidationMatchPredicate<LessThanType, byte>(Gen.Byte, (s) => s >= 10, 10);
    [Fact] public void LessThanOrEqual() => Asserts.ValidationMatchPredicate<LessThanOrEqualType, byte>(Gen.Byte, (s) => s > 10, 11);
    [Fact] public void Must() => Asserts.ValidationMatchPredicate<MustType, byte>(Gen.Byte, (s) => s.Equals(10), 10);

    private Gen<(BasicType, BasicType)> GenTrueEquals => Gen.Byte.Select(x => (BasicType.From(x), BasicType.From(x)));
    //private Gen<(BasicType, BasicType)> GenFalseEquals => Gen.Byte.Select(x => (BasicType.From(x), BasicType.From(x + 1))); //TODO
    private Gen<(byte primitive, BasicType valueObject, BasicType randomObj)> GenComparable => Gen.Select(Gen.Byte, Gen.Byte, (x, y) => (x, BasicType.From(x), BasicType.From(y)));
}