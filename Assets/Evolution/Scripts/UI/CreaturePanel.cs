using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Kimeria.Nyx;

namespace Evo
{
    public class CreaturePanel : UIGamePanel
    {
        [SerializeField] Creature creature;
        [SerializeField] bool enableIfCreature = true;
        [Header("UI")]
        [SerializeField] TMP_Text texDNA;
        [SerializeField] UIParam[] paramsUI;
        [SerializeField] AbilityUI[] abilitiesUI;

        public Creature Creature { 
            get => creature; set
            {
                creature = value;
                Refresh();
            }
        }

        private void OnEnable()
        {
            Refresh();
        }
        [EditorButton]
        public void Refresh()
        {
            if (creature == null)
            {
                texDNA.text = string.Empty;
                if (enableIfCreature) gameObject.SetActive(false);
                return;
            }
            if (enableIfCreature) gameObject.SetActive(true);

            texDNA.text = creature.genetic.AsHexString;

            var p = creature.totalProperties;
            int index = 0;
            SetParam(index++, "Force", p.force);
            SetParam(index++, "Agility", p.agility);
            SetParam(index++, "Attack Boost", p.attack);
            SetParam(index++, "Armor", p.armor);
            SetParam(index++, "Evasion", p.evasion);
            SetParam(index++, "Reflect", p.damageReflect);
            SetParam(index++, "Lifesteal", p.lifesteal);

            var abs = creature.Abilities;
            for (int i = 0; i < abilitiesUI.Length; i++)
            {
                if (i < abs.Count) abilitiesUI[i].SetValue(abs[i]);
                else abilitiesUI[i].SetValue(null);
            }
        }

        void SetParam(int index, string title, int value)
        {
            if (index >= paramsUI.Length) return;
            paramsUI[index].SetValue(title, value.ToString());
        }
    }
}

