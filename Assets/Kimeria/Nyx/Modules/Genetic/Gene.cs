using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Security.Cryptography;

namespace Kimeria.Nyx.Modules.Genetic
{
    [System.Serializable]
    public class Gene
    {
        public static int GetBytesAsInt(GeneticCode code, int offsetByte, int lengthByte)
        {
            return code.GetAsInt(offsetByte, lengthByte);
        }
        public static int GetBitsAsInt(GeneticCode code, int offsetBits, int lengthBits)
        {
            var bitsArray = new BitArray(code.bytes);
            bool[] bits = new bool[bitsArray.Length];
            bitsArray.CopyTo(bits, 0);
            bits = bits.SubArray(offsetBits, lengthBits);
            int val = 0;
            for (int i = 0; i < bits.Length; ++i)
                if (bits[i]) val |= 1 << i;

            Debug.Log("gene[" + offsetBits + "-" + (offsetBits + lengthBits - 1) + "] = " + val);
            return val;
        }
        public static bool GetBitsAsBool(GeneticCode code, int offsetBits)
        {
            var bitsArray = new BitArray(code.bytes);
            return bitsArray[offsetBits];
        }
    }
}

