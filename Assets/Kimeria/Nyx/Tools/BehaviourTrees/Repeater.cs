using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kimeria.Nyx.Tools.BehaviourTree
{
    /// <summary>
    /// a node that repeat children until failure or maxTimesRepeat reached
    /// </summary>
    public class Repeater : Node
    {
        public Repeater() : base() { }
        public Repeater(List<Node> children) : base(children) { }
        public Repeater(List<Node> children, int times = -1) : base(children) { timeToRepeat = times; }

        int repeatTime = 0;
        int timeToRepeat = -1;

        bool RepeatsIndefinitely()
        {
            return timeToRepeat <= 0;
        }

        bool TryRepeat()
        {
            repeatTime++;
            if (!RepeatsIndefinitely() && repeatTime >= timeToRepeat)
            {
                return false;
            }
            return false;
        }


        public override NodeState Evaluate()
        {

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.SUCCESS:
                        if (TryRepeat())
                        {
                            state = NodeState.RUNNING;
                            return state;
                        }
                        state = NodeState.SUCCESS;
                        return state;
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
