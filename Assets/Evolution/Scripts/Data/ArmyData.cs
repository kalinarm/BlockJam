using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    [System.Serializable]
    public class EntitySpecification
    {
        //public GeneticCode genetic;
        public string dna = "";

        public EntitySpecification()
        {

        }
        public EntitySpecification(GeneticEntity c)
        {
            SetFromEntity(c);
        }

        public bool IsDefined()
        {
            return dna != null && dna.Length>0;
        }
        public void SetFromEntity(GeneticEntity c)
        {
            if (c == null) return;
            dna = c.genetic.AsHexString;
        }
        public void ApplyToEntity(GeneticEntity c)
        {
            if (c == null) return;
            c.genetic.AsHexString = dna;
        }
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
        public void FromJson(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }

    [System.Serializable]
    public class ArmyData
    {
        public List<EntitySpecification> entities = new List<EntitySpecification>();

        public ArmyData Clone()
        {
            ArmyData r = new ArmyData();
            r.entities = new List<EntitySpecification>(entities);
            return r;
        }

        public void Clear()
        {
            entities.Clear();
        }

        public void SetFromCreatureList(List<GeneticEntity> creaturesList)
        {
            entities.Clear();

            EntitySpecification ct = null;
            List<GeneticEntity> creatureInArmy = new List<GeneticEntity>(4);
            foreach (var item in creaturesList)
            {
                if (item == null)
                {
                    ct = new EntitySpecification();
                    entities.Add(ct);
                    continue;
                }
                ct = new EntitySpecification(item);
                entities.Add(ct);
            }
        }
        public void SetFromCreatureList(List<Creature> creaturesList)
        {
            entities.Clear();

            EntitySpecification ct = null;
            List<Creature> creatureInArmy = new List<Creature>(4);
            foreach (var item in creaturesList)
            {
                if (item == null)
                {
                    ct = new EntitySpecification();
                    entities.Add(ct);
                    continue;
                }
                ct = new EntitySpecification(item);
                entities.Add(ct);
            }
        }
        public void SetFromMushroomList(List<Mushroom> creaturesList)
        {
            entities.Clear();
            EntitySpecification ct = null;
            List<Mushroom> creatureInArmy = new List<Mushroom>(4);
            foreach (var item in creaturesList)
            {
                if (item == null)
                {
                    ct = new EntitySpecification();
                    entities.Add(ct);
                    continue;
                }
                ct = new EntitySpecification(item);
                entities.Add(ct);
            }
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }
        public void FromJson(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}

