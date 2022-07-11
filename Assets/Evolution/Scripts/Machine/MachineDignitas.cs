using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class MachineDignitas : Machine
    {
        public PlayerData playerData;
        public LevelData level;
        public Levels levels;
        public Web3.ProcessDownloadNFT process;
        public Web3.ProcessUI processUI;
        public LevelUi levelUI;

        public Sprite spriteNFTNull;

        int currentLevel = 0;

        protected override void Start()
        {
            base.Start();

            ConfigureLevel();
            LaunchProcess();
        }

        private void ConfigureLevel()
        {
            level = levels.GetLevel(currentLevel);
            levelUI.SetValue(level);
        }

        public override void Activate()
        {
            base.Activate();
        }
        [EditorButton]
        void LaunchProcess()
        {
            process = new Web3.ProcessDownloadNFT(this, level.url);
            process.CallbackFinished += OnProcessFinished;
            processUI.SetProcess(process);
            processUI.gameObject.SetActive(true);
            process.Start();
        }

        void OnProcessFinished(Web3.Web3Process proc)
        {
            Web3.ProcessDownloadNFT p = (Web3.ProcessDownloadNFT)proc;
            processUI.gameObject.SetActive(false);

            if (!p.IsSuccess) return;

            if (p.sprite != null)
            {
                levelUI.icon.sprite = p.sprite;
            }else
            {
                levelUI.icon.sprite = spriteNFTNull;
            }
        }

        public void IncrementCurrentLevelIndex(int i)
        {
            currentLevel += i;
            if (currentLevel < 0) currentLevel = levels.LevelsCount - 1;
            if (currentLevel >= levels.LevelsCount) currentLevel = 0;
            ConfigureLevel();
        }

        public void StartBattle()
        {
            playerData.levelDignitas = currentLevel;
            Game.Instance.StartBattle(levels);
        }
    }
}

