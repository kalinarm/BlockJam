using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo.BT
{
    public class LookForDestination : BaseNode
    {

        public LookForDestination(Creature c) : base(c)
        {

        }

        public override NodeState Evaluate()
        {
            if (creature.Entity.slotAttached != null)
            {
                RootNode().ClearData("destination");
                state = NodeState.FAILURE;
                return state;
            }
            var rootNode = RootNode();
            object t = rootNode.GetData("destination");
            if (t == null)
            {
                Vector3 dest = creature.transform.position + Vector3.right * Random.Range(-1f, 1f) * 10;
                rootNode.SetData("destination", dest);
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.SUCCESS;
            return state;
        }
    }
}
