using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Kimeria.Nyx;
using Kimeria.Nyx.Tools;

namespace Evo
{
    public class Button : Interactable
    {
        public Machine machine;
        [SerializeField] Colorizer colorizeActivate = new Colorizer();
        [SerializeField] Fx fxOnClick;

        private void Start()
        {
            if (machine != null)
            {
                machine.OnStateChange.AddListener(Refresh);
            }
        }
        public void Refresh()
        {
            if (machine == null) return;
            colorizeActivate.SetColor(machine.Data.GetColor(machine.CurrentState, CanBeActivated()));
        }
        public override bool CanBeActivated()
        {
            return machine == null || machine.CanBeActivated();
        }
        public override void Activate()
        {
            if (!CanBeActivated()) return;
            machine?.Activate();
            base.Activate();
            fxOnClick?.Trigger();
        }
    }
}

