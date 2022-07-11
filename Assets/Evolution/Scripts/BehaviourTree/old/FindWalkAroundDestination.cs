using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo.BT
{
    public class FindWalkAroundDestination : Node
    {
        private Transform _transform;
        private float _distance = 10f;

        public FindWalkAroundDestination(Transform transform, float distance = 10f)
        {
            _transform = transform;
            _distance = distance;
        }
        public FindWalkAroundDestination(Node child, Transform transform, float distance = 10f)
        {
            _Attach(child);
            _transform = transform;
            _distance = distance;
        }

        public override NodeState Evaluate()
        {
            object destPos = GetData("destPos");
            if (destPos == null)
            {
                PickDestination();

                state = NodeState.SUCCESS;
                return state;
            }

            state = NodeState.SUCCESS;
            return state;

        }

        private void PickDestination()
        {
            Vector3 p = _transform.position;
            p += Vector3.right * _distance * Random.Range(-1f, 1f);
            parent.SetData("destPos", p);
        }
    }
}

