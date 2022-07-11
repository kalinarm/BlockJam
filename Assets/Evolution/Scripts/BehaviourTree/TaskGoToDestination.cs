using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo.BT
{
    public class TaskGoToDestination : BaseNode
    {
        private Transform _transform;

        public TaskGoToDestination(Creature c) : base(c)
        {
            _transform = c.transform;
        }

        public override NodeState Evaluate()
        {
            object t =  RootNode().GetData("destination");
            if (t == null)
            {
                state = NodeState.FAILURE;
                return state;
            }

            Vector3 dest = (Vector3) t;
            if (Vector3.Distance(_transform.position, dest) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position, dest, creature.GetSpeed() * Time.deltaTime);
            }

            state = NodeState.RUNNING;
            return state;
        }

    }
}

