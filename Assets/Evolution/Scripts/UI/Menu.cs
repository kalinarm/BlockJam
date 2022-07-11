using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kimeria.Nyx;

namespace Evo
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] GameData gameData;
        [SerializeField] PlayerData playerData;
        [SerializeField] UnityEngine.UI.Button newGameButton;
        [SerializeField] UnityEngine.UI.Button continueButton;

        bool hasGame = false;

        public void Start()
        {
            hasGame = playerData.HasSave();
            Refresh();
        }

        private void Refresh()
        {
            continueButton.interactable = hasGame;
        }

        public void OnClickNewGame()
        {
            if (hasGame)
            {
                Kimeria.Nyx.UI.ModalPanelTMP.ShowMessage($"This will erase your current game.\nAre you sure ?", NewGameEffective, delegate { });
                return;
            }
            NewGameEffective();
        }
        public void OnClickContinueGame()
        {
            playerData.Load();
            gameData.LoadDefaultScene();
        }

        void NewGameEffective()
        {
            Game.Instance.ResetPlayerData();
            gameData.LoadDefaultScene();
        }
        public void OnCLickCreatureExplorer()
        {
            Game.Instance.LoadCreatureExplorer();
        }
    }
}

