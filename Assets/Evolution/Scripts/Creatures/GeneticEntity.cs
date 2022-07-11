using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    public class GeneticEntity : MonoBehaviour
    {
        public GeneticCode genetic;
        public Entity entity;

        protected bool isSetuped = false;

        public Entity Entity
        {
            get
            {
                return entity;
            }
        }

        protected virtual void Start()
        {
            SetupReference();
        }

        public virtual void SetupReference()
        {
            if (isSetuped) return;
            isSetuped = true;
        }

        [EditorButton]
        public void Randomize()
        {
            genetic.Randomize();
            Generate();
        }

        [EditorButton]
        public virtual void Generate()
        {
            
        }

       
        [EditorButton]
        public virtual void RefreshView()
        {

        }
    }
}

