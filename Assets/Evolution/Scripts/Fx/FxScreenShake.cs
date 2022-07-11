using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public enum IntensityValue
    {
        CUSTOM,
        TRIGGERED
    }
    public class FxScreenShake : IFX
    {

        public float duration = 0.4f;
        public override void Trigger(Vector3 position, float intensity = 1)
        {
            Game.Events.Trigger(new Evt.ScreenShake(Intensity(intensity), duration));
        }
    }
}

