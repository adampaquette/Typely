﻿namespace Typely.Tests;

public class Tests
{
    [Fact(Skip = "debug")]
    public void BadLanguageConstructs()
    {
        var a = new ReferenceSample();
        var c = default(ReferenceSample);
    }

    [Fact]
    public void TestReferenceSample()
    {
    }
}