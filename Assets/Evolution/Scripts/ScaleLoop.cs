using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class ScaleLoop : MonoBehaviour
    {
        public Transform target;
        public float amplitude = 1f;
        public float frequency = 1f;

        private void Update()
        {
            target.localScale = Vector3.one * (1f + amplitude * Mathf.Sin(frequency * Time.time));
        }

    }
}

