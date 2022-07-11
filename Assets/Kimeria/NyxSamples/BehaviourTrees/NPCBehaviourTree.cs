﻿using System;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx.Tools.BehaviourTree;

namespace Kimeria.Nyx.Samples.BehaviourTrees
{
    class NPCBehaviourTree : Nyx.Tools.BehaviourTree.Tree
    {
        public UnityEngine.Transform[] waypoints;

        public static float speed = 2f;
        public static float fovRange = 6f;
        public static float attackRange = 1f;

        protected override Node SetupTree()
        {
            Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });

            return root;
        }
    }
}
