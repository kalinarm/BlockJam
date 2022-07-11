using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Kimeria.Nyx;

namespace Evo
{
    public class PlayerDataPanel : UIGamePanel
    {
        [SerializeField] PlayerData playerData;

        [SerializeField] TMP_Text textBattleLevel;
        [SerializeField] TMP_Text textConcentrateAmount;
        [SerializeField] TMP_Text textGold;

        private void Start()
        {
            Game.Events.AddListener<Evt.PlayerDataChanged>(OnPlayerDataChanged);
        }
        private void OnEnable()
        {
            Refresh();
        }
        public void Refresh()
        {
            if (textBattleLevel!=null) textBattleLevel.text = playerData.LevelHumanReadable.ToString();
            if (textConcentrateAmount != null) textConcentrateAmount.text = playerData.concentrateAmount.ToString();
            if (textGold != null) textGold.text = playerData.Gold.ToString();
        }

        void OnPlayerDataChanged(Evt.PlayerDataChanged evt)
        {
            Refresh();
        }
    }
}

