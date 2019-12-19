using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Tedd.RandomUtils
{
    public class CryptoRandom : IDisposable
    {
        private ArrayPool<byte> _arrayPool = ArrayPool<byte>.Shared;
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
            var result = _arrayPool.Rent(4);
            var diff = maxValue - minValue;
            int val;
            for (; ; )
            {
                _rng.GetBytes(result);
                uint i = (uint)(
                      (result[0] << 8 * 3)
                    | (result[1] << 8 * 2)
                    | (result[2] << 8 * 1)
                    | result[3]);
                var d = (double)i / (double)UInt32.MaxValue;
                val = minValue + (int)(d * (double)diff);

                if (val < maxValue) // Unlikely that we'll get 1.0D, but a promise is a promise.
                    break;
            }
            _arrayPool.Return(result);
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
            var result = _arrayPool.Rent(8);
            var d = 0.0D;

            for (; ; )
            {
                _rng.GetBytes(result);
                var i = BitConverter.ToUInt64(result, 0);

                d = (double)i / (double)ulong.MaxValue;
                if (d < 1.0D) // Unlikely that we'll get 1.0D, but a promise is a promise.
                    break;
            }

            _arrayPool.Return(result);
            return d;
        }


        /// <summary>
        /// Gets random value from inclusive SByte.MinValue to inclusive SByte.MaxValue.
        /// </summary>
        /// <returns>Random number from -128 to 127 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public SByte NextSByte() => (SByte)NextByte();

        /// <summary>
        /// Gets random value from inclusive Byte.MinValue to inclusive Byte.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 255 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Byte NextByte()
        {
            var buffer = _arrayPool.Rent(sizeof(Byte));
            _rng.GetBytes(buffer);
            var result = (Byte)buffer[0];
            _arrayPool.Return(buffer);
            return result;
        }

        /// <summary>
        /// Gets random value from inclusive Int16.MinValue to inclusive Int16.MaxValue.
        /// ()
        /// </summary>
        /// <returns>Random number from -32_768 to 32_767 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int16 NextInt16() => (Int16)NextUInt16();

        /// <summary>
        /// Gets random value from inclusive UInt16.MinValue to inclusive UInt16.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 65_535 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt16 NextUInt16()
        {
            var buffer = _arrayPool.Rent(sizeof(UInt16));
            _rng.GetBytes(buffer);
            var result = (UInt16)((UInt16)buffer[0]
                               | ((UInt16)buffer[1] << 8));
            _arrayPool.Return(buffer);
            return result;
        }

        /// <summary>
        /// Gets random value from inclusive Int32.MinValue to inclusive Int32.MaxValue.
        /// </summary>
        /// <returns>Random number from -2_147_483_648 to 2_147_483_647 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int32 NextInt32() => (Int32)NextUInt32();

        /// <summary>
        /// Gets random value from inclusive UInt32.MinValue to inclusive UInt32.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 4_294_967_295 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt32 NextUInt32()
        {
            var buffer = _arrayPool.Rent(sizeof(UInt32));
            _rng.GetBytes(buffer);
            var result = (UInt32)((UInt32)buffer[0]
                               | ((UInt32)buffer[1] << 8 * 1)
                               | ((UInt32)buffer[2] << 8 * 2)
                               | ((UInt32)buffer[3] << 8 * 3)
                               );
            _arrayPool.Return(buffer);
            return result;
        }

        /// <summary>
        /// Gets random value from inclusive Int64.MinValue to inclusive Int64.MaxValue.
        /// </summary>
        /// <returns>Random number from -9_223_372_036_854_775_808 to 9_223_372_036_854_775_807 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Int64 NextInt64() => (Int64)NextUInt64();

        /// <summary>
        /// Gets random value from inclusive UInt64.MinValue to inclusive UInt64.MaxValue.
        /// </summary>
        /// <returns>Random number from 0 to 18_446_744_073_709_551_615 inclusive.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public UInt64 NextUInt64()
        {
            var buffer = _arrayPool.Rent(sizeof(UInt64));
            _rng.GetBytes(buffer);
            var result = (UInt64)((UInt64)buffer[0]
                               | ((UInt64)buffer[1] << 8 * 1)
                               | ((UInt64)buffer[2] << 8 * 2)
                               | ((UInt64)buffer[3] << 8 * 3)

                               | ((UInt64)buffer[4] << 8 * 4)
                               | ((UInt64)buffer[5] << 8 * 5)
                               | ((UInt64)buffer[6] << 8 * 6)
                               | ((UInt64)buffer[7] << 8 * 7)
                               );
            _arrayPool.Return(buffer);
            return result;
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
