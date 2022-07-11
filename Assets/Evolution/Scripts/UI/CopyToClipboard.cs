using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;
namespace Kimeria.Nyx.UI
{
    public class CopyToClipboard : MonoBehaviour
    {
        [SerializeField] TMP_Text title;

        public void Copy()
        {
            if (title == null) return;
            GUIUtility.systemCopyBuffer = title.text;
        }
    }
}

