using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.Modules.Genetic;

namespace Evo
{
    [System.Serializable]
    public class Properties
    {
        public int force = 0;
        public int agility = 0;
        public int attack = 0;

        public int armor = 0;
        public int damageReflect = 0;
        public int lifesteal = 0;
        public int evasion = 0;

        public int bravery = 0;
        public int temperature = 0;

        public static Properties operator +(Properties a, Properties b)
        {
            Properties c = new Properties();
            c.force = a.force + b.force;
            c.agility = a.agility + b.agility;
            c.attack = a.attack + b.attack;
            c.armor = a.armor + b.armor;
            c.damageReflect = a.damageReflect + b.damageReflect;
            c.lifesteal = a.lifesteal + b.lifesteal;
            c.evasion = a.evasion + b.evasion;
            c.bravery = a.bravery + b.bravery;
            c.temperature = a.temperature + b.temperature;
            return c;
        }

        public static Properties ComputeChildProperties(GameObject obj)
        {
            var props = obj.GetComponentsInChildren<PropertiesHolder>();
            Properties p = new Properties();
            foreach (var item in props)
            {
                p = p + item.properties;
            }
            return p;
        }
    }
}