using BenchmarkDotNet.Running;
using Typely.Benchmarks;

BenchmarkRunner.Run<ReadonlyStructBenchmark>();
//BenchmarkRunner.Run<ClassVsStructBenchmark>();
//BenchmarkRunner.Run<ValueObjectLibrariesBenchmark.IntSerialization>();
//BenchmarkRunner.Run<ValueObjectLibrariesBenchmark.IntRead>();
//BenchmarkRunner.Run<ErrorBenchmark>();
//BenchmarkRunner.Run<EqualsBenchmark.GuidTests>();
//BenchmarkRunner.Run(typeof(Program).Assembly);