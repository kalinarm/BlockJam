using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    [System.Serializable]
    public class SlotStack : Slot
    {
        public Slot[] slots;

        protected override void Start()
        {
            base.Start();
            foreach (var item in slots)
            {
                item.onCharacterAdd.AddListener(CallbackCharacterAdd);
                item.onCharacterRemove.AddListener(CallbackCharacterRemove);
            }
        }

        void CallbackCharacterAdd(Entity e)
        {
            onCharacterAdd.Invoke(e);
        }
        void CallbackCharacterRemove(Entity e)
        {
            onCharacterRemove.Invoke(e);
        }

        public int GetNextIndexAvailabe()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] != null && !slots[i].HasEntity())
                    return i;
            }
            return -1;
        }
        public Slot Get(int index)
        {
            return slots[index];
        }
        public Slot GetNextFreeSlot()
        {
            int i = GetNextIndexAvailabe();
            if (i < 0) return null;
            return slots[i];
        }

        public override bool HasFreeSlot()
        {
            return GetNextIndexAvailabe() >= 0;
        }
        public bool HasOneSlotTaken()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].HasEntity())
                    return true;
            }
            return false;
        }
    }
}

