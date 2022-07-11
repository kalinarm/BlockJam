using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kimeria.Nyx.Tools.BehaviourTree
{
    /// <summary>
    /// class for running the behaviour tree and holding the root node
    /// </summary>
    public abstract class Tree : MonoBehaviour
    {

        private Node _root = null;

        protected void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            if (_root != null)
                _root.Evaluate();
        }

        protected abstract Node SetupTree();

    }
}
