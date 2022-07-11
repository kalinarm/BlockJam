using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class MachineSendToBattle : Machine
    {
        public Levels levels;
        public SlotStack slots;
        public BattleArmies dataExchange;
        protected override void Start()
        {
            slots.onCharacterAdd.AddListener(OnSlotContentChanged);
            slots.onCharacterRemove.AddListener(OnSlotContentChanged);

            LoadData();
            base.Start();
        }

        [EditorButton]
        public override void SaveData()
        {
            //determine army
            ArmyData army = new ArmyData();
            List<Creature> creatureInArmy = SlotsToArmy(slots.slots);
            dataExchange.armyPlayer.SetFromCreatureList(creatureInArmy);
        }

        [EditorButton]
        public override void LoadData()
        {
            //recreate army
            List<Creature> cs = Game.Instance.CreateArmy(dataExchange.armyPlayer, 0);
            int index = -1;
            foreach (var item in cs)
            {
                index++;
                if (cs == null) continue;
                slots.slots[index].AttachCharacter(item.Entity);
            }
        }

        List<Creature> SlotsToArmy(Slot[] slots)
        {
            Creature c;
            Entity entity;
            List<Creature> creatureInArmy = new List<Creature>(4);
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

        public override bool CanBeActivated()
        {
            return slots.HasOneSlotTaken();
        }

        public override void Activate()
        {
            base.Activate();
            if (levels == null)
            {
                //go with default campaign
                Game.Instance.StartBattle();
                return;
            }
            Game.Instance.StartBattle(levels);
        }
    }
}

