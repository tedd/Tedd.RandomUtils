using System;
using Xunit;

namespace Tedd.RandomUtils.Tests.CryptoRandom
{
    public class BoundaryTest
    {
        private RandomUtils.CryptoRandom _trueRandom = new RandomUtils.CryptoRandom();
        private const int TestIterations = 1_000_000;

        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(959)]
        [Theory]
        public void Next_max(int max)
        {
            if (max < 0)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _trueRandom.Next(max));
                return;
            }

            for (int i = 0; i < TestIterations; i++)
            {
                var val = _trueRandom.Next(max);
                if (max == 0)
                {
                    Assert.True(val == 0);
                }
                else
                {
                    Assert.True(val > -1);
                    Assert.True(val < max);
                }
            }
        }

        [InlineData(-5, -1)]     // -5 to -2
        [InlineData(-1, -5)]     // ArgumentOutOfRangeException
        [InlineData(-1, -1)]     // -1
        [InlineData(-100, 0)]    // -100 to -1
        [InlineData(50, 52)]     // 50 to 51
        [InlineData(10, 100)]    // 10 to 99
        [InlineData(573, 1523)]  // ...
        [Theory]
        public void Next_min_max(int min, int max)
        {
            if (max < min)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _trueRandom.Next(min, max));
                return;
            }

            for (int i = 0; i < TestIterations; i++)
            {
                var val = _trueRandom.Next(min, max);
                if (max == min)
                {
                    Assert.True(val == min);
                }
                else
                {
                    Assert.True(val >= min);
                    Assert.True(val < max);
                }
            }
        }

        [Fact]
        public void Next()
        {
            for (int i = 0; i < TestIterations; i++)
            {
                var val = _trueRandom.Next();
                Assert.True(val >= 0);
                Assert.True(val < Int32.MaxValue);
            }
        }

        [Fact]
        public void NextDouble()
        {
            for (int i = 0; i < TestIterations; i++)
            {
                var val = _trueRandom.NextDouble();
                Assert.True(val >= 0);
                Assert.True(val < 1.0D);
            }
        }

    }
}