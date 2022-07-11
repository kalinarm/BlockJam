using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kimeria.Nyx.Tools.BehaviourTree
{
    /// <summary>
    /// repeat until a node failed, then return SUCCESS
    /// </summary>
    public class RepeatUntilFail : Node
    {
        public RepeatUntilFail() : base() { }
        public RepeatUntilFail(List<Node> children) : base(children) { }

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
                    case NodeState.RUNNING:
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                }
            }
            state = NodeState.FAILURE;
            return state;
        }
    }
}
