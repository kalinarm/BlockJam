using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo.Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        class InventorySlot
        {
            public InventoryItemData item;
            public int amount;
        }

        private Dictionary<int, InventorySlot> _inventory = new Dictionary<int, InventorySlot>();

        public void AddItem(InventoryItemData item, int amount = 1)
        {
            // fill stack of same item type
            Dictionary<int, InventorySlot> currentSlots = new Dictionary<int, InventorySlot>(_inventory);
            foreach (KeyValuePair<int, InventorySlot> p in currentSlots)
            {
                InventorySlot slot = p.Value;

                if (slot.amount == slot.item.maxStackSize)
                    continue;
                if (slot.item.code == item.code)
                {
                    if (slot.amount + amount <= slot.item.maxStackSize)
                        slot.amount += amount;
                    else
                    {
                        int consumed = slot.item.maxStackSize - slot.amount;
                        slot.amount = slot.item.maxStackSize;
                        int remaining = amount - consumed;
                        foreach (int stackCount in _DistributeItems(
                            slot.item.maxStackSize,
                            remaining))
                        {
                            _inventory.Add(_GetNextSlotIndex(), new InventorySlot()
                            {
                                item = item,
                                amount = stackCount
                            });
                        }
                    }
                    amount = 0;
                }
            }

            // create new slot(s)
            if (amount > 0)
            {
                foreach (int stackCount in _DistributeItems(item.maxStackSize,amount))
                {
                    _inventory.Add(_GetNextSlotIndex(), new InventorySlot()
                    {
                        item = item,
                        amount = stackCount
                    });
                }
            }
        }

        private int _GetNextSlotIndex()
        {
            if (_inventory.Count == 0) return 0;
            List<int> occupiedIndices = new List<int>(_inventory.Keys);
            occupiedIndices.Sort();
            for (int i = 1; i < occupiedIndices.Count; i++)
            {
                if (occupiedIndices[i] - occupiedIndices[i - 1] > 1)
                {
                    return occupiedIndices[i - 1] + 1;
                }
            }
            return occupiedIndices.Count;
        }

        private List<int> _DistributeItems(int maxStackSize, int amount)
        {
            List<int> stackCounts = new List<int>();

            int nStacks = amount / maxStackSize;
            for (int i = 0; i < nStacks; i++)
                stackCounts.Add(maxStackSize);

            int remaining = amount % maxStackSize;
            if (remaining > 0)
                stackCounts.Add(remaining);

            return stackCounts;
        }
    }
}

