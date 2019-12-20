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
        private static readonly ThreadLocal<Random> ThreadRandom = new ThreadLocal<Random>(CreateRandom);

        private static Random CreateRandom()
        {
            // Default seed is time. If we simply created all Random objects on first access with default seed then many threads accessing random object at the same time would end up with the same sequence of randomness.
            // Therefore we need to randomize the seed. To do that we have another static Random, and we lock to access it. SpinLock is faster than lock().
            var l = false;
            try
            {
                SpinLock.Enter(ref l);
                return new Random(Random.NextInt32());
            }
            finally
            {
                if (l)
                    SpinLock.Exit();
            }
        }

        /// <summary>
        /// Gets the System.Random object for current thread.
        /// </summary>
        /// <remarks>Use of this object is not thread safe. Use only within current thread!</remarks>
        /// <returns>Random object associated with current thread.</returns>
        public static Random GetUnsafeInstance() => ThreadRandom.Value;

        #region Standard Random methods
        /// <summary>
        /// Returns a non-negative random integer.
        /// </summary>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than <see cref="Int32.MaxValue"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Next() => ThreadRandom.Value.Next();
        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than maxValue; that is, the range of return values ordinarily includes 0 but not maxValue. However, if maxValue equals 0, maxValue is returned.</returns>
        /// <exception cref="ArgumentOutOfRangeException">maxValue is less than 0.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Next(int maxValue) => ThreadRandom.Value.Next(maxValue);
        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        /// <returns>A 32-bit signed integer greater than or equal to minValue and less than maxValue; that is, the range of return values includes minValue but not maxValue. If minValue equals maxValue, minValue is returned.</returns>
        /// <exception cref="ArgumentOutOfRangeException">minValue is greater than maxValue.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Next(int minValue, int maxValue) => ThreadRandom.Value.Next(minValue, maxValue);
        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">The array to be filled with random numbers.</param>
        /// <exception cref="ArgumentNullException">buffer is null.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void NextBytes(byte[] buffer) => ThreadRandom.Value.NextBytes(buffer);

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
        public static Double NextDouble() => ThreadRandom.Value.NextDouble();
        /// <summary>
        /// Returns a random floating-point number between 0.0 and 1.0.
        /// </summary>
        /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Double Sample() => ThreadRandom.Value.NextDouble();

        #endregion


        #region Extension methods
        /// <summary>
        /// Gets random value of true/false.
        /// </summary>
        /// <param name="trueProbability">A probability of <see langword="true"/> result (should be between 0.0 and 1.0).</param>
        /// <returns>Random true or false.</returns>1
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NextBoolean(double trueProbability = 0.5D) => ThreadRandom.Value.NextBoolean(trueProbability);

        /// <summary>
        /// Gets random value from between 0 and 1.
        /// </summary>
        /// <returns>Random number between 0 and 1.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat() => ThreadRandom.Value.NextFloat();

        /// <summary>
        /// Gets random value from inclusive SByte.MinValue to inclusive SByte.MaxValue.
        /// </summary>
        /// <returns>Random number from -128 to 127 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public static SByte NextSByte() => ThreadRandom.Value.NextSByte();

        /// <summary>
        /// Gets random value from inclusive Byte.MinValue to inclusive Byte.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 255 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte NextByte() => ThreadRandom.Value.NextByte();

        /// <summary>
        /// Gets random value from inclusive Int16.MinValue to inclusive Int16.MaxValue.
        /// ()
        /// </summary>
        /// <returns>Random number from -32_768 to 32_767 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 NextInt16() => ThreadRandom.Value.NextInt16();

        /// <summary>
        /// Gets random value from inclusive UInt16.MinValue to inclusive UInt16.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 65_535 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 NextUInt16() => ThreadRandom.Value.NextUInt16();

        /// <summary>
        /// Gets random value from inclusive Int32.MinValue to inclusive Int32.MaxValue.
        /// </summary>
        /// <returns>Random number from -2_147_483_648 to 2_147_483_647 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        // Due to second param being "exclusive" the max value can never be reached
        // We solve by removing the value there and or'ing in another random value where we pick only that one bit
        public static Int32 NextInt32() => ThreadRandom.Value.NextInt32();

        /// <summary>
        /// Gets random value from inclusive UInt32.MinValue to inclusive UInt32.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 4_294_967_295 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 NextUInt32() => ThreadRandom.Value.NextUInt32();

        /// <summary>
        /// Gets random value from inclusive Int64.MinValue to inclusive Int64.MaxValue.
        /// </summary>
        /// <returns>Random number from -9_223_372_036_854_775_808 to 9_223_372_036_854_775_807 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 NextInt64() => ThreadRandom.Value.NextInt64();

        /// <summary>
        /// Gets random value from inclusive UInt64.MinValue to inclusive UInt64.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 18_446_744_073_709_551_615 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 NextUInt64() => ThreadRandom.Value.NextUInt64();

        #region String

        // Note that .Net Core sometime after 3.1 will probably be getting these
#if NET461 || NETSTANDARD || NETCOREAPP2_1 || NETCOREAPP3 || NETCOREAPP3_0 || NETCOREAPP3_1

        /// <summary>
        /// Generates random string of the given length.
        /// </summary>
        /// <param name="allowedChars">The allowed characters for the random string.</param>
        /// <param name="length">The length of the random string.</param>
        /// <returns>Randomly generated string.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is less than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NextString(ReadOnlySpan<char> allowedChars, int length)
            => ThreadRandom.Value.NextString(allowedChars, length);

        /// <summary>
        /// Generates random string of the given length.
        /// </summary>
        /// <param name="allowedChars">The array of allowed characters for the random string.</param>
        /// <param name="length">The length of the random string.</param>
        /// <returns>Randomly generated string.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is less than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NextString(char[] allowedChars, int length)
            => ThreadRandom.Value.NextString(new ReadOnlySpan<char>(allowedChars), length);

        /// <summary>
        /// Generates random string of the given length.
        /// </summary>
        /// <param name="allowedChars">The string of allowed characters for the random string.</param>
        /// <param name="length">The length of the random string.</param>
        /// <returns>Randomly generated string.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is less than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NextString(string allowedChars, int length)
            => ThreadRandom.Value.NextString(allowedChars.AsSpan(), length);
#endif
        #endregion
    }

    #endregion

}
