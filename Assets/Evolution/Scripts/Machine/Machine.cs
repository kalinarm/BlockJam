using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Kimeria.Nyx;
using Kimeria.Nyx.Tools;

namespace Evo
{
    public class Machine : MonoBehaviour
    {
        public enum State
        {
            DISABLED,
            ACTIVABLE,
            RUNNING
        }
        [System.Serializable]
        public class Cost
        {
            public int gold = 1;
        }

        [Header("Params")]
        public Cost cost = new Cost();
        [SerializeField] protected float timeRun = 3f;

        [Header("Refs")]
        [SerializeField] private MachineData data;
        [SerializeField] Colorizer colorizeActivate = new Colorizer();

        [Header("Variables")]
        [SerializeField] private State state = State.DISABLED;
        [SerializeField] protected bool isRunning = false;

        [Header("Fx")]
        public Fx fxStartActivation;
        public Fx fxFinishActivation;

        public UnityEvent OnStateChange;

        public State CurrentState { get => state; set => state = value; }
        public MachineData Data { get => data; }

        protected virtual void Start()
        {
            Invoke(nameof(RefreshView), 0.5f);
        }

        public virtual void SaveData()
        {

        }
        public virtual void LoadData()
        {

        }
        public virtual bool CanBeActivated()
        {
            return state == State.ACTIVABLE;
        }
        public virtual void Activate()
        {
            fxStartActivation?.Trigger(transform.position);
            state = State.RUNNING;
            RefreshView();
            Invoke(nameof(FinishActivation), timeRun);
        }
        public virtual void FinishActivation()
        {
            fxFinishActivation?.Trigger(transform.position);
            state = State.ACTIVABLE;
            RefreshView();
        }

        

        [EditorButton]
        public virtual void RefreshView()
        {
            colorizeActivate.SetColor(data.GetColor(CurrentState, CanBeActivated()));
            OnStateChange.Invoke();
        }
        public virtual void OnSlotContentChanged(Entity entity)
        {
            RefreshView();
        }
        public static Creature GetCreatureInSlot(Slot slot)
        {
            Entity e = slot.linkedCharacter;
            if (e == null) return null;
            Creature c = e.GetComponent<Creature>();
            if (c == null) return null;
            return c;
        }

        public void SetLocked(bool locked)
        {
            if (locked)
            {
                state = State.DISABLED;
                RefreshView();
                return;
            }
            if (state == State.DISABLED)
            {
                state = State.ACTIVABLE;
                RefreshView();
            }
        }
    }
}

