using CsCheck;
using Typely.Core;

namespace Typely.Tests.TypeGeneration.Bool;

public class BoolTests
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
    //[Fact] public void NotEqual() => Asserts.ValidationMatchPredicate<NotEqualType, bool>(Gen.Bool, (s) => s.Equals(true), false);
    //[Fact] public void Must() => Asserts.ValidationMatchPredicate<MustType, bool>(Gen.Bool, (s) => s.Equals(true), false);

    private Gen<(BasicType, BasicType)> GenTrueEquals => Gen.Bool.Select(x => (BasicType.From(x), BasicType.From(x)));
    private Gen<(BasicType, BasicType)> GenFalseEquals => Gen.Bool.Select(x => (BasicType.From(x), BasicType.From(!x)));
    private Gen<(bool primitive, BasicType valueObject, BasicType randomObj)> GenComparable => Gen.Select(Gen.Bool, Gen.Bool, (x, y) => (x, BasicType.From(x), BasicType.From(y)));
}