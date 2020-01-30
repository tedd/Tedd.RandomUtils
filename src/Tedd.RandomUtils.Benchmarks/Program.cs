using System;
using BenchmarkDotNet.Running;
using Tedd.RandomUtils.Benchmarks.Benchmarks;

namespace Tedd.RandomUtils.Benchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            var speedTest = new SpeedTest();
            speedTest.GlobalSetup();
            speedTest.IterationSetup();
            speedTest.SystemRandom();
            speedTest.LehmerNaive();



            var summary1 = BenchmarkRunner.Run<SpeedTest>();
        }
    }
}
