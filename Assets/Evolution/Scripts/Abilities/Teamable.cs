using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Evo
{
    public class Teamable : MonoBehaviour
    {
        public int teamIndex = 0;

        public bool IsSameTeam(Teamable other)
        {
            return teamIndex == other.teamIndex;
        }
        public bool IsSameTeam(GameObject other)
        {
            Teamable t = other.GetComponent<Teamable>();
            if (t == null) return false;
            return IsSameTeam(t);
        }
    }
}

