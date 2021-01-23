using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tedd.RandomUtils
{
    public static class ConcurrentRandom
    {
        private static readonly SpinLock SpinLock = new SpinLock(false);
        private static readonly Random Random = new Random();
     
        #region Standard Random methods

        /// <summary>
        /// Returns a non-negative random integer.
        /// </summary>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than <see cref="Int32.MaxValue"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Next()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.Next();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than maxValue; that is, the range of return values ordinarily includes 0 but not maxValue. However, if maxValue equals 0, maxValue is returned.</returns>
        /// <exception cref="ArgumentOutOfRangeException">maxValue is less than 0.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Next(int maxValue)
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.Next(maxValue);
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }
        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue; that is, the range of return values includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.</returns>
        /// <exception cref="ArgumentOutOfRangeException">minValue is greater than maxValue.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Next(int minValue, int maxValue)
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.Next(minValue, maxValue);
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }
        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">The array to be filled with random numbers.</param>
        /// <exception cref="ArgumentNullException">buffer is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NextBytes(byte[] buffer)
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                Random.NextBytes(buffer);
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

#if NETCOREAPP3
        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">The array to be filled with random numbers.</param>
        /// <exception cref="ArgumentNullException">buffer is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NextBytes(Span<byte> buffer) => ThreadRandom.Value.NextBytes(buffer);
#endif 

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double NextDouble()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextDouble();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }
        /// <summary>
        /// Returns a random floating-point number between 0.0 and 1.0.
        /// </summary>
        /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sample()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextDouble();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        #endregion


        #region Extension methods
        /// <summary>
        /// Gets random value of true/false.
        /// </summary>
        /// <param name="trueProbability">A probability of <see langword="true"/> result (should be between 0.0 and 1.0).</param>
        /// <returns>Random true or false.</returns>1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NextBoolean(double trueProbability = 0.5D)
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextBoolean(trueProbability);
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Gets random value from between 0 and 1.
        /// </summary>
        /// <returns>Random number between 0 and 1.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextFloat();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Gets random value from inclusive SByte.MinValue to inclusive SByte.MaxValue.
        /// </summary>
        /// <returns>Random number from -128 to 127 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public static SByte NextSByte()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextSByte();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Gets random value from inclusive Byte.MinValue to inclusive Byte.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 255 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte NextByte()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextByte();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Gets random value from inclusive Int16.MinValue to inclusive Int16.MaxValue.
        /// ()
        /// </summary>
        /// <returns>Random number from -32_768 to 32_767 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 NextInt16()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextInt16();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Gets random value from inclusive UInt16.MinValue to inclusive UInt16.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 65_535 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 NextUInt16()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextUInt16();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Gets random value from inclusive Int32.MinValue to inclusive Int32.MaxValue.
        /// </summary>
        /// <returns>Random number from -2_147_483_648 to 2_147_483_647 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // Due to second param being "exclusive" the max value can never be reached
        // We solve by removing the value there and or'ing in another random value where we pick only that one bit
        public static Int32 NextInt32()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextInt32();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Gets random value from inclusive UInt32.MinValue to inclusive UInt32.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 4_294_967_295 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 NextUInt32()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextUInt32();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Gets random value from inclusive Int64.MinValue to inclusive Int64.MaxValue.
        /// </summary>
        /// <returns>Random number from -9_223_372_036_854_775_808 to 9_223_372_036_854_775_807 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 NextInt64()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextInt64();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Gets random value from inclusive UInt64.MinValue to inclusive UInt64.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 18_446_744_073_709_551_615 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 NextUInt64()
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextUInt64();
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        #region String

        // Note that .Net Core sometime after 3.1 will probably be getting these
#if HASSPAN

        /// <summary>
        /// Generates random string of the given length.
        /// </summary>
        /// <param name="allowedChars">The allowed characters for the random string.</param>
        /// <param name="length">The length of the random string.</param>
        /// <returns>Randomly generated string.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is less than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NextString(ReadOnlySpan<char> allowedChars, int length)
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextString(allowedChars, length);
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Generates random string of the given length.
        /// </summary>
        /// <param name="allowedChars">The array of allowed characters for the random string.</param>
        /// <param name="length">The length of the random string.</param>
        /// <returns>Randomly generated string.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is less than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NextString(char[] allowedChars, int length)
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextString(new ReadOnlySpan<char>(allowedChars), length);
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }

        /// <summary>
        /// Generates random string of the given length.
        /// </summary>
        /// <param name="allowedChars">The string of allowed characters for the random string.</param>
        /// <param name="length">The length of the random string.</param>
        /// <returns>Randomly generated string.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is less than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NextString(string allowedChars, int length)
        {
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return Random.NextString(allowedChars.AsSpan(), length);
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }

        }
#endif
        #endregion
        #endregion


        #region Aliases
        /// <summary>
        /// Gets random value from between 0 and 1.
        /// </summary>
        /// <returns>Random number between 0 and 1.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextSingle() => NextFloat();
        #endregion

    }
}
