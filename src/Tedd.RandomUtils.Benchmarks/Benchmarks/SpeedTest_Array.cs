using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;

namespace Tedd.RandomUtils.Benchmarks.Benchmarks
{
    [Config(typeof(TestConfig))]
    public class SpeedTest_Array
    {
        private Random _random;
        private const int Iterations = 10_000;
        public byte[] Result = new byte[4096];
        private FastRandom _fastRandom;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _random = new Random();

            // Fill SIMD vector with seed
            _vectorSize = Vector<UInt64>.Count;
            _vectorNumber = _vectorSize;
            var u = new UInt64[_vectorSize];
            var v = ((UInt64)Environment.TickCount << 1) | ((UInt64)(Environment.TickCount + 10) << 32);
            for (var i = 0; i < _vectorSize; i++)
                u[i] = v >> i;
            lehmer_simd_state = new Vector<UInt64>(new Span<UInt64>(u));
            var c = new UInt64[_vectorSize];
            for (var i = 0; i < _vectorSize; i++)
                c[i] = 0xda942042e4dd58b5;
            _lehmerConst = new Vector<UInt64>(c);
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
                _random.NextBytes(Result);
        }

        #endregion

        #region LehmerNaive

        [Benchmark()]
        public void LehmerNaive()
        {
            for (var n = 0; n < Iterations; n++)
                _fastRandom.NextBytes(Result);
        }

        //private UInt64 lehmer_state = ((UInt64)Environment.TickCount) | ((UInt64)(Environment.TickCount + 10) << 32);

        //private Int32 LehmerNext()
        //{
        //    lehmer_state *= 0xda942042e4dd58b5;
        //    return (Int32)(lehmer_state >> 32);
        //}

        #endregion
        #region LehmerStatic

        //[Benchmark()]
        //public void LehmerStatic()
        //{
        //    for (var n = 0; n < Iterations; n++)
        //        FastRandomStatic.NextBytes(Result);
        //}


        #endregion

        #region LehmerSIMD

        [Benchmark()]
        public void LehmerSIMD()
        {
            for (var n = 0; n < Iterations; n++)
                LehmerSIMDNext(Result);
        }

        private Vector<UInt64> lehmer_simd_state;
        private Vector<ulong> _lehmerConst;
        private Vector<uint> _lehmer_simds;
        private int _vectorSize;
        private int _vectorNumber;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void LehmerSIMDNext(byte[] array)
        {
            for (var i = 0; i < array.Length; i++) {

                if (_vectorNumber == _vectorSize)
                {
                    _vectorNumber = 0;
                    lehmer_simd_state = Vector.Multiply(lehmer_simd_state, _lehmerConst);
                    //_lehmer_simds = Vector.AsVectorUInt32(lehmer_simd_state);
                }
                array[i] = (Byte)(lehmer_simd_state[_vectorNumber++] >> 32);
            }
        }
        #endregion
    }
}
