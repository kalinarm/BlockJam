using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class MachineBuyGold : Machine
    {
        public Web3.ProcessUI processUI;
        Web3.Web3Process process = null;

        public float tokenCost = 10;
        public int goldReward = 10;

        bool IsProcessRunning
        {
            get => process != null && !process.IsFinished;
        }
        

        protected override void Start()
        {
            base.Start();
        }

        public override void Activate()
        {
            //base.Activate();
            fxStartActivation?.Trigger(transform.position);
            process = new Web3.ProcessBurn(this, Web3.Web3Hub.Instance.account, new System.Numerics.BigInteger(tokenCost * Mathf.Pow(10, 18)));
            process.CallbackFinished += OnProcessFinished;
            process.Start();
            processUI.SetProcess(process);
            RefreshProcess();
        }
        public override void FinishActivation()
        {
            base.FinishActivation();
        }
        public override bool CanBeActivated()
        {
            return base.CanBeActivated();
        }
        public override void RefreshView()
        {
            base.RefreshView();
        }


        void RefreshProcess()
        {
            if (process == null)
            {
                processUI.gameObject.SetActive(false);
                return;
            }
            processUI.gameObject.SetActive(true);
            processUI.SetProcess(process);
        }

        void OnProcessFinished(Web3.Web3Process p)
        {
            processUI.gameObject.SetActive(false);
            Game.Instance.WinGold(goldReward);
            fxFinishActivation?.Trigger(transform.position);
        }
    }
}

