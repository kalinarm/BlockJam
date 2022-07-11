using System;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo.BT
{
    public class CheckEnemyInRange : BaseNode
    {

        public CheckEnemyInRange(Creature c) : base(c)
        {

        }

        public override NodeState Evaluate()
        {
            var root = RootNode();

            object t = root.GetData("enemy");
            if (t == null || ((Creature) t).IsDead())
            {
                var enemies = CreatureManager.GetEnemyNearSorted(creature, creature.GetAttackRange());
                if (enemies.Count > 0)
                {
                    root.SetData("enemy", enemies[0]);
                    //debug
                    creature.__enemy = enemies[0];
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
