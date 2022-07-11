using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Security.Cryptography;
using System.Linq;

namespace Evo
{
    [CreateAssetMenu]
    public class NameGenerator : ScriptableObject
    {
        public List<string> data1 = new List<string>();
        public List<string> data2 = new List<string>();
        public List<string> data3 = new List<string>();

        public string PickName(int seed)
        {
            var rand = new System.Random(seed);
            int p1 = rand.Next(0, data1.Count);
            int p2 = rand.Next(0, data2.Count);
            int p3 = rand.Next(0, data3.Count);

            return string.Empty + data1[p1] + data2[p2] + data3[p3];
        }
    }
}

