using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo.BT
{
    public class CheckCanAct : BaseNode
    {
        public CheckCanAct(Creature parent) : base (parent)
        {

        }
        public override NodeState Evaluate()
        {
            return NodeState.FAILURE;
        }
    }
}

