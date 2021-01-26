using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace Tedd.RandomUtils.Benchmarks.Benchmarks
{
    [Config(typeof(TestConfig))]
    public class SpeedTest_Double
    {
        private Random _random;
        private const int Iterations = 10_000_000;
        public Double Result;
        private FastRandom _fastRandom;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _random = new Random();
            _fastRandom = new FastRandom();
        }

        [IterationSetup]
        public void IterationSetup()
        {
        }

        #region SystemRandom

        [Benchmark(Baseline = true)]
        public void SystemRandom()
        {
            for (var n = 0; n < Iterations; n++)
                Result = _random.NextDouble();
        }

        #endregion
        

        [Benchmark()]
        public void FastRandom()
        {
            for (var n = 0; n < Iterations; n++)
                Result = _fastRandom.NextDouble();
        }
        [Benchmark()]
        public void FastRandom_Divide()
        {
            for (var n = 0; n < Iterations; n++)
            {
                double d;
                do
                {
                    var i = _fastRandom.NextUInt64();
                    d = (double) i / (double) ulong.MaxValue;
                } while (d >= 1.0D);

                Result = d;
            }
        }

    }
}
