// using Microsoft.CodeAnalysis;
//
// namespace Typely.Generators.Typely.Parsing.TypeBuilders;
//
// internal class EmittableTypeBuilderOfString : EmittableTypeBuilderBase, IEmittableTypeBuilder
// {
//     public EmittableTypeBuilderOfString(string defaultNamespace, IEnumerable<ParsedInvocation> invocations,
//         SemanticModel model)
//         : base(invocations, new EmittableType(typeof(string), defaultNamespace), model)
//     {
//     }
//
//     public EmittableType Build()
//     {
//         foreach (var invocation in Invocations)
//         {
//             switch (invocation.MemberName)
//             {
//                 // case nameof(ITypelyBuilder<string>.For):
//                 //     var typeName = invocation.ArgumentListSyntax.Arguments.First().ToString();
//                 //     typeName = typeName.Substring(1, typeName.Length - 2);
//                 //     _builder.For(typeName);
//                 //     break;
//                 // case nameof(ITypelyBuilderOfString.Length):
//                 //     var length = int.Parse(invocation.ArgumentListSyntax.Arguments.First().ToString());
//                 //     _builder.Length(length);
//                 //     break;
//                 default: throw new NotSupportedException(invocation.MemberName);
//             }
//         }
//
//         return EmittableType;
//     }
// }