using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class MachineTrash : Machine
    {
        public Slot slotInput;

        public int goldReward = 5;

        protected override void Start()
        {
            slotInput.onCharacterAdd.AddListener(OnSlotContentChanged);
            slotInput.onCharacterRemove.AddListener(OnSlotContentChanged);
        }

        public override bool CanBeActivated()
        {
            return base.CanBeActivated() && slotInput.HasEntity();
        }

        [EditorButton]
        public override void FinishActivation()
        {
            Creature c = GetCreatureInSlot(slotInput);
            if (c == null) return;
            GameObject.Destroy(c.gameObject);
            Game.Instance.WinGold(goldReward);
            base.FinishActivation();
        }

        public override void RefreshView()
        {
            base.RefreshView();
        }
    }
}

