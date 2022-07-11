using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    public class Creature : GeneticEntity
    {
        public CreatureTraits traits = new CreatureTraits();
        public Properties totalProperties = new Properties();
        public Teamable team;
        public HP hp;
        public Transform childs;

        [SerializeField] CreatureData data;
        [SerializeField] CreatureView view;

        [SerializeField] List<IAbility> abilities = new List<IAbility>();

        public float speed = 2f;
        public int teamIndex = 0;

        [Header("Fx")]
        public Fx fxOnDie;

        [Header("Debug")]
        public Creature __enemy;
        public Creature __target;

        public List<IAbility> Abilities { get => abilities; }

        public override void SetupReference()
        {
            if (isSetuped) return;
            team = gameObject.GetOrCreateComponent<Teamable>();
            hp = gameObject.GetOrCreateComponent<HP>();
            entity = gameObject.GetOrCreateComponent<Entity>();

            team.teamIndex = teamIndex;
            CreatureManager.RegisterCreature(this);

            hp.onDamageEvent.AddListener(OnDamaged);
            hp.onDieEvent.AddListener(OnDie);

            isSetuped = true;
        }

        private void OnDestroy()
        {
            CreatureManager.UnregisterCreature(this);
        }

        void OnDamaged(float damage)
        {

        }
        void OnDie()
        {
            fxOnDie?.Trigger(transform.position);
            GameObject.Destroy(gameObject);
        }


        [EditorButton]
        public override void Generate()
        {
            Phenotype p = data.phenotype;
            if (p == null)
            {
                Debug.LogError("No phenotype defined", this);
                return;
            }

            traits.bodyShape = p.GetGeneAsInt(genetic, "body");
            traits.eyeShape = p.GetGeneAsInt(genetic, "eye");
            traits.earsShape = p.GetGeneAsInt(genetic, "ear");
            traits.tieShape = p.GetGeneAsInt(genetic, "tie");
            traits.handShape = p.GetGeneAsInt(genetic, "hand");
            traits.mouthShape = p.GetGeneAsInt(genetic, "mouth");

            traits.colorAll = p.GetGeneAsColor(genetic, "color", data.refColor);
            traits.colorEye = p.GetGeneAsColor(genetic, "colorEye", data.refColor);


            RefreshView();

            ComputeTotalProperties();

            abilities.Clear();
            abilities = view.GetComponentsInChildren<IAbility>().ToList();
            /*if( abilities.Count == 0)
            {
                //add simple attack if creature has no ability
                AbilityAttackMelee ab = gameObject.AddComponent<AbilityAttackMelee>();
                ab.gameName = "Simple Punch";
                ab.attackData.damage = 2;
                ab.reloadTime = 1.5f;
            }*/
        }

        public static GameObject Pick(GameObject[] array, int index)
        {
            if (array.Length == 0) return null;
            return array[index % array.Length];
        }

        [EditorButton]
        public override void RefreshView()
        {
            view.Refresh(traits, data);
        }

        [EditorButton]
        void ComputeTotalProperties()
        {
            totalProperties = Properties.ComputeChildProperties(gameObject);
            ApplyPropertiesEffect();
        }

        public void ApplyBonusProperties(Properties p)
        {
            totalProperties = totalProperties + p;
            ApplyPropertiesEffect();
        }

        void ApplyPropertiesEffect()
        {
            hp.SetMaxHp(100 + totalProperties.force * 20);
            foreach (var item in abilities)
            {
                item.reloadSpeedUp = 1f - 0.1f * totalProperties.agility;
            }
        }

        public bool IsSameTeam(Creature other)
        {
            return team.IsSameTeam(other.team);
        }

        #region helpers
        public bool CanAttack()
        {
            return GetAbility(IAbility.TYPE.Attack) != null;
        }
        public float GetAttackRange()
        {
            var ab = GetAbility(IAbility.TYPE.Attack);
            if (ab == null) return Mathf.Infinity;
            return ab.range;
        }
        public float GetSpeed()
        {
            return speed;
        }
        public bool IsDead()
        {
            return hp.IsDead;
        }
        public IAbility GetAbility(IAbility.TYPE type)
        {
            return abilities.Find(x => x.type == type);
        }
        public IAbility GetAbilityAvailable(IAbility.TYPE type)
        {
            return abilities.Find(x => x.CanUse() && x.type == type);
        }
        #endregion
    }
}

