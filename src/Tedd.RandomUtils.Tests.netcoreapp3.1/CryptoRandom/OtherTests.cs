using System;
using Xunit;

namespace Tedd.RandomUtils.Tests.CryptoRandom
{
    public class OtherTests
    {
        [Fact]
        public void TestFinalize()
        {
            {
                var rnd = new RandomUtils.CryptoRandom();
                _ = rnd.Next();
            }
            GC.Collect(2, GCCollectionMode.Forced, true, true);
        }
    }
}