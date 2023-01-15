﻿using Typely.Core;

namespace Typely.Tests;

[UsesVerify]
public class ValidationErrorTests
{
    [Fact]
    public Task Validate_ShouldReturn_ValidationError() => Verify(ValidationErrorTestsType.Validate(""));

    [Fact]
    public void Exception_ShouldBe_ValidationException()
    {
        try
        {
            ValidationErrorTestsType.From("");
        }
        catch (ValidationException ex)
        {
            Assert.NotNull(ex.ValidationError);
        }
    }
}
