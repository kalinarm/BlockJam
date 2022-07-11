using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    [CreateAssetMenu]
    public class Levels : ScriptableObject
    {
        public enum Campaign
        {
            Default = 0,
            Dignitas = 1,
            Multiplayer =2
        }

        public Campaign campaign = Campaign.Default;

        [SerializeField] List<LevelData> levels = new List<LevelData>();

        public int LevelsCount { get => levels.Count; }

        public LevelData GetLevel(int index)
        {
            return levels[index % levels.Count];
        }
    }
}

