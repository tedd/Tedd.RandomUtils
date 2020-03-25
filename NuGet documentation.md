## Random by type
```csharp
var rnd = new Random();

bool   val1  = rnd.NextBoolean();
sbyte  val2  = rnd.NextSByte();
byte   val3  = rnd.NextByte();
short  val4  = rnd.NextInt16();
ushort val5  = rnd.NextUInt16();
int    val6  = rnd.NextInt32();
uint   val7  = rnd.NextUInt32();
long   val8  = rnd.NextInt64();
ulong  val9  = rnd.NextUInt64();
float  val10 = rnd.NextFloat();
string val11 = rnd.NextString("abcdefg", 8);
```

## Thread safe random
Thread safe random.
```csharp
bool   val1  = ConcurrentRandom.NextBoolean();
sbyte  val2  = ConcurrentRandom.NextSByte();
byte   val3  = ConcurrentRandom.NextByte();
short  val4  = ConcurrentRandom.NextInt16();
ushort val5  = ConcurrentRandom.NextUInt16();
int    val6  = ConcurrentRandom.NextInt32();
uint   val7  = ConcurrentRandom.NextUInt32();
long   val8  = ConcurrentRandom.NextInt64();
ulong  val9  = ConcurrentRandom.NextUInt64();
float  val10 = ConcurrentRandom.NextFloat();
string val11 = ConcurrentRandom.NextString("abcdefg", 8);
ConcurrentRandom.NextBytes(byteArray);
```

## Crypto strength random
```csharp
using rnd = new CryptoRandom();

bool   val1  = rnd.NextBoolean();
sbyte  val2  = rnd.NextSByte();
byte   val3  = rnd.NextByte();
short  val4  = rnd.NextInt16();
ushort val5  = rnd.NextUInt16();
int    val6  = rnd.NextInt32();
uint   val7  = rnd.NextUInt32();
long   val8  = rnd.NextInt64();
ulong  val9  = rnd.NextUInt64();
float  val10 = rnd.NextFloat();
string val11 = rnd.NextString("abcdefg", 8);
rnd.NextBytes(byteArray);
```

## Thread safe random
Thread safe crypto strength random.
```csharp
bool   val1  = ConcurrentCryptoRandom.NextBoolean();
sbyte  val2  = ConcurrentCryptoRandom.NextSByte();
byte   val3  = ConcurrentCryptoRandom.NextByte();
short  val4  = ConcurrentCryptoRandom.NextInt16();
ushort val5  = ConcurrentCryptoRandom.NextUInt16();
int    val6  = ConcurrentCryptoRandom.NextInt32();
uint   val7  = ConcurrentCryptoRandom.NextUInt32();
long   val8  = ConcurrentCryptoRandom.NextInt64();
ulong  val9  = ConcurrentCryptoRandom.NextUInt64();
float  val10 = ConcurrentCryptoRandom.NextFloat();
string val11 = ConcurrentCryptoRandom.NextString("abcdefg", 8);
ConcurrentCryptoRandom.NextBytes(byteArray);
```

## Fast random
```csharp
using rnd = new FastRandom();

bool   val1  = rnd.NextBoolean();
sbyte  val2  = rnd.NextSByte();
byte   val3  = rnd.NextByte();
short  val4  = rnd.NextInt16();
ushort val5  = rnd.NextUInt16();
int    val6  = rnd.NextInt32();
uint   val7  = rnd.NextUInt32();
long   val8  = rnd.NextInt64();
ulong  val9  = rnd.NextUInt64();
float  val10 = rnd.NextFloat();
string val11 = rnd.NextString("abcdefg", 8);
rnd.NextBytes(byteArray);
```

# CryptoRandom
Drop-in replacement for [System.Random](https://msdn.microsoft.com/en-us/library/system.random(v=vs.110).aspx) that gets more random data from Cryptographic Service Provider.

## Example
Works exactly like [System.Random](https://msdn.microsoft.com/en-us/library/system.random(v=vs.110).aspx), except you may want to dispose of it when you are done.
(If you don't dispose of it, the destructor will do it for you upon garbage collect.)
```csharp
var rnd = new CryptoRandom();
var dice = rnd.Next(1, 7); // A random number between 1 and 6 inclusive
rnd.Dispose();
```

Or with using:
```csharp
using (var rnd = new CryptoRandom()) {
	var percent = rnd.NextDouble() * 100;
	Console.WriteLine($"You are {percent}% done, please wait...");
}
```

Note that it is recommended to create a shared Random object, and in case of multiple threads use synchronized access to generate random data.
```csharp
public static class Main {
	public static CryptoRandom Rnd = new CryptoRandomRandom();

	public static void Start() {
		int dice;
		lock (Rnd)
			dice = Rnd.Next(1, 7); // A random number between 1 and 6 inclusive
	}
}
```

# Benchmarks
Each execution is *1.000.000 calculation* of random + loop overhead. So 2.381 ms / 1M is 0.002381 ms per calculation = 2.381 us (microseconds). In case of FastRandom the loop overhead may account for around 50% of the time.

``` ini

BenchmarkDotNet=v0.12.0, OS=Windows 10.0.18363
AMD Ryzen 9 3950X, 1 CPU, 32 logical and 16 physical cores
.NET Core SDK=3.1.101
  [Host]                : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT
  x64 .Net Core 3.1 Ryu : .NET Core 3.1.1 (CoreCLR 4.700.19.60701, CoreFX 4.700.19.60801), X64 RyuJIT

Job=x64 .Net Core 3.1 Ryu  Jit=RyuJit  Platform=X64  
Runtime=.NET Core 3.1  Force=True  InvocationCount=1  
IterationCount=100  LaunchCount=1  UnrollFactor=1  
WarmupCount=15  

```
|                      Method |      Mean |     Error |    StdDev |    Median |       Min |       Max |       P95 |       P90 | Iterations |   Op/s | Ratio | RatioSD | Baseline | Gen 0 | Gen 1 | Gen 2 | Allocated | TotalIssues/Op | BranchInstructions/Op | BranchMispredictions/Op |
|---------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|----------:|----------:|-----------:|-------:|------:|--------:|--------- |------:|------:|------:|----------:|---------------:|----------------------:|------------------------:|
|                  FastRandom |  2.381 ms | 0.0019 ms | 0.0055 ms |  2.380 ms |  2.372 ms |  2.394 ms |  2.392 ms |  2.387 ms |      95.00 | 420.08 |  0.32 |    0.00 |       No |     - |     - |     - |         - |      4,081,116 |             1,053,191 |                  31,383 |
|                SystemRandom |  7.331 ms | 0.0048 ms | 0.0125 ms |  7.330 ms |  7.312 ms |  7.373 ms |  7.350 ms |  7.348 ms |      78.00 | 136.41 |  1.00 |    0.00 |      Yes |     - |     - |     - |         - |     17,775,692 |             5,953,001 |                 353,063 |
|                CryptoRandom |  7.486 ms | 0.0042 ms | 0.0108 ms |  7.485 ms |  7.467 ms |  7.526 ms |  7.506 ms |  7.499 ms |      77.00 | 133.58 |  1.02 |    0.00 |       No |     - |     - |     - |         - |     17,813,788 |             5,968,906 |                 354,385 |
|            ConcurrentRandom | 30.236 ms | 0.0179 ms | 0.0529 ms | 30.218 ms | 30.161 ms | 30.390 ms | 30.355 ms | 30.303 ms |     100.00 |  33.07 |  4.12 |    0.01 |       No |     - |     - |     - |         - |     66,216,751 |            22,077,440 |               1,261,947 |