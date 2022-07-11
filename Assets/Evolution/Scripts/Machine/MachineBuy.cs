using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class MachineBuy : Machine
    {
        public Slot slotInput;

        protected override void Start()
        {
            slotInput.onCharacterAdd.AddListener(OnSlotContentChanged);
            slotInput.onCharacterRemove.AddListener(OnSlotContentChanged);
            base.Start();
        }
        public override bool CanBeActivated()
        {
            return base.CanBeActivated() && slotInput.HasFreeSlot();
        }

        [EditorButton]
        public override void FinishActivation()
        {
            Creature c = Game.Instance.CreateRandomCreature();
            slotInput.AttachCharacter(c.Entity);
            base.FinishActivation();
        }

        public override void RefreshView()
        {
            base.RefreshView();
        }
    }
}

