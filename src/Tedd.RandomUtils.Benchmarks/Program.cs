using System;
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
                speedTest.LehmerStatic();
                speedTest.LehmerSIMD();
            }


            var summary1 = BenchmarkRunner.Run<SpeedTest_Single>();
            var summary2 = BenchmarkRunner.Run<SpeedTest_Array>();
        }
    }
}
