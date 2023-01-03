using Typely.Core.Builders;
using Typely.Generators.Typely.Parsing.String;

namespace Typely.Generators.Tests.Typely.Parsing;

internal static class RuleBuilderExtensions
{
    public static SettingsTask VerifyRules(this IRuleBuilderOfString ruleBuilder)
    {
        var ruleBuilderImp = ruleBuilder as RuleBuilderOfString;
        var validations = ruleBuilderImp!.GetEmittableTypes().SelectMany(x => x.Rules);

        return Verify(validations).UseDirectory("Snapshots");
    }
}
