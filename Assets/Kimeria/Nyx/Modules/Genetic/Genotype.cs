using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Security.Cryptography;

namespace Kimeria.Nyx.Modules.Genetic
{
    [CreateAssetMenu]
    public class Genotype : ScriptableObject
    {
        public GeneticCode code;

        public void Randomize()
        {
            code.Randomize();
        }

        public int GetBytesAsInt(int offset, int length)
        {
            return Gene.GetBytesAsInt(code, offset, length);
        }
        public int GetBitsAsInt(int offset, int length)
        {
            return Gene.GetBitsAsInt(code, offset, length);
        }
        public bool GetBitsAsBool(int offset)
        {
            return Gene.GetBitsAsBool(code, offset);
        }
    }
}

