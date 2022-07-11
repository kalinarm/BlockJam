using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kimeria.Nyx.Tools.BehaviourTree
{
    /// <summary>
    /// a decorator node that just invert the result of its child
    /// </summary>
    public class Inverter : Node
    {
        public Inverter() : base() { }
        public Inverter(Node children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.SUCCESS:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }

    }
}
