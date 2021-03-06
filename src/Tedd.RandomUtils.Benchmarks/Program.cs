﻿using System;
using BenchmarkDotNet.Running;
using Tedd.RandomUtils.Benchmarks.Benchmarks;

namespace Tedd.RandomUtils.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            {
                var speedTest = new SpeedTest_Single();
                speedTest.GlobalSetup();
                speedTest.IterationSetup();

                speedTest.SystemRandom();
                speedTest.LehmerNaive();
                speedTest.LehmerStatic();
                speedTest.LehmerSIMD();
            }

            {
                var speedTest = new SpeedTest_Array();
                speedTest.GlobalSetup();
                speedTest.IterationSetup();

                speedTest.SystemRandom();
                speedTest.LehmerNaive();
                //speedTest.LehmerStatic();
                speedTest.LehmerSIMD();
            }

            {
                var speedTest = new SpeedTest_All();
                speedTest.GlobalSetup();
                speedTest.IterationSetup();

                speedTest.SystemRandom();
                speedTest.CryptoRandom();
                speedTest.FastRandom();
                speedTest.FastRandomStatic();

                speedTest.GlobalCleanup();
            }   
            {
                var speedTest = new SpeedTest_Double();
                speedTest.GlobalSetup();
                speedTest.IterationSetup();

                speedTest.SystemRandom();
                speedTest.FastRandom();


            }


            //var summary1 = BenchmarkRunner.Run<SpeedTest_Single>();
            //var summary2 = BenchmarkRunner.Run<SpeedTest_Array>();
            //var summary3 = BenchmarkRunner.Run<SpeedTest_All>();
            var summary4 = BenchmarkRunner.Run<SpeedTest_Double>();
        }
    }
}
