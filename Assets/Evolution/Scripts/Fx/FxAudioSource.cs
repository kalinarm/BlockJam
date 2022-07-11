using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class FxAudioSource : IFX
    {
        public AudioSource audioSource;

        public bool intensityAsPitch = false;
        public Vector2 pitchRange = Vector2.right;

        private void Start()
        {
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        public override void Trigger(Vector3 position, float intensity = 1)
        {
            if (intensityAsPitch)
            {
                audioSource.pitch =  (Mathf.Clamp01(intensity) - pitchRange.x) / (pitchRange.y - pitchRange.x);
            }
            audioSource.Play();
        }
    }
}

