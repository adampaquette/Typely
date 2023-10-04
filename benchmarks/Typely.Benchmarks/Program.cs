using BenchmarkDotNet.Running;
using Typely.Benchmarks;

BenchmarkRunner.Run<ValueObjectLibrariesBenchmark.IntSerialization>();
//BenchmarkRunner.Run<ValueObjectLibrariesBenchmark.IntRead>();
//BenchmarkRunner.Run<ErrorBenchmark>();
//BenchmarkRunner.Run<EqualsBenchmark.GuidTests>();
//BenchmarkRunner.Run(typeof(Program).Assembly);