using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tedd.RandomUtils
{
    internal static class Common
    {
        public const UInt64 mask1_64 = 0b00111111_11110000_00000000_00000000__00000000_00000000_00000000_00000000;
        public const UInt64 mask2_64 = 0b00111111_11111111_11111111_11111111__11111111_11111111_11111111_11111111;
        public const UInt32 mask1_32 = 0b00111111_10000000_00000000_00000000;
        public const UInt32 mask2_32 = 0b00111111_11111111_11111111_11111111;

        public static unsafe float UInt32ToFloat(UInt32 i)
        {
            float d;
            do
            {
                // Modified from https://stackoverflow.com/a/52148190/313088
                i = (i | Common.mask1_32) & Common.mask2_32;
                d = *(float*)(&i);
                // Unlikely that we'll get 1.0D, but a promise is a promise.
            } while (d >= 2.0D);
            return d - 1;
        }

        public static unsafe double UInt64ToDouble(UInt64 i)
        {
            double d;
            do
            {
                // https://stackoverflow.com/a/52148190/313088
                i = (i | Common.mask1_64) & Common.mask2_64;
                d = *(double*)(&i);
                // Unlikely that we'll get 1.0D, but a promise is a promise.
            } while (d >= 2.0D);

            return d - 1;
        }
    }
}
