using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Kimeria.Nyx;
using TMPro;

namespace Evo
{
    public class MachineMultiplayer : Machine
    {
        public TMP_Text outputCid;
        public TMP_InputField inputCid;
        public UnityEngine.UI.Button[] buttons;
        public Evo.Web3.ProcessUI processUI;

        Web3.Web3Process currentProcess = null;

        protected override void Start()
        {
            LoadData();
            base.Start();
        }
        private void Update()
        {
            if (currentProcess == null) return;
            if (currentProcess.IsFinished)
            {
                currentProcess = null;
                foreach (var item in buttons)
                {
                    item.interactable = true;
                }
            }
        }

        [EditorButton]
        public override void SaveData()
        {
        }

        [EditorButton]
        public override void LoadData()
        {

        }

       
        public override bool CanBeActivated()
        {
            return true;
        }

        public override void Activate()
        {
            base.Activate();
        }

        void LaunchProcess()
        {
            foreach (var item in buttons)
            {
                item.interactable = false;
            }
        }
        [EditorButton]
        public void UploadArmy()
        {
            if (currentProcess != null) return;

            var playerArmy = Game.Instance.battleData.armyPlayer;
            currentProcess = new Web3.ProcessUploadArmy(this, playerArmy);
            currentProcess.CallbackSucceed = UploadArmySucceed;
            currentProcess.Start();
            processUI.SetProcess(currentProcess);
            LaunchProcess();
        }
        void UploadArmySucceed(Web3.Web3Process process)
        {
            var p = (Web3.ProcessUploadArmy) process;
            Debug.Log("Upload army succeed");
            outputCid.text = p.cidData;
        }

        [EditorButton]
        public void DownloadArmy()
        {
            if (currentProcess != null) return;

            string cid = inputCid.text;
            if (cid.Length==0)
            {
                Debug.LogWarning("Invalid Cid", this);
                return;
            }

            currentProcess = new Web3.ProcessDownloadArmy(this, cid);
            currentProcess.CallbackSucceed = DownloadArmySucceed;
            currentProcess.Start();
            processUI.SetProcess(currentProcess);
            LaunchProcess();
        }

        void DownloadArmySucceed(Web3.Web3Process process)
        {
            var p = (Web3.ProcessDownloadArmy)process;
            Debug.Log("Download army succeed");
            Game.Instance.StartBattleAgainst(p.army);
        }
    }
}

