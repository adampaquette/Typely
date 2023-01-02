using CsCheck;

namespace Typely.Tests;

public class TypelyValueTypeTests
{
    [Fact] public void Equals_ShouldBe_True() => GenTrueEquals.Sample((x, y) => x.Equals(y));
    [Fact] public void Equals_ShouldBe_False() => GenFalseEquals.Sample((x, y) => !x.Equals(y));
    [Fact] public void OperatorEqual_ShouldBe_True() => GenTrueEquals.Sample((x, y) => x == y);
    [Fact] public void OperatorEqual_ShouldBe_False() => GenFalseEquals.Sample((x, y) => !(x == y));
    [Fact] public void OperatorNotEqual_ShouldBe_True() => GenTrueEquals.Sample((x, y) => !(x != y));
    [Fact] public void OperatorNotEqual_ShouldBe_False() => GenFalseEquals.Sample((x, y) => x != y);

    [Fact] public void CompareTo() => GenComparable.Sample((x) => x.primitive.CompareTo(x.randomObj.Value) == x.valueObject.CompareTo(x.randomObj));
    [Fact] public void CompareToObject() => GenComparable.Sample((x) => x.primitive.CompareTo((object)x.randomObj.Value) == x.valueObject.CompareTo((object)x.randomObj));

    private Gen<(ValueType, ValueType)> GenTrueEquals => Gen.Int.Select(x => (ValueType.From(x), ValueType.From(x)));
    private Gen<(ValueType, ValueType)> GenFalseEquals => Gen.Int.Select(x => (ValueType.From(x), ValueType.From(x + 1)));
    private Gen<(int primitive, ValueType valueObject, ValueType randomObj)> GenComparable => Gen.Select(Gen.Int, Gen.Int, (x, y) => (x, ValueType.From(x), ValueType.From(y)));
}
