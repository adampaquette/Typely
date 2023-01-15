using CsCheck;

namespace Typely.Tests.String;

public class TypelyReferenceTypeTests
{
    [Fact] public void Equals_ShouldBe_True() => GenTrueEquals.Sample((x, y) => x.Equals(y));
    [Fact] public void Equals_ShouldBe_False() => GenFalseEquals.Sample((x, y) => !x.Equals(y));
    [Fact] public void OperatorEqual_ShouldBe_True() => GenTrueEquals.Sample((x, y) => x == y);
    [Fact] public void OperatorEqual_ShouldBe_False() => GenFalseEquals.Sample((x, y) => !(x == y));
    [Fact] public void OperatorNotEqual_ShouldBe_True() => GenTrueEquals.Sample((x, y) => !(x != y));
    [Fact] public void OperatorNotEqual_ShouldBe_False() => GenFalseEquals.Sample((x, y) => x != y);
    [Fact] public void CompareTo() => GenComparable.Sample((x) => x.primitive.CompareTo(x.randomObj.Value) == x.valueObject.CompareTo(x.randomObj));
    [Fact] public void CompareToObject() => GenComparable.Sample((x) => x.primitive.CompareTo((object)x.randomObj.Value) == x.valueObject.CompareTo((object)x.randomObj));

    private Gen<(StringType, StringType)> GenTrueEquals => Gen.String.Select(x => (StringType.From(x), StringType.From(x)));
    private Gen<(StringType, StringType)> GenFalseEquals => Gen.String.Select(x => (StringType.From(x), StringType.From(x + "1")));
    private Gen<(string primitive, StringType valueObject, StringType randomObj)> GenComparable => Gen.Select(Gen.String, Gen.String, (x, y) => (x, StringType.From(x), StringType.From(y)));
}