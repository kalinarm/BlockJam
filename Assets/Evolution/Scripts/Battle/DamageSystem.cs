using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class DamageSystem
    {
        public static Battle battle
        {
            get
            {
                return Game.Instance.battle;
            }
        }
        public static int Damage(AttackData attack, Creature target, Creature source)
        {
            
            if (Random.Range(0, 100) < target.totalProperties.evasion)
            {
                //evade
                battle.fxEvade?.Trigger(target.transform.position);
                return 0;
            }

            //normal damage
            int baseDmg = attack.damage + source.totalProperties.attack;
            int dmg = Mathf.Max(baseDmg - target.totalProperties.armor, 0);
            if (dmg == 0 )
            {
                return dmg;
            }
            target.hp.Damage(dmg);
            battle.fxLooseLife?.Trigger(target.transform.position, dmg);

            //vampire
            int heal = (source.totalProperties.lifesteal * dmg) / 100;
            if( heal > 0)
            {
                source.hp.Heal(heal);
                battle.fxWinLife?.Trigger(source.transform.position, heal);
            }

            //reflect
            int dmgReflect = (target.totalProperties.damageReflect * dmg) / 100;
            if (dmgReflect > 0)
            {
                source.hp.Damage(dmgReflect);
                battle.fxLooseLife?.Trigger(source.transform.position, dmgReflect);
            }

            Game.Events.Trigger(new Evt.CreatureDamaged(target, dmg));
            return dmg;
        }
    }
}

