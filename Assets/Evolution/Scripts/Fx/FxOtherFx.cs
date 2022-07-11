using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class FxOtherFx : IFX
    {
        public Fx other;
        public override void Trigger(Vector3 position, float intensity = 1)
        {
            other.Trigger(position, intensity);
        }
    }
}

