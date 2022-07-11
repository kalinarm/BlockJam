using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class FxScale : IFX
    {
        public Transform target;
        public Vector3 fromScale = Vector3.one;
        public Vector3 toScale = Vector3.one;
        public float duration = 1f;
        public AnimationCurve anim = AnimationCurve.EaseInOut(0, 1, 1, 0);

        private void Start()
        {
            fromScale = target.localScale;
        }
        public override void Trigger(Vector3 position, float intensity = 1)
        {
            StartCoroutine(routine());
        }

        public void Set(float tNorm)
        {
            //target.localScale = Vector3.Lerp(fromScale, toScale, anim.Evaluate(tNorm));
            target.localScale = toScale * anim.Evaluate(tNorm);

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

