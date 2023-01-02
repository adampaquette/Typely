using Typely.Core;
using Typely.Generators.Typely.Parsing;

namespace Typely.Generators.Tests.Typely.Parsing;
internal static class RuleBuilderExtensions
{
    public static SettingsTask VerifyRules<T>(this IRuleBuilder<T> builder)
    {
        var ruleBuilder = builder as RuleBuilder<T>;
        var validations = ruleBuilder!.GetEmittableTypes().SelectMany(x => x.Validations);

        return Verify(validations).UseDirectory("Snapshots");
    }
}
