using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class FxMove : IFX
    {
        public Transform target;
        public Vector3 fromPosition = Vector3.one;
        public Vector3 toPosition = Vector3.one;
        public float duration = 1f;
        public AnimationCurve anim = AnimationCurve.EaseInOut(0, 1, 1, 0);

        private void Start()
        {

        }
        public override void Trigger(Vector3 position, float intensity = 1)
        {
            StartCoroutine(routine());
        }

        public void Set(float tNorm)
        {
            target.localPosition = Vector3.Lerp(fromPosition, toPosition, anim.Evaluate(tNorm));

        }

        IEnumerator routine()
        {
            float t = 0f;
            float tN = 0f;
            for (t = 0f; t < duration; t+= Time.deltaTime)
            {
                tN = Mathf.Clamp01(t / duration);
                Set(tN);
                yield return null;
            }
            Set(1f);
        }
    }
}

