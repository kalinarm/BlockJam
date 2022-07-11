using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;
namespace Evo
{
    [CreateAssetMenu]
    public class PlayerData : ScriptableObject
    {
        public int level = 0;
        public int levelDignitas = 0;

        [SerializeField] int gold = 0;
        public int concentrateAmount = 0;

        public ArmyData creatures = new ArmyData();
        public ArmyData mushroom = new ArmyData();

        //public Dictionary<string, bool> locks = new Dictionary<string, bool>();
        public List<string> locks = new List<string>();

        public int Gold { get => gold; set => gold = value; }
        public int LevelHumanReadable { get => level + 1; }

        public void Reset()
        {
            level = 0;
            levelDignitas = 0;
            gold = 5;
            concentrateAmount = 0;

            creatures.Clear();
            mushroom.Clear();

            locks.Clear();

            //add a normal creature to begin
            EntitySpecification es = new EntitySpecification();
            es.dna = "d4509d9ace47c0067bad";
            creatures.entities.Add(es);
        }
        public bool CanPayCost(Machine.Cost cost)
        {
            return gold >= cost.gold;
        }
        public void RemoveCost(Machine.Cost cost)
        {
            gold -= cost.gold;
            Game.Events.Trigger(new Evt.PlayerDataChanged(this));
        }
        public void WinGold(int amount)
        {
            gold += amount;
            Game.Events.Trigger(new Evt.PlayerDataChanged(this));
        }

        public void Unlock(string id)
        {
            if (IsLockerUnlock(id)) return;
            locks.Add(id);
        }
        public bool IsLockerUnlock(string id)
        {
            return locks.Contains(id);
        }

        #region serialization

        const string keySerialisation = "playerData";

        public void Save()
        {
            PlayerPrefs.SetString(keySerialisation, JsonUtility.ToJson(this));
        }
        public void Load()
        {
            string s = PlayerPrefs.GetString(keySerialisation);
            JsonUtility.FromJsonOverwrite(s, this);
        }
        public bool HasSave()
        {
            return PlayerPrefs.HasKey(keySerialisation);
        }
        public void ClearSave()
        {
            PlayerPrefs.DeleteKey(keySerialisation);
        }
        #endregion
    }
}

