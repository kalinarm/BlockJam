using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class FxParticlePlay : IFX
    {
        public ParticleSystem target;
        public override void Trigger(Vector3 position, float intensity = 1)
        {
            target.Play();
        }
    }
}

