using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace Kimeria.Nyx.UI.Tools
{
    [CreateAssetMenu(menuName ="Nyx/UI/UIBuilderData")]
    public class UIBuilderData : ScriptableObject
    {
        public RectTransform prefabLabel;
        public RectTransform prefabButton;
        public RectTransform prefabToggle;
        public RectTransform prefabSlider;
        public RectTransform prefabInputText;
    }
}