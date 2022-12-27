
using BenchmarkDotNet.Running;
using Typely.Benchmarks;

BenchmarkRunner.Run<EqualsBenchmark.GuidTests>();
//BenchmarkRunner.Run(typeof(Program).Assembly);