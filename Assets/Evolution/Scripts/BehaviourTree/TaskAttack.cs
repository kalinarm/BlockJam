using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo.BT
{
    public class TaskAttack : BaseNode
    {

        public TaskAttack(Creature c) : base(c)
        {

        }
        public override NodeState Evaluate()
        {
            object t = RootNode().GetData("enemy");
            if (t == null)
            {
                RootNode().ClearData("enemy");
                state = NodeState.FAILURE;
                return state;
            }
            Creature target = (Creature)t;
            if (target.IsDead())
            {
                RootNode().ClearData("enemy");
                state = NodeState.FAILURE;
                return state;
            }


            var abilities = creature.Abilities.FindAll(x => x.type == IAbility.TYPE.Attack).ToList();

            if (abilities.Count == 0)
            {
                RootNode().ClearData("enemy");
                state = NodeState.FAILURE;
                return state;
            }

            var abCanUse = abilities.FindAll(x => x.CanUse());
            if (abCanUse.Count == 0)
            {
                state = NodeState.RUNNING;
                return state;
            }

            var abRecover = abCanUse.FindAll(x => x.state == IAbility.STATE.Recover || x.state == IAbility.STATE.Cast);
            if (abRecover.Count > 0)
            {
                state = NodeState.RUNNING;
                return state;
            }

            abCanUse[0].Use(target, creature);

            state = NodeState.SUCCESS;
            return state;
        }
    }
}
