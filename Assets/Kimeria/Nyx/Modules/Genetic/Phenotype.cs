using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using System.Security.Cryptography;

namespace Kimeria.Nyx.Modules.Genetic
{
    [CreateAssetMenu]
    public class Phenotype : ScriptableObject
    {
        public List<GeneExpression> genes = new List<GeneExpression>();

        public GeneExpression GetGene(string name)
        {
            return genes.Find(x => x.name == name);
        }
        public GeneExpression GetGene(int index)
        {
            return genes[index];
        }

        public int GetGeneAsInt(GeneticCode genetic, string name, int defaultValue = 0)
        {
            var gene = GetGene(name);
            if (gene == null) return defaultValue;
            return gene.GetAsInt(genetic);
        }
        public Color GetGeneAsColor(GeneticCode genetic, string name, Color defaultValue)
        {
            var gene = GetGene(name);
            if (gene == null) return defaultValue;
            return gene.GetAsColor(genetic, defaultValue);
        }
        public float GetGeneAsFloat(GeneticCode genetic, string name, float min, float max)
        {
            var gene = GetGene(name);
            if (gene == null) return min;
            return gene.GetAsFloat(genetic, min, max);
        }
    }
}

