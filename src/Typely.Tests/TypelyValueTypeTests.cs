using CsCheck;

namespace Typely.Tests;

public class TypelyValueTypeTests
{
    [Fact] public void Equals_ShouldBe_True() => Gen.Int.Select(x => (ValueType.From(x), ValueType.From(x))).Sample((x, y) => x.Equals(y));
    [Fact] public void Equals_ShouldBe_False() => Gen.Int.Select(x => (ValueType.From(x), ValueType.From(x + 1))).Sample((x, y) => !x.Equals(y));
    [Fact] public void OperatorEqual_ShouldBe_True() => Gen.Int.Select(x => (ValueType.From(x), ValueType.From(x))).Sample((x, y) => x == y);
    [Fact] public void OperatorEqual_ShouldBe_False() => Gen.Int.Select(x => (ValueType.From(x), ValueType.From(x + 1))).Sample((x, y) => !(x == y));
    [Fact] public void OperatorNotEqual_ShouldBe_True() => Gen.Int.Select(x => (ValueType.From(x), ValueType.From(x))).Sample((x, y) => !(x != y));
    [Fact] public void OperatorNotEqual_ShouldBe_False() => Gen.Int.Select(x => (ValueType.From(x), ValueType.From(x + 1))).Sample((x, y) => x != y);
}
