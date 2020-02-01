using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;

namespace Tedd.RandomUtils.Benchmarks.Benchmarks
{
    [Orderer(SummaryOrderPolicy.FastestToSlowest, MethodOrderPolicy.Declared)]
    [Config(typeof(TestConfig))]
    public class SpeedTest_All
    {
        public Random _random;
        private const int Iterations = 1_000_000;
        //public long Result;
        public FastRandom _fastRandom;
        public CryptoRandom _cryptoRandom;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _random = new Random();
            _fastRandom = new FastRandom();
            _cryptoRandom = new CryptoRandom();
        }

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            _cryptoRandom.Dispose();
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
                //Result += 
                    _random.Next();
        }

        #endregion
        #region CryptoRandom

        [Benchmark()]
        public void CryptoRandom()
        {
            for (var n = 0; n < Iterations; n++)
                //Result += 
                    _random.Next();
        }

        #endregion
        #region FastRandom

        [Benchmark()]
        public void FastRandom()
        {
            for (var n = 0; n < Iterations; n++)
                //Result += 
                    _fastRandom.NextInt32();
        }

        //private UInt64 lehmer_state = ((UInt64)Environment.TickCount) | ((UInt64)(Environment.TickCount + 10) << 32);

        //private Int32 LehmerNext()
        //{
        //    lehmer_state *= 0xda942042e4dd58b5;
        //    return (Int32)(lehmer_state >> 32);
        //}

        #endregion

        #region FastRandomStatic
        [Benchmark()]
        public void FastRandomStatic()
        {
            for (var n = 0; n < Iterations; n++)
                //Result += 
                    Tedd.RandomUtils.FastRandomStatic.NextInt32();
        }
        #endregion
        
        #region ConcurrentRandom
        [Benchmark()]
        public void ConcurrentRandom()
        {
            for (var n = 0; n < Iterations; n++)
                //Result += 
                Tedd.RandomUtils.ConcurrentRandom.NextInt32();
        }
        #endregion

        #region ConcurrentRandomThreadLocal
        [Benchmark()]
        public void ConcurrentRandomThreadLocal()
        {
            for (var n = 0; n < Iterations; n++)
                //Result += 
                    Tedd.RandomUtils.ConcurrentRandomThreadLocal.NextInt32();
        }
        #endregion

        #region ConcurrentRandomLock

        [Benchmark()]
        public void ConcurrentRandomLock()
        {
            for (var n = 0; n < Iterations; n++)
                //Result += 
                    Tedd.RandomUtils.ConcurrentRandomLock.NextInt32();
        }
        #endregion

   
    }
}
