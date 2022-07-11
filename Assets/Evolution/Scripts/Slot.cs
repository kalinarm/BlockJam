using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Kimeria.Nyx;

namespace Evo
{
    [System.Serializable]
    public class CharacterEvent : UnityEvent<Entity>
    {
    }
    [System.Serializable]
    public class CharacterSlotEvent : UnityEvent<Entity, Slot>
    {
    }

    public class Slot : MonoBehaviour
    {
        public Entity linkedCharacter;

        public CharacterEvent onCharacterAdd;
        public CharacterEvent onCharacterRemove;

        public CharacterSlotEvent onCharacterAddedToSlot;
        public CharacterSlotEvent onCharacterRemovedFromSlot;

        public bool isParenting = false;

        public Entity.TYPE type = Entity.TYPE.UNDEFINED;

        public GameObject objWhenSomethingAttached;


        protected virtual void Start()
        {
            Refresh();
        }
        public bool IsCompatible(Entity entity)
        {
            if (type == Entity.TYPE.UNDEFINED) return true;
            return type == entity.type;
        }

        public virtual bool HasEntity()
        {
            return linkedCharacter != null;
        }
        public virtual bool HasFreeSlot()
        {
            return !HasEntity();
        }

        public virtual void AttachCharacter(Entity c)
        {
            linkedCharacter = c;
            if (isParenting) linkedCharacter.transform.SetParent(transform, false);
            linkedCharacter.transform.position = transform.position;
            linkedCharacter.slotAttached = this;
            onCharacterAdd.Invoke(c);
            onCharacterAddedToSlot.Invoke(c, this);

            Refresh();
        }

        public void DetachCharacter()
        {
            if (linkedCharacter == null) return;
            var c = linkedCharacter;
            linkedCharacter = null;
            c.slotAttached = null;
            onCharacterRemove.Invoke(c);
            onCharacterRemovedFromSlot.Invoke(c, this);

            Refresh();
        }

        void Refresh()
        {
            if (objWhenSomethingAttached == null) return;
            objWhenSomethingAttached.SetActive(linkedCharacter != null);
        }
    }
}

