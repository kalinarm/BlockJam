using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Security.Cryptography;

namespace Kimeria.Nyx.Modules.Genetic
{
    public static class Extensions
    {
        public static T[] SubArray<T>(this T[] array, int offset, int length)
        {
            T[] result = new T[length];
            System.Array.Copy(array, offset, result, 0, length);
            return result;
        }

        public static int BoolArrayToInt(bool[] arr)
        {
            if (arr.Length > 31)
                return 0;
            int val = 0;
            for (int i = 0; i < arr.Length; ++i)
                if (arr[i]) val |= 1 << i;
            return val;
        }

        public static float Remap(this float value, float from1, float to1, float from2, float to2)
        {
            return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
        public static int IntPow(this int x, int pow)
        {
            int ret = 1;
            while (pow != 0)
            {
                if ((pow & 1) == 1)
                    ret *= x;
                x *= x;
                pow >>= 1;
            }
            return ret;
        }
    }
}

