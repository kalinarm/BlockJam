using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kimeria.Nyx.Tools.BehaviourTree
{
    /// <summary>
    /// A node that just log a message when Evaluate is called
    /// </summary>
    public class NodeLogger : Node
    {
        public NodeLogger(string message) : base() { _message = message; }

        string _message;

        public override NodeState Evaluate()
        {
            Debug.Log($"NodeLogger : {_message}");
            return NodeState.SUCCESS;
        }
    }
}
