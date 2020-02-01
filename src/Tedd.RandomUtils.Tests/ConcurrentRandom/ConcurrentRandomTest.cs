using System;
using System.Reflection;
using System.Threading;
using Xunit;

namespace Tedd.RandomUtils.Tests.ConcurrentRandom
{
    public class ConcurrentRandomThreadLocalTest
    {
        //private int count = 100;
        //private int iterations = 100_000;
        //private float tolerance = 0.1f;


        [Fact]
        public void TestSequence()
        {
            var threadCount = 20;
            var randomSamplesPerThreadCount = 10_000;
            var rnd = new Random();
            var seed = rnd.NextInt32();


            var seedRandom = new Random(seed);
            var facitData = new int[threadCount][];
            for (var i = 0; i < threadCount; i++)
            {
                // Create random object with same value as ConcurrentStatic would
                var tr = new Random(seedRandom.NextInt32());
                // Now get samples
                var fd = facitData[i] = new int[randomSamplesPerThreadCount];
                for (var s = 0; s < randomSamplesPerThreadCount; s++)
                    fd[s] = tr.NextInt32();
            }



            // Trickery: We replace the original Random used to create Random objects with one we control the seed of.
            var field = typeof(RandomUtils.ConcurrentRandomThreadLocal).GetField("Random", BindingFlags.Static | BindingFlags.NonPublic);
            field.SetValue(null, new Random(seed));

            var threads = new Thread[threadCount];
            var threadData = new int[threadCount][];
            for (var i = 0; i < threadCount; i++)
            {
                var started = false;
                threadData[i] = new int[randomSamplesPerThreadCount];
                threads[i] = new Thread(() => ThreadRandomLoop(threadData[i], ref started));
                threads[i].IsBackground = true;
                threads[i].Start();
                // We need to make sure threads are started in the correct order for random sequence to be predictable
                while (!started) { }
            }

            // Wait for all threads
            for (var i = 0; i < threadCount; i++)
                threads[i].Join();

            // Now compare
            for (var i = 0; i < threadCount; i++)
            {
                var fd = facitData[i];
                var td = threadData[i];
                for (var s = 0; s < randomSamplesPerThreadCount; s++)
                    Assert.Equal(fd[i], td[i]);
            }

        }

        private void ThreadRandomLoop(int[] threadData, ref bool started)
        {
            var instance = RandomUtils.ConcurrentRandomThreadLocal.GetUnsafeInstance();
            started = true;
            for (var i = 0; i < threadData.Length; i++)
            {
                threadData[i] = RandomUtils.ConcurrentRandom.NextInt32();
            }
        }

    }
}