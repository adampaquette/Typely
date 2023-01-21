﻿//using CsCheck;
//using Typely.Core;

//namespace Typely.Tests;

///// <summary>
///// Common asserts.
///// </summary>
//internal static class Asserts
//{
//    /// <summary>
//    /// Test many possibilities that should not return an error and one case that does.
//    /// </summary>
//    /// <typeparam name="TTypelyValue">Type generated by Typely.</typeparam>
//    /// <typeparam name="TUnderlyingValue">Underlying type of <see cref="TTypelyValue"/>.</typeparam>
//    /// <param name="gen">Generator of the underlying value.</param>
//    /// <param name="predicate">Predicate that should create a validation error.</param>
//    /// <param name="valueThatReturnError">Value that always causes a validation error.</param>
//    public static void ValidationMatchPredicate<TTypelyValue, TUnderlyingValue>(Gen<TUnderlyingValue> gen, Func<TUnderlyingValue, bool> predicate, TUnderlyingValue valueThatReturnError)
//        where TTypelyValue : ITypelyValue<TUnderlyingValue, TTypelyValue>
//    {
//        gen.Sample(x => predicate(x) == (TTypelyValue.Validate(x) != null));
//        Assert.NotNull(TTypelyValue.Validate(valueThatReturnError));
//    }
//}
