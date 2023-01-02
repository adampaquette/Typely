using CsCheck;

namespace Typely.Tests;

public class ComparableTests
{
    [Fact] public void ValueType_WithShouldEquals() => Gen.Int.Select(x => (ValueType.From(x), x)).Sample((x, y) => x.Equals(y));
    [Fact] public void ShouldNotEquals() => Gen.Int.Select(x => (ValueType.From(x), x + 1)).Sample((x, y) => !x.Equals(y));



}
