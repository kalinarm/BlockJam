using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    [CreateAssetMenu]
    public class MachineData : ScriptableObject
    {
        public Color colorOff = Color.grey;
        public Color colorCondition= Color.grey;
        public Color colorEnabled = Color.green;
        public Color colorRunning = Color.yellow;
        
        public Color GetColor(Machine.State state, bool canBeActivated)
        {
            switch (state)
            {
                case Machine.State.DISABLED:
                    return colorOff;
                case Machine.State.ACTIVABLE:
                    return canBeActivated ? colorEnabled : colorCondition;
                case Machine.State.RUNNING:
                    return colorRunning;
            }
            return colorOff;
        }
    }
}

