using UnityEngine;
using System.Collections;

namespace Kimeria.Nyx
{
    public static class LayerMaskExtensions
    {
        /// <summary>
        /// Extension method to check if a layer is in a layermask
        /// </summary>
        /// <param name="mask"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static bool Contains(this LayerMask mask, int layer)
        {
            return ((mask.value & (1 << layer)) > 0);
        }

        public static bool Contains(this LayerMask mask, GameObject gameObject)
        {
            return ((mask.value & (1 << gameObject.layer)) > 0);
        }
    }
}
