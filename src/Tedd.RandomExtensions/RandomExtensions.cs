using System;
using System.Runtime.CompilerServices;

namespace Tedd
{
    public static class RandomExtensions
    {
        /// <summary>
        /// Gets random value from between 0 and 1.
        /// </summary>
        /// <returns>Random number between 0 and 1.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat(this Random random) => (float)(random.Next() * (1.0f / Int32.MaxValue));

        /// <summary>
        /// Gets random value from inclusive SByte.MinValue to inclusive SByte.MaxValue.
        /// </summary>
        /// <returns>Random number from -128 to 127 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte NextSByte(this Random random) => (SByte)random.Next(SByte.MinValue, SByte.MaxValue + 1);
        /// <summary>
        /// Gets random value from inclusive Byte.MinValue to inclusive Byte.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 255 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte NextByte(this Random random) => (Byte)random.Next(Byte.MinValue, Byte.MaxValue + 1);
        /// <summary>
        /// Gets random value from inclusive Int16.MinValue to inclusive Int16.MaxValue.
        /// ()
        /// </summary>
        /// <returns>Random number from -32_768 to 32_767 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 NextInt16(this Random random) => (Int16)random.Next(Int16.MinValue, Int16.MaxValue + 1);
        /// <summary>
        /// Gets random value from inclusive UInt16.MinValue to inclusive UInt16.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 65_535 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 NextUInt16(this Random random) => (UInt16)random.Next(UInt16.MinValue, UInt16.MaxValue + 1);
        /// <summary>
        /// Gets random value from inclusive Int32.MinValue to inclusive Int32.MaxValue.
        /// </summary>
        /// <returns>Random number from -2_147_483_648 to 2_147_483_647 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // Due to second param being "exclusive" the max value can never be reached
        // We solve this by removing the value there and or'ing in another random value where we pick only that one bit
        public static Int32 NextInt32(this Random random) => (Int32)((random.Next(Int32.MinValue, Int32.MaxValue) & ~1) | (random.Next() & 1));
        /// <summary>
        /// Gets random value from inclusive UInt32.MinValue to inclusive UInt32.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 4_294_967_295 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 NextUInt32(this Random random) => (UInt32)((random.Next(Int32.MinValue, Int32.MaxValue) & ~1) | (random.Next() & 1));
        /// <summary>
        /// Gets random value from inclusive Int64.MinValue to inclusive Int64.MaxValue.
        /// </summary>
        /// <returns>Random number from -9_223_372_036_854_775_808 to 9_223_372_036_854_775_807 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 NextInt64(this Random random) => ((((Int64)random.Next(Int32.MinValue, Int32.MaxValue) & ~1) << 31) | (((Int64)random.Next(Int32.MinValue, Int32.MaxValue) & ~1) << 2) | (Int64)(random.Next() & 1));
        /// <summary>
        /// Gets random value from inclusive UInt64.MinValue to inclusive UInt64.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 18_446_744_073_709_551_615 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 NextUInt64(this Random random) => (UInt64)((((Int64)random.Next(Int32.MinValue, Int32.MaxValue) & ~1) << 31) | (((Int64)random.Next(Int32.MinValue, Int32.MaxValue) & ~1) << 2) | (Int64)(random.Next() & 1));
    }
}
