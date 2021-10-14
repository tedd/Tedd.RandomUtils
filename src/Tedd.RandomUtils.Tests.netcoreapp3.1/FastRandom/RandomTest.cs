using System;
using Xunit;

namespace Tedd.RandomUtils.Tests.FastRandom
{
    public class RandomTest
    {
        private RandomUtils.FastRandom _trueRandom = new RandomUtils.FastRandom();
        private const int TestIterations = 1_000_000;

        [InlineData(0, 4)]
        [InlineData(10, 21)]
        [InlineData(-10223, 165373)]
        [InlineData(Int32.MinValue, 100_000)]
        [Theory]
        public void Ctor(int min, int max)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RandomUtils.FastRandom(0));
            var rnd = new RandomUtils.FastRandom(1);

            ulong accumulated = 0;
            for (int i = 0; i < TestIterations; i++)
            {
                accumulated += (ulong)_trueRandom.Next(min, max);
            }
            decimal avg = (decimal)accumulated / (decimal)TestIterations;

            // We expect average of high amount of random numbers to be close to 0.5D.
            var diff = min - Math.Abs(avg - (decimal)((max - 1 - min) * 0.5m));
            Assert.True(diff < 0.01m, $"Diff {diff} must be less than 0.01m");
        }

        [InlineData(0, 4)]
        [InlineData(10, 21)]
        [InlineData(-10223, 165373)]
        [InlineData(Int32.MinValue, 100_000)]
        [Theory]
        public void Next_min_max(int min, int max)
        {
            ulong accumulated = 0;
            for (int i = 0; i < TestIterations; i++)
            {
                accumulated += (ulong)_trueRandom.Next(min, max);
            }
            decimal avg = (decimal)accumulated / (decimal)TestIterations;

            // We expect average of high amount of random numbers to be close to 0.5D.
            var diff = min - Math.Abs(avg - (decimal)((max - 1 - min) * 0.5m));
            Assert.True(diff < 0.01m, $"Diff {diff} must be less than 0.01m");
        }

        [Fact]
        public void NextSingle()
        {
            decimal accumulated = 0;
            for (int i = 0; i < TestIterations; i++)
            {
                accumulated += (decimal)_trueRandom.NextSingle();
            }
            accumulated /= TestIterations;

            // We expect average of high amount of random numbers to be close to 0.5D.
            var diff = Math.Abs((decimal)accumulated - 0.5m);
            Assert.True(diff < 0.01m, $"Diff {diff} must be less than 0.01m");
        }

        [Fact]
        public void NextDouble()
        {
            decimal accumulated = 0;
            for (int i = 0; i < TestIterations; i++)
            {
                accumulated += (decimal)_trueRandom.NextDouble();
            }
            accumulated /= TestIterations;

            // We expect average of high amount of random numbers to be close to 0.5D.
            var diff = Math.Abs((decimal)accumulated - 0.5m);
            Assert.True(diff < 0.01m, $"Diff {diff} must be less than 0.01m");
        }

        [Fact]
        public void NextDoubleHistogram()
        {
            var iterations = 1_000_000_000;
            var rCount = new int[100_000];
            for (int i = 0; i < iterations; i++)
            {
                var d = (Decimal)_trueRandom.NextDouble();
                var n = (int)(d * rCount.Length);
                rCount[n]++;
            }

            var expected = iterations / rCount.Length;
            var maxOff = (int)(expected * 0.95f);
            for (var i = 0; i < rCount.Length; i++)
                Assert.True(rCount[i] > maxOff, $"Histogram has uneven distribution, count is {rCount[i]} < {maxOff}");
        } 

        [Fact]
        public void NextFloatHistogram()
        {
            var iterations = 1_000_000_000;
            var rCount = new int[100_000];
            for (int i = 0; i < iterations; i++)
            {
                var d = (Decimal)_trueRandom.NextFloat();
                var n = (int)(d * rCount.Length);
                rCount[n]++;
            }

            var expected = iterations / rCount.Length;
            var maxOff = (int)(expected * 0.95f);
            for (var i = 0; i < rCount.Length; i++)
                Assert.True(rCount[i] > maxOff, $"Histogram has uneven distribution, count is {rCount[i]} < {maxOff}");
        }

        [InlineData(1)]
        [InlineData(3)]
        [InlineData(177)]
        [Theory]
        public void NextBytes(int size)
        {
            ulong accumulated = 0;

            // If the sample set is too small we need to loop a few times extra to get enough statistical data
            var loops = (int)Math.Ceiling(50f / (float)size);

            for (var loop = 0; loop < loops; loop++)
            {
                for (int i = 0; i < TestIterations; i++)
                {
                    var buffer = new byte[size];
                    _trueRandom.NextBytes(buffer);

                    foreach (var b in buffer)
                        accumulated += (ulong)b;
                }
            }
            var avg = (decimal)accumulated / (decimal)(TestIterations * size * loops);

            // We expect average of high amount of random numbers to be close to 127.5D.
            var diff = Math.Abs(avg - 127.5m);
            Assert.True(diff < 0.02m, $"Diff {diff} must be less than 0.02m");
        }

    }
}
