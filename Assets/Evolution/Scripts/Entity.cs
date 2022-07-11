using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class Entity : MonoBehaviour
    {
        public enum TYPE
        {
            UNDEFINED = 0,
            CREATURE = 1,
            MUSHROOM = 2
        }

        public TYPE type = TYPE.CREATURE;
        public Slot slotAttached;
    }
}

