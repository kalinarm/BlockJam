using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

using System.Security.Cryptography;

namespace Kimeria.Nyx.Modules.Genetic
{
    [System.Serializable]
    public class GeneticCode
    {
        public byte[] bytes;

        public int Length
        {
            get
            {
                return bytes.Length;
            }
        }
        public string AsString
        {
            get
            {
                return new string(System.Text.Encoding.UTF8.GetString(bytes).ToCharArray());
            }
            set
            {
                bytes = System.Text.Encoding.UTF8.GetBytes(value);
            }
        }
        public string AsHexString
        {
            get
            {
                StringBuilder hex = new StringBuilder(bytes.Length * 2);
                foreach (byte b in bytes)
                    hex.AppendFormat("{0:x2}", b);
                return hex.ToString();
            }
            set
            {
                int NumberChars = value.Length;
                bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2)
                    bytes[i / 2] = System.Convert.ToByte(value.Substring(i, 2), 16);
            }
        }

        public GeneticCode()
        {

        }
        public GeneticCode(string dna)
        {
            AsHexString = dna;
        }

        public GeneticCode Clone() {
            GeneticCode r = new GeneticCode();
            r.bytes = new byte[bytes.Length];
            bytes.CopyTo(r.bytes, 0);
            return r;
        }

        public void Randomize()
        {
            System.Random rnd = new System.Random(); 
            rnd.NextBytes(bytes);
        }

        public void Randomize(int length)
        {
            bytes = new byte[length];
            System.Random rnd = new System.Random();
            rnd.NextBytes(bytes);
        }

        public void RandomizeString()
        {
            System.Random rnd = new System.Random();
            for (int i = 0; i < Length; i++)
            {
                bytes[i] = System.Convert.ToByte(rnd.Next(0, 26) + 'a');
            }
        }

        public void RandomizeString(int length)
        {
            bytes = new byte[length];
            System.Random rnd = new System.Random();
            for (int i = 0; i < Length; i++)
            {
                bytes[i] = System.Convert.ToByte(rnd.Next(0, 26) + 'a');
            }
        }

        public int GetGeneIntByte(int min, int max)
        {
            int r = 0;
            for (int i = 0; i < max; i++)
            {
                r += bytes[min + i] * (255.IntPow(i));
            }
            Debug.Log("gene [" + min + "-" + max  + "] = " + r);
            return r;
            //var array = bytes.SubArray(min, max);
            //return System.BitConverter.ToInt32(array, 0);
        }

       
        public void Mutate(float mutationRate = 0.05f)
        {
            var bitsArray = new BitArray(bytes);
            for (int i = 0; i < bitsArray.Length; i++)
            {
                if (Random.Range(0f, 1f) > mutationRate) continue;
                bitsArray[i] = !bitsArray[i];
            }
            bytes = CloneBitArray(bitsArray);
        }

        public static GeneticCode Cross(GeneticCode A, GeneticCode B)
        {
            var bitsArrayA = new BitArray(A.bytes);
            var bitsArrayB = new BitArray(B.bytes);
            var bitsArrayR = new BitArray(A.bytes);
            int start = Random.Range(0, bitsArrayA.Length - 1);
            int end = Random.Range(start, bitsArrayA.Length);
            Debug.Log("cross " + start + " - " + end);
            for (int i = 0; i < bitsArrayA.Length; i++)
            {
                bitsArrayR[i] = (i > start && i < end) ? bitsArrayA[i] : bitsArrayB[i];
            }

            GeneticCode r = new GeneticCode();
            r.bytes = CloneBitArray(bitsArrayR);
            return r;
        }
        public static GeneticCode CrossCheated(GeneticCode A, GeneticCode B)
        {
            GeneticCode r = new GeneticCode();
            r.bytes = new byte[A.bytes.Length];
            for (int i = 0; i < r.bytes.Length; i++)
            {
                r.bytes[i] = (Random.Range(0f, 1f) >= 0.5f) ? A.bytes[i] : B.bytes[i];
            }
            
            return r;
        }

        public static byte[] CloneBitArray(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;
        }

        public int GetAsInt(int offsetBits, int lengthBits)
        {
            var bitsArray = new BitArray(bytes);
            bool[] bits = new bool[bitsArray.Length];
            bitsArray.CopyTo(bits, 0);
            bits = bits.SubArray(offsetBits, lengthBits);
            int val = 0;
            for (int i = 0; i < bits.Length; ++i)
                if (bits[i]) val |= 1 << i;
            return val;
        }


        public float GetAsFloat(int offsetBits, int lengthBits, float min, float max)
        {
            return Map(GetAsInt(offsetBits, lengthBits), 2.IntPow(lengthBits), min, max);
        }

        public Color GetAsColor(int offsetBits, int lengthBits, Color refColor)
        {
            float h, s, v;
            Color.RGBToHSV(refColor, out h, out s, out v);
            h = GetAsFloat(offsetBits, lengthBits, 0f, 1f);
            return Color.HSVToRGB(h, s, v);
        }

        public static float Map(int input, float inMax, float min, float max)
        {
            return ((float)input).Remap(0, inMax, min, max);
        }
    }
}

