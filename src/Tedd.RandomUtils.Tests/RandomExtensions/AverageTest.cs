using System;
using System.Numerics;
using Xunit;

namespace Tedd.RandomUtils.Tests.RandomExtensions
{
    public class AverageTest
    {
        private int count = 100;
        private int iterations = 100_000;
        private float tolerance = 0.1f;


        [Fact]
        public void TestFloat()
        {
            var rnd = new Random();
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
        public void TestSByte()
        {
            var rnd = new Random();
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
            var rnd = new Random();
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
            var rnd = new Random();
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
            var rnd = new Random();
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
            var rnd = new Random();
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
            var rnd = new Random();
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
            var rnd = new Random();
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
            var rnd = new Random();
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
    }
}
