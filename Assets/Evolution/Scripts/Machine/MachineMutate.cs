using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class MachineMutate : Machine
    {
        public Slot slotInput;

        public float mutationRate = 0.05f;

        protected override void Start()
        {
            slotInput.onCharacterAdd.AddListener(OnSlotContentChanged);
            slotInput.onCharacterRemove.AddListener(OnSlotContentChanged);
            RefreshView();
        }

        public override bool CanBeActivated()
        {
            return base.CanBeActivated() && slotInput.HasEntity();
        }

        public override void FinishActivation()
        {
            Creature c = GetCreatureInSlot(slotInput);
            c.genetic.Mutate(mutationRate);
            c.Generate();

            base.FinishActivation();
        }
    }
}

