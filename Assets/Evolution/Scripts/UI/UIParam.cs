using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kimeria.Nyx;

namespace Evo
{
    public class UIParam : MonoBehaviour
    {
        [SerializeField] TMP_Text title;
        [SerializeField] TMP_Text content;

        public void SetValue(string title, string content)
        {
            this.title.text = title;
            this.content.text = content;
        }
    }
}

