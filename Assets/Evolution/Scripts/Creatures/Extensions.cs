using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    public static class Extensions
    {
        public static T PickRandom<T>(this T[] array, int index) where T : struct
        {
            //if (array.Length == 0) return null;
            return array[Random.Range(0, array.Length)];
        }
        public static T PickRandom<T>(this List<T> array, int index) where T : struct
        {
            //if (array.Length == 0) return null;
            return array[Random.Range(0, array.Count)];
        }
    }
}

