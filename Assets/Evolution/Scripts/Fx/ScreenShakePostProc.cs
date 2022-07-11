using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    namespace Evt
    {
        public class ScreenShake : IEvent
        {
            public float intensity;
            public float duration;
            public ScreenShake(float intensity, float duration = 0.2f)
            {
                this.intensity = intensity;
                this.duration = duration;
            }
        }
    }
    public class ScreenShakePostProc : MonoBehaviour
    {
        protected Cinemachine.CinemachineBasicMultiChannelPerlin perlin;
        protected Cinemachine.CinemachineVirtualCamera vCam;

        private Coroutine shakeRoutine;

        /// <summary>
        /// On awake we grab our components
        /// </summary>
        protected virtual void Awake()
        {
            vCam = this.gameObject.GetComponent<CinemachineVirtualCamera>();
            perlin = vCam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
        public void OnEnable()
        {
            Game.Events.AddListener<Evt.ScreenShake>(OnScreenShake);
        }

        void OnScreenShake(Evt.ScreenShake evt)
        {
            Shake(evt.intensity, evt.duration);
        }


        void Shake(float intensity = 1f, float duration = 0.2f)
        {
            StartCoroutine(routineShake(intensity, duration));
        }

        IEnumerator routineShake(float intensity, float duration)
        {
            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            float fN = 0;
            for (float f = 0f; f < duration; f+= Time.deltaTime)
            {
                fN = Mathf.Clamp01(f / duration);
                perlin.m_AmplitudeGain = 1f - fN;
                yield return wait;
            }
            perlin.m_AmplitudeGain = 0f;
        }

        [EditorButton]
        void TestShake()
        {
            Shake(1f, 0.4f);
        }
    }
}

