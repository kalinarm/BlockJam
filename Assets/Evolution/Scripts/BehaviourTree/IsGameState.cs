using System;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo.BT
{
    public class IsGameState : BaseNode
    {
        public Evo.Game.State gameState;

        public IsGameState(Evo.Game.State state) : base(null)
        {
            this.gameState = state;
        }
        public override NodeState Evaluate()
        {
            state = Evo.Game.Instance.StartState == gameState ? NodeState.SUCCESS : NodeState.FAILURE;
            return state;
        }

    }
}
