using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo.Web3.Evt
{
    public class ConnectionStatusChanged : IEvent
    {
        public bool isConnected;
        public ConnectionStatusChanged(bool connected)
        {
            isConnected = connected;
        }
    }
    public class VariableChanged : IEvent
    {
        public TokenData variable;
        public VariableChanged(TokenData variable)
        {
            this.variable = variable;
        }
    }
}

