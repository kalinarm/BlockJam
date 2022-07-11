using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo.BT
{
    public class BaseNode : Node
    {
        protected Creature creature;

        public BaseNode(Creature parent)
        {
            creature = parent;
        }
        public override NodeState Evaluate()
        {
            return NodeState.FAILURE;
        }

    }
}

