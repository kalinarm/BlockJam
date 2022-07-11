using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class MachineStockCreature : Machine
    {
        public SlotStack slots;
        public PlayerData playerData;
        public BattleArmies battleArmies;

        private void Start()
        {
            LoadData();
        }

        [EditorButton]
        public override void SaveData()
        {
            Debug.Log("Save Creatures Data");
            playerData.creatures.SetFromCreatureList(CreatureManager.Creatures);
        }
        [EditorButton]
        public override void LoadData()
        {
            var creats = playerData.creatures.Clone();

            //remove creatures already in SendToBattle
            foreach (var item in battleArmies.armyPlayer.entities)
            {
                if (item == null || !item.IsDefined()) continue;
                var es = creats.entities.Find(x => x.dna == item.dna);
                creats.entities.Remove(es);
            }

            //recreate army
            List<Creature> cs = Game.Instance.CreateArmy(creats, 0);
            int index = -1;
            foreach (var item in cs)
            {
                if (++index >= slots.slots.Length)
                {
                    break;
                }
                if (item == null) continue;
                slots.slots[index].AttachCharacter(item.GetComponent<Entity>());
            }
        }


        List<Creature> SlotsToArmy(Slot[] slots)
        {
            Creature c;
            Entity entity;
            List<Creature> creatureInArmy = new List<Creature>(8);
            foreach (var item in slots)
            {
                entity = item.linkedCharacter;
                if (entity == null)
                {
                    creatureInArmy.Add(null);
                    continue;
                }
                c = entity.GetComponent<Creature>();
                creatureInArmy.Add(c);
            }
            return creatureInArmy;
        }


    }
}

