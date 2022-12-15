
using BenchmarkDotNet.Running;
using FluentType.Benchmarks;

BenchmarkRunner.Run<EqualsBenchmark.GuidTests>();
//BenchmarkRunner.Run(typeof(Program).Assembly);