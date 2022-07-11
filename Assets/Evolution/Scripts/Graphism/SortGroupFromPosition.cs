using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

using Kimeria.Nyx;

namespace Evo
{
    [RequireComponent(typeof(SortingGroup))]
    public class SortGroupFromPosition : MonoBehaviour
    {
        public SortingGroup group;

        public int offset = 0;
        public int multiplicator = -10;

        private void Start()
        {
            group = GetComponent<SortingGroup>();
        }

        private void Update()
        {
            Sort();
        }

        [EditorButton]
        void Sort()
        {
            group.sortingOrder = offset + (int)(multiplicator * transform.position.z);
        }
    }
}

