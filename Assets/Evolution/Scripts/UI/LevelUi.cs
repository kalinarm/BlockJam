using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kimeria.Nyx;

namespace Evo
{
    public class LevelUi : MonoBehaviour
    {
        [SerializeField] TMP_Text textTitle;
        [SerializeField] TMP_Text textDescription;
        [SerializeField] public Image icon;
        public void SetValue(LevelData target)
        {
            if (target == null)
            {
                gameObject.SetActive(false);
                return;
            }
            gameObject.SetActive(true);
            textTitle.text = target.title;
            if (textDescription != null) textDescription.text = target.description;
            icon.sprite = target.icon;
            icon.gameObject.SetActive(icon.sprite != null);
        }
    }
}

