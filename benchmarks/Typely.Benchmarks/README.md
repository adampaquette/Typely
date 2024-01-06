# ClassVsStructBenchmark

| Method | N   | Mean      | Error     | StdDev    | Median    | Gen0   | Allocated |
|------- |---- |----------:|----------:|----------:|----------:|-------:|----------:|
| Struct | 10  | 0.0000 ns | 0.0000 ns | 0.0000 ns | 0.0000 ns |      - |         - |
| Class  | 10  | 2.0950 ns | 0.0106 ns | 0.0088 ns | 2.0922 ns | 0.0015 |      24 B |
| Struct | 100 | 0.0011 ns | 0.0011 ns | 0.0010 ns | 0.0008 ns |      - |         - |
| Class  | 100 | 2.1348 ns | 0.0534 ns | 0.0473 ns | 2.1205 ns | 0.0015 |      24 B |
