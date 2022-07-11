using System;
using System.Collections.Generic;
using UnityEngine;


namespace Kimeria.Nyx.Tools.FSM
{
    public class StateMachine : MonoBehaviour
    {
        protected BaseState currentState;

        void Start()
        {
            currentState = GetInitialState();
            if (currentState != null)
                currentState.Enter();
        }

        protected virtual void Update()
        {
            if (currentState != null)
                currentState.UpdateLogic();
        }

        void LateUpdate()
        {
            if (currentState != null)
                currentState.UpdatePhysics();
        }

        protected virtual BaseState GetInitialState()
        {
            return null;
        }

        public void ChangeState(BaseState newState)
        {
            currentState?.Exit();

            currentState = newState;
            newState?.Enter();
        }
    }
}
