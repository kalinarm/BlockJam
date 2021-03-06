using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo.Inventory
{
    public enum ItemRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    public class InventoryItemData : ScriptableObject
    {
        public static Dictionary<ItemRarity, Color> ITEM_RARITY_COLORS
           = new Dictionary<ItemRarity, Color>()
           {
                { ItemRarity.Uncommon, new Color(0.1f, 1f, 0.1f, 1f) },
                { ItemRarity.Rare, new Color(0.1f, 0.1f, 1f, 1f) },
                { ItemRarity.Epic, new Color(0.6f, 0.1f, 0.6f, 1f) },
                { ItemRarity.Legendary, new Color(0.6f, 0.6f, 0.1f, 1f) },
           };

        public string code;
        public string itemName;
        public Sprite icon;
        public ItemRarity rarity;
        public int maxStackSize = 20;
    }
}

