# Extension methods
Extension methods for System.Random that adds support for more datatypes.

## Note on vanilla System.Random vs this library
Standard System.Random.Next() returns a positive integer, while all this library return full range of values for given datatype.
Standard System.Random.Next(from, to) has "exclusive to" value, meaning it only returns 31 random bits in the 32-bit integer. This library returns random for all 32 and 64 bits on NextInt32(), NextUInt32, NextInt64() and NextUInt64() respectively.

# Examples
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
