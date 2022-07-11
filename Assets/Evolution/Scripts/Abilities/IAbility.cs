using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class IAbility : MonoBehaviour
    {
        public enum TYPE
        {
            Others = 0,
            Attack = 1,
            Defense = 2,
            Supp = 3,
            Locomotion = 4
        }
        public enum STATE
        {
            Ready,
            Cast,
            Recover,
            Reload
        }

        [Header("General")]
        public string gameName;
        public TYPE type = TYPE.Attack;
        public STATE state = STATE.Reload;

        [Header("Stats")]
        public float range = 2f;
        public float castTime = 0f;
        public float recoverTime = 0.5f;
        public float reloadTime = 1f;
        public float reloadSpeedUp = 1f;

        [Header("Fx")]
        public Fx fxOnSource;
        public Fx fxOnTarget;

        float timeToFinishReload = 0f;

        private void Start()
        {
            SwitchToReloadState();
        }

        public virtual void Update()
        {
            if (timeToFinishReload > 0f)
            {
                timeToFinishReload -= Time.deltaTime;
                if (timeToFinishReload <= 0f)
                {
                    timeToFinishReload = 0f;
                    switch (state)
                    {
                        case STATE.Cast: SwitchToRecoverState(); break;
                        case STATE.Recover: SwitchToReloadState(); break;
                        case STATE.Reload: SwitchToReadyState(); break;
                        default: break;
                    }
                }
            }
        }
        public virtual bool CanUse()
        {
            return state == STATE.Ready;
        }
        public virtual void Use(Creature target, Creature source)
        {
            SwitchToCastState();
        }
        private void SwitchToReadyState()
        {
            timeToFinishReload = 0f;
            state = STATE.Ready;
        }
        private void SwitchToCastState()
        {
            timeToFinishReload = reloadTime * reloadSpeedUp;
            state = STATE.Cast;
        }
        private void SwitchToRecoverState()
        {
            timeToFinishReload = reloadTime * reloadSpeedUp;
            state = STATE.Recover;
        }
        private void SwitchToReloadState()
        {
            timeToFinishReload = reloadTime * reloadSpeedUp;
            state = STATE.Reload;
        }
    }
}

