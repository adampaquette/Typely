using CsCheck;
using Typely.Core;

namespace Typely.Tests.TypeGeneration.DateTimeType;

public class DateTimeTests
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
    [Fact] public void NotEqual() => Asserts.ValidationMatchPredicate<NotEqualType, DateTime>(Gen.DateTime, (s) => s.Equals(new DateTime(2023,04,26)), new DateTime(2023,04,26));
    [Fact] public void GreaterThan() => Asserts.ValidationMatchPredicate<GreaterThanType, DateTime>(Gen.DateTime, (s) => s <= new DateTime(2023,04,26), new DateTime(2023,04,25));
    [Fact] public void GreaterThanOrEqual() => Asserts.ValidationMatchPredicate<GreaterThanOrEqualType, DateTime>(Gen.DateTime, (s) => s < new DateTime(2023,04,26), new DateTime(2023,04,25));
    [Fact] public void LessThan() => Asserts.ValidationMatchPredicate<LessThanType, DateTime>(Gen.DateTime, (s) => s >= new DateTime(2023,04,26), new DateTime(2023,04,26));
    [Fact] public void LessThanOrEqual() => Asserts.ValidationMatchPredicate<LessThanOrEqualType, DateTime>(Gen.DateTime, (s) => s > new DateTime(2023,04,26), new DateTime(2023,04,27));
    [Fact] public void Must() => Asserts.ValidationMatchPredicate<MustType, DateTime>(Gen.DateTime, (s) => s.Equals(new DateTime(2023,04,26)), new DateTime(2023,04,26));

    private Gen<(BasicType, BasicType)> GenTrueEquals => Gen.DateTime.Select(x => (BasicType.From(x), BasicType.From(x)));
    private Gen<(BasicType, BasicType)> GenFalseEquals => Gen.DateTime.Select(x => (BasicType.From(x), BasicType.From(x.AddDays(1))));
    private Gen<(DateTime primitive, BasicType valueObject, BasicType randomObj)> GenComparable => 
        Gen.Select(Gen.DateTime, Gen.DateTime, (x, y) => (x, BasicType.From(x), BasicType.From(y)));
}