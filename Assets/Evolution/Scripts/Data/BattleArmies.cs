using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

using Kimeria.Nyx;

namespace Evo
{
    [CreateAssetMenu]
    public class BattleArmies : ScriptableObject
    {
        public Levels levels;
        public ArmyData armyPlayer = new ArmyData();
        public LevelData level;

        public Levels.Campaign Campaign
        {
            get => levels.campaign;
        }

        public void Reset()
        {
            armyPlayer.Clear();
        }
    }
}

