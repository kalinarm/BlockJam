using System;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo.BT
{
    public class FindNearestEnemy : BaseNode
    {

        public FindNearestEnemy(Creature c) : base(c)
        {

        }

        public override NodeState Evaluate()
        {
            var rootNode = RootNode();
            object t = rootNode.GetData("target");
            if (t == null)
            {
                var enemies = CreatureManager.GetEnemyNearSorted(creature, 100f);
                if (enemies.Count > 0)
                {
                    rootNode.SetData("target", enemies[0]);
                    //debug
                    creature.__target = enemies[0];
                    state = NodeState.SUCCESS;
                    return state;
                }

                state = NodeState.FAILURE;
                return state;
            }

            state = NodeState.SUCCESS;
            return state;
        }

    }
}
