using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Kimeria.Nyx;

namespace Evo
{
    public class Interactable : MonoBehaviour
    {
        public GameEventType launchGameEvent = GameEventType.NONE;
        public UnityEvent OnActivatedCallback;

        public virtual bool CanBeActivated()
        {
            return true;
        }

        [EditorButton]
        public virtual void Activate()
        {
            OnActivatedCallback.Invoke();

            if (launchGameEvent != GameEventType.NONE)
            {
                Game.Events.Trigger(new Evt.GameEvent(launchGameEvent));
            }
        }
    }
}

