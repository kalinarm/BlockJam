using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    [CreateAssetMenu]
    public class MushroomData : ScriptableObject
    {
        public Phenotype phenotype;
        public Color refColor = Color.red;
        public Color refColorInside = Color.red;
        public Color refColorToxin = Color.red;

        public NameGenerator toxinNameGenerator;
    }
}

