using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kimeria.Nyx;

namespace Evo
{
    public class DignitasPanel : MonoBehaviour
    {
        [Header("Config")]
        public PlayerData playerData;
        [Header("Refs")]
        [SerializeField] TMP_Text textPlayerLevel;
        [SerializeField] Image icon;
        
        [EditorButton]
        public void RetrieveCurrentNFT()
        {

        }
    }
}

