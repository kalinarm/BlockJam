using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo.BT
{
    public class TaskGoToDestPos : Node
    {
        private Transform _transform;
        private float _speed;

        public TaskGoToDestPos(Transform transform, float speed)
        {
            _transform = transform;
            _speed = speed;
        }

        public override NodeState Evaluate()
        {
            object t = GetData("destination");
            if (t == null)
            {
                state = NodeState.FAILURE;
                return state;
            }
            Vector3 target = (Vector3)t;

            if (Vector3.Distance(_transform.position, target) > 0.01f)
            {
                _transform.position = Vector3.MoveTowards(
                    _transform.position, target, _speed * Time.deltaTime);
                state = NodeState.RUNNING;
                return state;

            }

            ClearData("destination");
            state = NodeState.SUCCESS;
            return state;
        }
    }
}

