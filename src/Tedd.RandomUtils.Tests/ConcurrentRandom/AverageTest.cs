using System;
using System.Numerics;
using Xunit;
using rnd = Tedd.RandomUtils.ConcurrentRandom;

namespace Tedd.RandomUtils.Tests.ConcurrentRandom
{
    public class AverageTest
    {
        private int count = 100;
        private int iterations = 100_000;
        private float tolerance = 0.1f;

        [Fact]
        public void TestNext()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                {
                    sum += rnd.Next();
                }

                sum /= iterations;

                var mid = Int32.MaxValue / 2;
                Assert.InRange((Int32)sum, mid - (Int32.MaxValue * tolerance), mid + (Int32.MaxValue * tolerance));
            }
        }

        [InlineData(1)]
        [InlineData(10_000)]
        [InlineData(100_000)]
        [Theory]
        public void TestNext_max(int max)
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                {
                    sum += rnd.Next(max);
                }

                sum /= iterations;

                var mid = max / 2;
                Assert.InRange((Int32)sum, mid - (max * tolerance), mid + (max * tolerance));
            }
        }

        [InlineData(-1, 1)]
        [InlineData(-500, 1_000)]
        [InlineData(9_999, 10_000)]
        [InlineData(Int32.MinValue, 100_000)]
        [Theory]
        public void TestNext_min_max(int min, int max)
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                {
                    sum += rnd.Next(min, max);
                }

                sum /= iterations;

                var range = (Int64)max - (Int64)min;
                var mid = (Int64)min + range / 2;
                Assert.InRange((Int64)sum, mid - (range * tolerance), mid + (range * tolerance));
            }
        }

        [InlineData(1_000)]
        [InlineData(10_000)]
        [InlineData(100_000)]
        [Theory]
        public void TestNextBytes(int size)
        {
            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                var array = new byte[size];
                rnd.NextBytes(array);
                Assert.Equal(size, array.Length);

                for (var i = 0; i < size; i++)
                {
                    sum += array[i];
                }

                sum /= size;

                Assert.InRange((int)sum, 120, 140);
            }
        }

        [Fact]
        public void TestBoolean()
        {

            for (var c = 0; c < count; c++)
            {
                var t = 0;
                var f = 0;
                for (var i = 0; i < iterations; i++)
                {
                    if (rnd.NextBoolean())
                        t++;
                    else
                        f++;
                }

                var mid = iterations / 2;
                Assert.InRange(t, mid - (iterations * tolerance), mid + (iterations * tolerance));
                Assert.InRange(f, mid - (iterations * tolerance), mid + (iterations * tolerance));
            }
        }

        [Fact]
        public void TestFloat()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += (Int64)rnd.NextFloat();
                sum /= iterations;
                var mid = (float.MinValue + float.MaxValue) / 2;
                Assert.InRange((Int64)sum, (Int64)(mid - float.MaxValue * 2 * tolerance), (Int16)(mid + float.MaxValue * 2 * tolerance));
            }
        }

        [Fact]
        public void TestDouble()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += (Int64)rnd.NextDouble();
                sum /= iterations;
                var mid = (Double.MinValue + Double.MaxValue) / 2;
                Assert.InRange((Int64)sum, (Int64)(mid - Double.MaxValue * 2 * tolerance), (Int16)(mid + Double.MaxValue * 2 * tolerance));
            }
        }

        [Fact]
        public void TestSample()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += (Int64)rnd.Sample();
                sum /= iterations;
                var mid = (Double.MinValue + Double.MaxValue) / 2;
                Assert.InRange((Int64)sum, (Int64)(mid - Double.MaxValue * 2 * tolerance), (Int16)(mid + Double.MaxValue * 2 * tolerance));
            }
        }

        [Fact]
        public void TestSByte()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += rnd.NextSByte();
                sum /= iterations;
                var mid = (SByte.MinValue + SByte.MaxValue) / 2;
                Assert.InRange(sum, (SByte)(mid - Byte.MaxValue * tolerance), (SByte)(mid + Byte.MaxValue * tolerance));
            }
        }

        [Fact]
        public void TestByte()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += rnd.NextByte();
                sum /= iterations;
                var mid = (Byte.MinValue + Byte.MaxValue) / 2;
                Assert.InRange(sum, (Byte)(mid - Byte.MaxValue * tolerance), (Byte)(mid + Byte.MaxValue * tolerance));
            }
        }

        [Fact]
        public void TestInt16()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += rnd.NextInt16();
                sum /= iterations;
                var mid = (Int16.MinValue + Int16.MaxValue) / 2;
                Assert.InRange(sum, (Int16)(mid - UInt16.MaxValue * tolerance), (Int16)(mid + UInt16.MaxValue * tolerance));
            }
        }

        [Fact]
        public void TestUInt16()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += rnd.NextUInt16();
                sum /= iterations;
                var mid = (UInt16.MinValue + UInt16.MaxValue) / 2;
                Assert.InRange(sum, (UInt16)(mid - UInt16.MaxValue * tolerance), (UInt16)(mid + UInt16.MaxValue * tolerance));
            }
        }


        [Fact]
        public void TestInt32()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += rnd.NextInt32();
                sum /= iterations;
                var mid = (Int32.MinValue + Int32.MaxValue) / 2;
                Assert.InRange(sum, (Int32)(mid - UInt32.MaxValue * tolerance), (Int32)(mid + UInt32.MaxValue * tolerance));
            }
        }

        [Fact]
        public void TestUInt32()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += rnd.NextUInt32();
                sum /= iterations;
                var mid = (UInt32.MinValue + UInt32.MaxValue) / 2;
                Assert.InRange(sum, (UInt32)(mid - UInt32.MaxValue * tolerance), (UInt32)(mid + UInt32.MaxValue * tolerance));
            }
        }


        [Fact]
        public void TestInt64()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += rnd.NextInt64();
                sum /= iterations;
                var mid = 0;
                var t = (Int64)((double)Int64.MaxValue * (double)tolerance);
                Assert.InRange((Int64)sum, mid - t, mid + t);
            }
        }

        [Fact]
        public void TestUInt64()
        {

            for (var c = 0; c < count; c++)
            {
                BigInteger sum = 0;
                for (var i = 0; i < iterations; i++)
                    sum += (Int64)rnd.NextUInt64();
                sum /= iterations;
                var mid = 0;
                var t = (Int64)((double)Int64.MaxValue * (double)tolerance);
                Assert.InRange((Int64)sum, mid - t, mid + t);
            }
        }


        [InlineData("a", 1)]
        [InlineData("abc", 100)]
        [InlineData("abcdeFGHIJ!\"#�%&/()=1234", 10_000)]
        [Theory]
        public void TestString_String(string chars, int length)
        {

            for (var c = 0; c < count; c++)
            {
                var str = rnd.NextString(chars, length);
                Assert.Equal(length, str.Length);
                foreach (var ch in chars)
                    Assert.Contains(ch, str);
            }
        }

        [InlineData("a", 1)]
        [InlineData("abc", 100)]
        [InlineData("abcdeFGHIJ!\"#�%&/()=1234", 10_000)]
        [Theory]
        public void TestString_Chars(string chars, int length)
        {

            for (var c = 0; c < count; c++)
            {
                var str = rnd.NextString(chars.ToCharArray(), length);
                Assert.Equal(length, str.Length);
                foreach (var ch in chars)
                    Assert.Contains(ch, str);
            }
        }

#if HASSPAN
        [InlineData("a", 1)]
        [InlineData("abc", 100)]
        [InlineData("abcdeFGHIJ!\"#�%&/()=1234", 10_000)]
        [Theory]
        public void TestString_Span(string chars, int length)
        {

            for (var c = 0; c < count; c++)
            {
                var str = rnd.NextString(new ReadOnlySpan<char>(chars.ToCharArray()), length);
                Assert.Equal(length, str.Length);
                foreach (var ch in chars)
                    Assert.Contains(ch, str);
            }
        }


        [Fact]
        public void TestString_ArgumentOutOfRangeException()
        {

            Assert.Throws<ArgumentOutOfRangeException>(() => rnd.NextString("abc", -1));
        }


        [Fact]
        public void TestString_Empty()
        {

            Assert.Equal(string.Empty, rnd.NextString("abc", 0));
        }
#endif
    }

}
