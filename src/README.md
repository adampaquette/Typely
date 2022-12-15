|               Method |      Mean |     Error |    StdDev |    Median | Allocated |
|--------------------- |----------:|----------:|----------:|----------:|----------:|
|    Int_EqualOperator | 0.0001 ns | 0.0006 ns | 0.0005 ns | 0.0000 ns |         - |
| Int_EqualityComparer | 0.0031 ns | 0.0054 ns | 0.0048 ns | 0.0000 ns |         - |
|           Int_Equals | 0.0145 ns | 0.0171 ns | 0.0160 ns | 0.0105 ns |         - |


|                  Method |     Mean |     Error |    StdDev | Allocated |
|------------------------ |---------:|----------:|----------:|----------:|
|    String_EqualOperator | 2.460 ns | 0.0551 ns | 0.0460 ns |         - |
| String_EqualityComparer | 5.186 ns | 0.0962 ns | 0.0900 ns |         - |
|           String_Equals | 2.157 ns | 0.0445 ns | 0.0416 ns |         - |
|     String_StaticEquals | 2.406 ns | 0.0208 ns | 0.0195 ns |         - |

|                             Method |      Mean |     Error |    StdDev | Allocated |
|----------------------------------- |----------:|----------:|----------:|----------:|
| ValueObjectString_EqualityComparer | 5.3953 ns | 0.0362 ns | 0.0339 ns |         - |
|           ValueObjectString_Equals | 0.0000 ns | 0.0000 ns | 0.0000 ns |         - |