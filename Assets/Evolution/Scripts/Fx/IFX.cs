using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{

    public class IFX : MonoBehaviour
    {
        public IntensityValue intensityMode = IntensityValue.CUSTOM;
        public float customIntensity = 1f;

        public float Intensity(float intFromTrigger)
        {
            if (intensityMode == IntensityValue.CUSTOM) return customIntensity;
            return intFromTrigger;
        }

        public virtual void Trigger(Vector3 position, float intensity = 1f)
        {

        }
    }
}

