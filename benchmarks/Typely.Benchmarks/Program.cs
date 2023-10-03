using BenchmarkDotNet.Running;
using Typely.Benchmarks;

BenchmarkRunner.Run<ValueObjectLibrariesBenchmark.Int>();
//BenchmarkRunner.Run<ErrorBenchmark>();
//BenchmarkRunner.Run<EqualsBenchmark.GuidTests>();
//BenchmarkRunner.Run(typeof(Program).Assembly);