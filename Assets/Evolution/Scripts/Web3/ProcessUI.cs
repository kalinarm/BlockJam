using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Kimeria.Nyx;

namespace Evo.Web3
{
    public class ProcessUI : MonoBehaviour
    {
        Web3Process process;

        public TMP_Text step;
        public TMP_Text state;

        public void SetProcess(Web3Process process)
        {
            this.process = process;
        }
        private void Start()
        {
            Refresh();
        }

        private void Update()
        {
            Refresh();
        }

        void Refresh()
        {
            if (process == null)
            {
                step.text = string.Empty;
                state.text = string.Empty;
                return;
            }


            switch (process.CurrentState)
            {
                case Web3Process.State.None:
                    break;
                case Web3Process.State.Running:
                    step.text = process.CurrentStep;
                    state.text = process.CurrentState.ToString();
                    break;
                case Web3Process.State.Failed:
                    step.text = process.CurrentStep;
                    state.text = $"Failed ({process.Error})";
                    break;
                case Web3Process.State.Success:
                    step.text = process.CurrentStep;
                    state.text = process.CurrentState.ToString();
                    break;
                default:
                    step.text = process.CurrentStep;
                    state.text = process.CurrentState.ToString();
                    break;
            }
        }
    }
}


