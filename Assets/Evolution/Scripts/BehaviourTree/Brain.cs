using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Tools.BehaviourTree;

namespace Evo
{
    public class Brain : Kimeria.Nyx.Tools.BehaviourTree.Tree
    {
        public Creature creature;
        protected override Node SetupTree()
        {
            var seqStroll = new Sequence(new List<Node>
            {
                new BT.IsGameState(Game.State.Train),
                new BT.LookForDestination(creature),
                new BT.TaskGoToDestPos(creature.transform, creature.speed)

            }) ;
            var seqAttack = new Sequence(new List<Node>
            {
                new BT.IsGameState(Game.State.Battle),
                new BT.CheckEnemyInRange(creature),
                new BT.TaskAttack(creature),
            });

            Node root = new Selector(new List<Node>
            {
                //seqStroll,
                seqAttack,
                new Inverter(new BT.FindNearestEnemy(creature)),
                new BT.TaskGoToTarget(creature)
            }) ;
            return root;
        }
    }
}

