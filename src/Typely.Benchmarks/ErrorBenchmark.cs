using BenchmarkDotNet.Attributes;

namespace Typely.Benchmarks;

[MemoryDiagnoser]
public class ErrorBenchmark
{
    readonly string val = new Random().Next().ToString();

    [Benchmark]
    public void ReturnException()
    {
        try
        {
            throw new ArgumentOutOfRangeException("Param", val, "Parameter must not be empty.");
        }
        catch (Exception)
        {
        }
    }

    [Benchmark]
    public void ReturnValidationError() => new ValidationError("ERR001", "Parameter must not be empty.", val, "Param");
}

public class ValidationError
{
    public string ErrorCode { get; init; }
    public string ErrorMessage { get; init; }
    public object AttemptedValue { get; init; }
    public object Source { get; init; }
    public ValidationError(string errorCode, string errorMessage, object attemptedValue, object source)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        AttemptedValue = attemptedValue;
        Source = source;
    }
}
