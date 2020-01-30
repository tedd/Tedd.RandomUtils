using System;
using System.Runtime.CompilerServices;

namespace Tedd.RandomUtils
{
    /// <summary>
    /// Lehmer random generator is over 3-4 times faster than System.Random and passes all the Dieharder tests of randomness.
    /// Random is calculated by a single multiply and bitshift operation.
    /// </summary>
    public class FastRandomStatic
    {
        /// <summary>
        /// Non-zero seed used for calculation. Changes for every calculation made.
        /// </summary>
        /// <remarks>Must never be set to zero.</remarks>
        public static UInt64 Seed = (UInt64)((Int64)Environment.TickCount | (Int64)(Environment.TickCount + 10) << 32);

        private const UInt64 LehmerConst = 0xda942042e4dd58b5;

        /// <summary>
        /// Gets random value from inclusive Int32.MinValue to inclusive Int32.MaxValue.
        /// </summary>
        /// <returns>Random number from -2_147_483_648 to 2_147_483_647 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt32 NextUInt32()
        {
            Seed *= LehmerConst;
            return (UInt32)(Seed >> 32);
        }

        /// <summary>
        /// Gets random value from inclusive UInt32.MinValue to inclusive UInt32.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 4_294_967_295 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 NextInt32()
        {
            Seed *= LehmerConst;
            return (Int32)(Seed >> 32);
        }

        /// <summary>
        /// Gets random value from inclusive Int64.MinValue to inclusive Int64.MaxValue.
        /// </summary>
        /// <returns>Random number from -9_223_372_036_854_775_808 to 9_223_372_036_854_775_807 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int64 NextInt64() => (Int64)((Int64)NextInt32() | (((Int64)NextInt32()) << 32));

        /// <summary>
        /// Gets random value from inclusive UInt64.MinValue to inclusive UInt64.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 18_446_744_073_709_551_615 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 NextUInt64() => (UInt64)((UInt64)NextUInt32() | ((UInt64)NextUInt32() << 32));

        /// <summary>
        /// Gets random value from inclusive SByte.MinValue to inclusive SByte.MaxValue.
        /// </summary>
        /// <returns>Random number from -128 to 127 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SByte NextSByte() => (SByte)NextInt32();

        /// <summary>
        /// Gets random value from inclusive Byte.MinValue to inclusive Byte.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 255 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Byte NextByte() => (Byte)NextInt32();

        /// <summary>
        /// Gets random value from inclusive Int16.MinValue to inclusive Int16.MaxValue.
        /// </summary>
        /// <returns>Random number from -32_768 to 32_767 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int16 NextInt16() => (Int16)NextInt32();

        /// <summary>
        /// Gets random value from inclusive UInt16.MinValue to inclusive UInt16.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 65_535 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt16 NextUInt16() => (UInt16)NextInt32();

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public static void NextBytes(byte[] buffer)
        {
            for (var i = 0; i < buffer.Length; i++)
            {
                Seed *= LehmerConst;
                buffer[i] = (Byte)(Seed >> 32);
            }
        }

#if NETCOREAPP3
        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public static void NextBytes(Span<byte> buffer)
        {
             for (var i = 0; i < result.Length; i++)
            {
                Seed *= LehmerConst;
                buffer[i]= (Byte)(Seed >> 32);
            }
        }
#endif

        /// <summary>
        /// Gets random value of true/false.
        /// </summary>
        /// <param name="trueProbability">A probability of <see langword="true"/> result (should be between 0.0 and 1.0).</param>
        /// <returns>Random true or false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NextBoolean(double trueProbability = 0.5D) => trueProbability >= 0.0D && trueProbability <= 1.0D ?
            NextDouble() >= 1.0D - trueProbability :
            throw new ArgumentOutOfRangeException(nameof(trueProbability));
        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        public static double NextDouble()
        {
            double d;
            do
            {
                var i = NextUInt64();
                d = (double)i / (double)ulong.MaxValue;
                // Unlikely that we'll get 1.0D, but a promise is a promise.
            } while (d >= 1.0D);

            return d;
        }


        /// <summary>
        /// Gets random value from between 0 and 1.
        /// </summary>
        /// <returns>Random number between 0 and 1.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float NextFloat() => (float)NextDouble();

        /// <summary>
        /// Returns a non-negative random integer.
        /// </summary>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than MaxValue.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Next()
        {
            return Next(0, int.MaxValue);
        }

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than <paramref name="maxValue"/>; that is, the range of return values ordinarily includes 0 but not <paramref name="maxValue"/>. However, if <paramref name="maxValue"/> equals 0, <paramref name="maxValue"/> is returned.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxValue"/> is less than 0.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Next(int maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValue is less than 0.");

            return Next(0, maxValue);
        }

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
        /// <returns>A 32-bit signed integer greater than or equal to <paramref name="minValue"/> and less than <paramref name="maxValue"/>; that is, the range of return values includes <paramref name="minValue"/> but not <paramref name="maxValue"/>. If <paramref name="minValue"/> equals <paramref name="maxValue"/>, <paramref name="minValue"/> is returned.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="minValue"/> is greater than <paramref name="maxValue"/>.</exception>
        public static int Next(int minValue, int maxValue)
        {
            if (maxValue < minValue)
                throw new ArgumentOutOfRangeException("minValue is greater than maxValue.");

            // If same return minValue
            if (maxValue == minValue)
                return minValue;

            // Do
            var diff = maxValue - minValue;
            int val;
            do
            {
                var i = NextUInt32();
                var d = (double)i / (double)UInt32.MaxValue;
                val = minValue + (int)(d * (double)diff);
                // Unlikely that we'll get 1.0D, but a promise is a promise.
            } while (val >= maxValue);
            return val;
        }

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
        public static string NextString(ReadOnlySpan<char> allowedChars, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            if (length == 0)
                return string.Empty;
            var result = new char[length];
            for (var i = 0; i < length; i++)
                result[i] = allowedChars[Next(0, allowedChars.Length)];
            return new string(result, 0, length);
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
            => NextString(new ReadOnlySpan<char>(allowedChars), length);

        /// <summary>
        /// Generates random string of the given length.
        /// </summary>
        /// <param name="allowedChars">The string of allowed characters for the random string.</param>
        /// <param name="length">The length of the random string.</param>
        /// <returns>Randomly generated string.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="length"/> is less than zero.</exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string NextString(string allowedChars, int length)
            => NextString(allowedChars.AsSpan(), length);
#endif
        #endregion

    }
}