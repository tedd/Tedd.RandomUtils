# Tedd.RandomExtensions
Extension methods for System.Random that adds support for more datatypes.

Random.Next() normally returns a positive integer.
Random.Next(to, from) normally is exclusive on "from" making it difficult to get a full range random integer.

With this library extension method Random.NextInt32() returns a random integer including the full range of negative and positive values.

# Examples
```csharp
var rnd = new Random();

byte val1 = rnd.NextByte();
short val2 = rnd.NextInt16();
ushort val3 = rnd.NextUInt16();
int val4 = rnd.NextInt32();
uint val5 = rnd.NextUInt32();
long val6 = rnd.NextInt64();
ulong val7 = rnd.NextUInt64();
float val8 = rnd.NextFloat();
```
