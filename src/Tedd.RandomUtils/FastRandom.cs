using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Tedd.RandomUtils
{
    /// <summary>
    /// Lehmer random generator is over 3-4 times faster than System.Random and passes all the Dieharder tests of randomness.
    /// Random is calculated by a single multiply and bitshift operation.
    /// </summary>
    public class FastRandom
    {
        private UInt64 _lehmerState;

        public FastRandom() : this((UInt64)((Int64)Environment.TickCount | (Int64)(Environment.TickCount + 10) << 32))
        {
        }

        public FastRandom(UInt64 seed)
        {
            _lehmerState = seed;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt32 NextUInt32()
        {
            _lehmerState *= 0xda942042e4dd58b5;
            return (UInt32)(_lehmerState >> 32);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 NextInt32()
        {
            _lehmerState *= 0xda942042e4dd58b5;
            return (Int32)(_lehmerState >> 32);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int64 NextInt64() => (Int64)((Int64)NextInt32() | (((Int64)NextInt32()) << 32));
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64 NextUInt64() => (UInt64)((UInt64)NextUInt32() | ((UInt64)NextUInt32() << 32));


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SByte NextSByte() => (SByte)NextInt32();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Byte NextByte() => (Byte)NextInt32();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int16 NextInt16() => (Int16)NextInt32();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt16 NextUInt16() => (UInt16)NextInt32();
    }
}
