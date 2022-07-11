using System;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx.Tools.FSM;

namespace Kimeria.Nyx.Samples.FSM
{
    public class MovementSM : StateMachine
    {
        public float speed = 4f;
        public float jumpForce = 14f;
        public Rigidbody2D rigidbody;
        public SpriteRenderer spriteRenderer;

        /*[HideInInspector]
        public Idle idleState;
        [HideInInspector]
        public Moving movingState;
        [HideInInspector]
        public Jumping jumpingState;*/

        private void Awake()
        {
            /*idleState = new Idle(this);
            movingState = new Moving(this);
            jumpingState = new Jumping(this);*/
        }

        protected override BaseState GetInitialState()
        {
            //return idleState;
            return null;
        }
    }
}
