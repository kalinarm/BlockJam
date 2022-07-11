using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class FxInstantiate : IFX
    {
        public GameObject prefab;
        public Vector3 offset = Vector3.zero;
        public override void Trigger(Vector3 position, float intensity = 1)
        {
            GameObject obj = GameObject.Instantiate(prefab, position + offset, prefab.transform.rotation);
        }
    }
}

