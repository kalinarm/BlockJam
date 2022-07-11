using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kimeria.Nyx.Tools.Localisation
{
    public class LocalizedText : MonoBehaviour
    {
        public string key;
        public bool useCapitalLetter = false;
        
        void Start()
        {
            Text text = GetComponent<Text>();
            if (text != null)
            {
                text.text = LocalizationManager.Instance.GetLocalizedValue(key);
                if (useCapitalLetter)
                {
                    text.text = text.text.ToUpper();
                }
            }
        }
    }
}