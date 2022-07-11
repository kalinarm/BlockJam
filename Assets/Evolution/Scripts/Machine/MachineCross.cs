using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    public class MachineCross : Machine
    {
        public Slot slotInputA;
        public Slot slotInputB;
        public Slot slotOutput;

        public float mutationRate = 0.05f;

        private void Start()
        {
            slotInputA.onCharacterAdd.AddListener(OnSlotContentChanged);
            slotInputA.onCharacterRemove.AddListener(OnSlotContentChanged);
            slotInputB.onCharacterAdd.AddListener(OnSlotContentChanged);
            slotInputB.onCharacterRemove.AddListener(OnSlotContentChanged);
            slotOutput.onCharacterAdd.AddListener(OnSlotContentChanged);
            slotOutput.onCharacterRemove.AddListener(OnSlotContentChanged);
        }

        public override bool CanBeActivated()
        {
            return base.CanBeActivated() && slotInputA.HasEntity() && slotInputB.HasEntity() && slotOutput.HasFreeSlot();
        }

        [EditorButton]
        public override void FinishActivation()
        {
            Creature ca = GetCreatureInSlot(slotInputA);
            Creature cb = GetCreatureInSlot(slotInputB);
            if (ca == null || cb == null) return;

            GeneticCode c = GeneticCode.CrossCheated(ca.genetic, cb.genetic);
            Creature r = Game.Instance.CreateCreature(c);
            slotOutput.AttachCharacter(r.entity);

            base.FinishActivation();
        }

        public override void RefreshView()
        {
            base.RefreshView();
        }
    }
}

