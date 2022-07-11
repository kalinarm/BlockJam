using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kimeria.Nyx;

namespace Evo
{
    public class AbilityUI : MonoBehaviour
    {
        [SerializeField] TMP_Text textTitle;
        [SerializeField] TMP_Text textName;
        [SerializeField] TMP_Text texDamage;
        [SerializeField] TMP_Text texSpeed;
        [SerializeField] Image icon;
        public void SetValue(IAbility ability)
        {
            if (ability == null)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            textTitle.text = ability.type.ToString();
            textName.text = ability.gameName;
            
            if (ability is AbilityAttackMelee)
            {
                AbilityAttackMelee a = (AbilityAttackMelee)ability;
                texDamage.text = a.attackData.damage.ToString();
            }else
            {
                texDamage.text = string.Empty;
            }
        }
    }
}

