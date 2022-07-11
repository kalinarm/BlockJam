using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;
namespace Evo
{
    [System.Serializable]
    public class Fx : MonoBehaviour
    {
        [Header("Config")]
        public bool triggerOnStart = false;
        public bool triggerOnEnable = false;

        [Header("Refs")]
        public IFX[] fxInScene ;
        public IFxScriptableObject[] fxInData;

        public void Start()
        {
            if (triggerOnStart) Trigger(transform.position);
        }
        public void OnEnable()
        {
            if (triggerOnEnable) Trigger(transform.position);
        }

        public void Trigger()
        {
            Trigger(transform.position);
        }

        public void Trigger(float intensity)
        {
            Trigger(transform.position, intensity);
        }

        public void Trigger(Vector3 position, float intensity = 1f)
        {
            foreach (var item in fxInScene)
            {
                if (item == null) continue;
                item.Trigger(position, intensity);
            }
            foreach (var item in fxInData)
            {
                if (item == null) continue;
                item.Trigger(position, intensity);
            }
        }

        [EditorButton]
        void GetChilds()
        {
            fxInScene = gameObject.GetComponentsInChildren<IFX>();
        }
    }
}

