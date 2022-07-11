using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Security.Cryptography;

namespace Kimeria.Nyx.Modules.Genetic
{
    [System.Serializable]
    public class GeneExpression
    {
        public string name;
        public int bitOffset;
        public int bitLength;

        public GeneExpression() { }

        public GeneExpression(int offset, int length = 1)
        {
            bitOffset = offset;
            bitLength = length;
        }

        public GeneExpression(string name, int offset, int length = 1)
        {
            this.name = name;
            bitOffset = offset;
            bitLength = length;
        }
        public int GetAsInt(GeneticCode genetic)
        {
            return genetic.GetAsInt(bitOffset, bitLength);
        }
        public bool GetAsBool(GeneticCode genetic)
        {
            return genetic.GetAsInt(bitOffset, 1 )> 0;
        }
        public float GetAsFloat(GeneticCode genetic, float min, float max)
        {
            return genetic.GetAsFloat(bitOffset, bitLength, min, max);
        }

        public Color GetAsColor(GeneticCode genetic, Color refColor)
        {
            return genetic.GetAsColor(bitOffset, bitLength, refColor);
        }
    }
}

