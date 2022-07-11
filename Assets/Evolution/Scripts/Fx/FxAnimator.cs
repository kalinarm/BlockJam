using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class FxAnimator : IFX
    {
        public Animator animator;

        public string triggerName;
        public string boolName;
        public bool boolValue;
        public override void Trigger(Vector3 position, float intensity = 1)
        {
            if (!string.IsNullOrEmpty(triggerName))
            {
                animator.SetTrigger(triggerName);
            }
            if (!string.IsNullOrEmpty(boolName))
            {
                animator.SetBool(boolName, boolValue);
            }
        }
    }
}

