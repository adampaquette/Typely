using BenchmarkDotNet.Attributes;
using System.Runtime.CompilerServices;

namespace Typely.Benchmarks;

[MemoryDiagnoser]
public class ReadonlyStructBenchmark
{
    private struct Point
    {
        public int X { get; private set ; }
        public int Y { get; private set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    private readonly struct ReadOnlyPoint
    {
        public int X { get; }
        public int Y { get; }

        public ReadOnlyPoint(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
    
    [Params(10, 100)]
    public int X;

    [Params(10, 100)]
    public int Y;
    
    [Benchmark]
    public int Struct()
    {
        var point = new Point(X, Y);
        return Sum(point);
    }

    [Benchmark]
    public int ReadOnlyStruct()
    {
        var point = new ReadOnlyPoint(X, Y);
        return Sum(point);
    }
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    private int Sum(Point point)
    {
        return point.X + point.Y;
    }
    
    [MethodImpl(MethodImplOptions.NoInlining)]
    private int Sum(ReadOnlyPoint point)
    {
        return point.X + point.Y;
    }
}