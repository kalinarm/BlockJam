using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class UIWrapper : MonoBehaviour
    {
        public GameEventType launchGameEvent = GameEventType.NONE;

        public void Launch()
        {
            if (launchGameEvent != GameEventType.NONE)
            {
                Game.Events.Trigger(new Evt.GameEvent(launchGameEvent));
            }
        }
    }
}

