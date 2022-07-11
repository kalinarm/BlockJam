using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class MachineStockMushroom : Machine
    {
        public SlotStack slots;
        public PlayerData playerData;

        private void Start()
        {
            LoadData();
        }

        [EditorButton]
        public override void SaveData()
        {
            ArmyData army = new ArmyData();
            List<Mushroom> creatureInArmy = SlotsToArmy(slots.slots);
            playerData.mushroom.SetFromMushroomList(creatureInArmy);
        }
        [EditorButton]
        public override void LoadData()
        {
            //recreate army
            List<Mushroom> cs = Game.Instance.CreateMushrooms(playerData.mushroom);
            int index = -1;
            foreach (var item in cs)
            {
                index++;
                if (item == null) continue;
                slots.slots[index].AttachCharacter(item.GetComponent<Entity>());
            }
        }

       
        List<Mushroom> SlotsToArmy(Slot[] slots)
        {
            Mushroom c;
            Entity entity;
            List<Mushroom> creatureInArmy = new List<Mushroom>(4);
            foreach (var item in slots)
            {
                entity = item.linkedCharacter;
                if (entity == null)
                {
                    creatureInArmy.Add(null);
                    continue;
                }
                c = entity.GetComponent<Mushroom>();
                creatureInArmy.Add(c);
            }
            return creatureInArmy;
        }

        public override bool CanBeActivated()
        {
            return true;
        }
        public override void Activate()
        {
            base.Activate();
            Game.Instance.StartBattle();
        }
    }
}

