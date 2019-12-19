# Extension methods
Extension methods for System.Random that adds support for more datatypes.
NextSByte(), NextByte(), NextInt16(), NextUInt16(), NextIn32(), NextUInt32(), NextInt64(), NextUInt64() and NextFloat().

ConcurrentRandom provides a lock free thread safe way to access System.Random.

CryptoRandom provides 

# Examples

## Random by type
```csharp
var rnd = new Random();

sbyte val1 = rnd.NextSByte();
byte val1 = rnd.NextByte();
short val2 = rnd.NextInt16();
ushort val3 = rnd.NextUInt16();
int val4 = rnd.NextInt32();
uint val5 = rnd.NextUInt32();
long val6 = rnd.NextInt64();
ulong val7 = rnd.NextUInt64();
float val8 = rnd.NextFloat();
```

## Thread safe random
Thread safe random without locking.
```csharp
sbyte val1 = ConcurrentRandom.NextSByte();
byte val1 = ConcurrentRandom.NextByte();
short val2 = ConcurrentRandom.NextInt16();
ushort val3 = ConcurrentRandom.NextUInt16();
int val4 = ConcurrentRandom.NextInt32();
uint val5 = ConcurrentRandom.NextUInt32();
long val6 = ConcurrentRandom.NextInt64();
ulong val7 = ConcurrentRandom.NextUInt64();
float val8 = ConcurrentRandom.NextFloat();
```

## Crypto strength random
```csharp
using rnd = new CryptoRandom();

sbyte val1 = rnd.NextSByte();
byte val1 = rnd.NextByte();
short val2 = rnd.NextInt16();
ushort val3 = rnd.NextUInt16();
int val4 = rnd.NextInt32();
uint val5 = rnd.NextUInt32();
long val6 = rnd.NextInt64();
ulong val7 = rnd.NextUInt64();
float val8 = rnd.NextFloat();
```

# CryptoRandom
Drop-in replacement for [System.Random](https://msdn.microsoft.com/en-us/library/system.random(v=vs.110).aspx) that gets more random data from Cryptographic Service Provider.

## Example
Works exactly like [System.Random](https://msdn.microsoft.com/en-us/library/system.random(v=vs.110).aspx), except you may want to dispose of it when you are done.
(If you don't dispose of it, the destructor will do it for you upon garbage collect.)

	var rnd = new Tedd.MoreRandom.Random();
	var dice = rnd.Next(1, 7); // A random number between 1 and 6 inclusive
	rnd.Dispose();

Or with using:

	using (var rnd = new Tedd.MoreRandom.Random()) {
		var percent = rnd.NextDouble() * 100;
		Console.WriteLine($"You are {percent}% done, please wait...");
	}

Note that it is recommended to create a shared Random object, and in case of multiple threads use synchronized access to generate random data.

	public static class Main {
		public static Tedd.MoreRandom.Random Rnd = new Tedd.MoreRandom.Random();

		public static void Start() {
			int dice;
			lock (Rnd)
				dice = Rnd.Next(1, 7); // A random number between 1 and 6 inclusive
		}
	}


## Thread safety
Any public static (Shared in Visual Basic) members of this type are thread safe. Any instance members are not guaranteed to be thread safe.

## Background
[System.Random](https://msdn.microsoft.com/en-us/library/system.random(v=vs.110).aspx) is based on so called "pseudorandom" algorithm. This means that given a seed (default: number of milliseconds since computer was started) math is used to generate seemingly random numbers. If given the same seed, a sequence of random numbers will look the same every time. For most cases this is fine, but in some cases you need more random data. One such case is cryptography, where a pseudorandom generator such as [System.Random](https://msdn.microsoft.com/en-us/library/system.random(v=vs.110).aspx) would generate a predictable sequence of numbers.

[RNGCryptoServiceProvider](https://msdn.microsoft.com/en-us/library/system.security.cryptography.rngcryptoserviceprovider(v=vs.110).aspx) through [RandomNumberGenerator](https://msdn.microsoft.com/en-us/library/system.security.cryptography.randomnumbergenerator(v=vs.110).aspx) provides "cryptography grade random" numbers. These numbers are a bit more random as they are provided by the operating system, which has methods of collecting random data.

RandomNumberGenerator gives you a bunch of random bytes. It's up to you to convert to a number and size for whatever purpose. System.Random however has a simple interface, for example rnd.Next(10).

This is where MoreRandom comes in. Tedd.MoreRandom.Random mimics System.Random and is a drop-in replacement. You get the power of RandomNumberGenerator with the ease of System.Random.

## Compatibility
Created using .Net Standard 1.3.

## Unit testing
xUnit in .Net Core with near 100% code coverage. Boundary checks as well as average check (for statistical distribution) on vast number of samples.

# Remarks
## Vanilla System.Random vs this library
Standard System.Random.Next() returns a positive integer, while all this library return full range of values for given datatype.
Standard System.Random.Next(from, to) has "exclusive to" value, meaning it only returns 31 random bits in the 32-bit integer. This library returns random for all 32 and 64 bits on NextInt32(), NextUInt32, NextInt64() and NextUInt64() respectively.