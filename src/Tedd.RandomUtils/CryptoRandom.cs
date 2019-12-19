using System;
using System.Security.Cryptography;

namespace Tedd.RandomUtils
{
    public class CryptoRandom : IDisposable
    {

        private readonly RandomNumberGenerator _rng;

        public CryptoRandom()
        {
            _rng = RandomNumberGenerator.Create();
        }

        /// <summary>
        /// Returns a non-negative random integer.
        /// </summary>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0 and less than MaxValue.</returns>
        public int Next()
        {
            return Next(0, int.MaxValue);
        }

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number to be generated. maxValue must be greater than or equal to 0.</param>
        /// <returns>A 32-bit signed integer that is greater than or equal to 0, and less than <paramref name="maxValue"/>; that is, the range of return values ordinarily includes 0 but not <paramref name="maxValue"/>. However, if <paramref name="maxValue"/> equals 0, <paramref name="maxValue"/> is returned.</returns>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="maxValue"/> is less than 0.</exception>
        public int Next(int maxValue)
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
        public int Next(int minValue, int maxValue)
        {
            if (maxValue < minValue)
                throw new ArgumentOutOfRangeException("minValue is greater than maxValue.");

            // If same return minValue
            if (maxValue == minValue)
                return minValue;

            // Do
            var result = new byte[4];
            var diff = maxValue - minValue;
            int val;
            for (; ; )
            {
                _rng.GetBytes(result);
                uint i = (uint)(
                      (result[0] << 24)
                    | (result[1] << 16)
                    | (result[2] << 8)
                    | result[3]);
                var d = (double)i / (double)UInt32.MaxValue;
                val = minValue + (int)(d * (double)diff);

                if (val < maxValue) // Unlikely that we'll get 1.0D, but a promise is a promise.
                    break;
            }
            return val;
        }

        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public void NextBytes(byte[] buffer)
        {
            _rng.GetBytes(buffer);
        }

#if NETCOREAPP3
        /// <summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        /// </summary>
        /// <param name="buffer">An array of bytes to contain random numbers.</param>
        public void NextBytes(Span<byte> buffer)
        {
            _rng.GetBytes(buffer);
        }
#endif

        /// <summary>
        /// Generates an array and fills the byte elements with random numbers.
        /// </summary>
        public byte[] GetByteArray(int size)
        {
            var buffer = new byte[size];
            _rng.GetBytes(buffer);
            return buffer;
        }

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal to 0.0, and less than 1.0.
        /// </summary>
        /// <returns>A double-precision floating point number that is greater than or equal to 0.0, and less than 1.0.</returns>
        public double NextDouble()
        {
            var result = new byte[8];
            var d = 0.0D;

            for (; ; )
            {
                _rng.GetBytes(result);
                var i = BitConverter.ToUInt64(result, 0);

                d = (double)i / (double)ulong.MaxValue;
                if (d < 1.0D) // Unlikely that we'll get 1.0D, but a promise is a promise.
                    break;
            }

            return d;
        }

        #region IDisposable
        private void ReleaseUnmanagedResources()
        {
            _rng.Dispose();
        }

        private void Dispose(bool disposing)
        {
            ReleaseUnmanagedResources();
            if (disposing)
            {
                _rng?.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CryptoRandom()
        {
            Dispose(false);
        }
        #endregion

    }
}
