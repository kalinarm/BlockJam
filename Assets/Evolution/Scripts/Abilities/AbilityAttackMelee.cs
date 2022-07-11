using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    [System.Serializable]
    public class AttackData
    {
        public int damage = 10;
        public int damageVariability = 0;
        public float damageBooster = 1f;
        public int Damage
        {
            get
            {
                return(int) ((damage + damageVariability) * damageBooster);
            }
        }
    }

    public class AbilityAttackMelee : IAbility
    {
        [Header("Specific")]
        public AttackData attackData = new AttackData();

        public override void Use(Creature target, Creature source)
        {
            if (target == null) return;

            int dmg = DamageSystem.Damage(attackData, target, source);
            if (dmg > 0)
            {
                fxOnTarget?.Trigger(target.transform.position, dmg);
            }
            base.Use(target, source);
        }
    }
}

