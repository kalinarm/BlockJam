using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    [System.Serializable]
    public class Reward
    {
        public int gold = 5;
        public int mushroom = 0;
    }

    [CreateAssetMenu]
    public class LevelData : ScriptableObject
    { 
        [Header("General")]
        public string title;
        public string description;
        public string url;
        public Sprite icon;

        [Header("Army")]
        public ArmyData army;
        public Reward reward;
        public int addedRandomCreature = 1;
        public bool mutateArmy = false;
        public float mutateRate = 0.1f;

        [Header("Others")]
        public Properties bonusProperties = new Properties();
    }
}

