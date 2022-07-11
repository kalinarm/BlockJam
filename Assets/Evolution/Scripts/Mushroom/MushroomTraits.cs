using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    [System.Serializable]
    public class MushroomTraits
    {
        public Color colorHat = Color.white;
        public Color colorPoints = Color.white;
        public Color colorFeet = Color.white;
        public Color colorInside = Color.white;

        public string toxinName;
        public Color toxinColor = Color.white;

        public Color colorBackgroundA = Color.black;
        public Color colorBackgroundB = Color.black;
        public Color colorBackgroundC = Color.black;
    }
}