using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class FxSound : IFX
    {
        public AudioClip sound;
        public override void Trigger(Vector3 position, float intensity = 1)
        {
            var audioSource = gameObject.GetOrCreateComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = sound;
            audioSource.Play();
        }
    }
}

