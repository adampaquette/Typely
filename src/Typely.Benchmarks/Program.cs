using BenchmarkDotNet.Running;
using Typely.Benchmarks;

BenchmarkRunner.Run<ErrorBenchmark>();
//BenchmarkRunner.Run<EqualsBenchmark.GuidTests>();
//BenchmarkRunner.Run(typeof(Program).Assembly);