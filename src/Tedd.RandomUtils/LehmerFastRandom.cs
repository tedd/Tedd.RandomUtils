using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tedd.RandomUtils
{
    class LehmerFastRandom
    {
        private UInt64 g_lehmer64_state;

        private UInt32 NextUInt32()
        {
            g_lehmer64_state *= 0xda942042e4dd58b5;
            return (UInt32) (g_lehmer64_state >> 32);
        }
        private Int32 NextInt32()
        {
            g_lehmer64_state *= 0xda942042e4dd58b5;
            return (Int32) (g_lehmer64_state >> 32);
        }
        private UInt64 NextUInt64()
        {
            g_lehmer64_state *= 0xda942042e4dd58b5;
            return (UInt64) g_lehmer64_state;
        }
        Make this use vectors and calculate x number ahead
            also make benchmarks DateTimeOffset test
    }
}
