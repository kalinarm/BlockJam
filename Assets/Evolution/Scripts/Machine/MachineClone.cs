using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    public class MachineClone : Machine
    {
        public Slot slotInput;
        public Slot slotOutput;


        private void Start()
        {
            slotInput.onCharacterAdd.AddListener(OnSlotContentChanged);
            slotInput.onCharacterRemove.AddListener(OnSlotContentChanged);
            slotOutput.onCharacterAdd.AddListener(OnSlotContentChanged);
            slotOutput.onCharacterRemove.AddListener(OnSlotContentChanged);
        }

        public override bool CanBeActivated()
        {
            return base.CanBeActivated() && slotInput.HasEntity() && slotOutput.HasFreeSlot();
        }

        [EditorButton]
        public override void Activate()
        {
            Creature ca = GetCreatureInSlot(slotInput);
            if (ca == null) return;
            Creature r = Game.Instance.CreateCreature(ca.genetic);
            slotOutput.AttachCharacter(r.entity);
        }

        public override void RefreshView()
        {
            base.RefreshView();
        }
    }
}

