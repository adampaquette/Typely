using CsCheck;

namespace Typely.Tests;

public class TypelyReferenceTypeTests
{
    [Fact] public void Equals_ShouldBe_True() => Gen.String.Select(x => (ReferenceType.From(x), ReferenceType.From(x))).Sample((x, y) => x.Equals(y));
    [Fact] public void Equals_ShouldBe_False() => Gen.String.Select(x => (ReferenceType.From(x), ReferenceType.From(x + 1))).Sample((x, y) => !x.Equals(y));
    [Fact] public void OperatorEqual_ShouldBe_True() => Gen.String.Select(x => (ReferenceType.From(x), ReferenceType.From(x))).Sample((x, y) => x == y);
    [Fact] public void OperatorEqual_ShouldBe_False() => Gen.String.Select(x => (ReferenceType.From(x), ReferenceType.From(x + 1))).Sample((x, y) => !(x == y));
    [Fact] public void OperatorNotEqual_ShouldBe_True() => Gen.String.Select(x => (ReferenceType.From(x), ReferenceType.From(x))).Sample((x, y) => !(x != y));
    [Fact] public void OperatorNotEqual_ShouldBe_False() => Gen.String.Select(x => (ReferenceType.From(x), ReferenceType.From(x + 1))).Sample((x, y) => x != y);
}
