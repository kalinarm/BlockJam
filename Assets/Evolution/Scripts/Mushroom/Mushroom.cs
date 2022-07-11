using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    public class Mushroom : GeneticEntity
    {
        public MushroomTraits traits = new MushroomTraits();

        [SerializeField] MushroomData data;
        [SerializeField] MushroomView view;

        [EditorButton]
        public override void Generate()
        {
            Phenotype p = data.phenotype;
            if (p == null)
            {
                Debug.LogError("No phenotype defined", this);
                return;
            }
            int indexGene = 0;

            traits.colorHat = p.GetGene(indexGene++).GetAsColor(genetic, data.refColor);
            traits.colorFeet = p.GetGene(indexGene++).GetAsColor(genetic, data.refColor);
            traits.colorPoints = p.GetGene(indexGene++).GetAsColor(genetic, data.refColor);
            traits.colorInside = p.GetGene(indexGene++).GetAsColor(genetic, data.refColorInside);

            traits.colorBackgroundA = p.GetGene(indexGene++).GetAsColor(genetic, data.refColor);
            traits.colorBackgroundB = p.GetGene(indexGene++).GetAsColor(genetic, data.refColor);
            traits.colorBackgroundC = p.GetGene(indexGene++).GetAsColor(genetic, data.refColor);

            if (data.toxinNameGenerator != null)
            {
                var geneToxin = p.GetGene(indexGene++);
                traits.toxinName = data.toxinNameGenerator.PickName(geneToxin.GetAsInt(genetic));
                traits.toxinColor = geneToxin.GetAsColor(genetic, data.refColorToxin);
            }

            RefreshView();
        }

        [EditorButton]
        public override void RefreshView()
        {
            view.Refresh(traits, data);
        }
    }
}

