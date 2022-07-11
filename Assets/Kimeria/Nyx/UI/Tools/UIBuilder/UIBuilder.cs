using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Kimeria.Nyx.UI.Tools
{
    public class UIBuilder : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] UIBuilderData data;
        [Header("Refs")]
        [SerializeField] Transform widgetParent;

        public delegate void OnClick();
        public delegate void OnToggleValueChange(Toggle t);
        public delegate void OnSlider(float f);

        [EditorButton]
        public void DestroyAllChildrens()
        {
            widgetParent.DeleteAllChilds();
        }

        public RectTransform AddLabel(string label)
        {
            //setup
            RectTransform rt = Instantiate(data.prefabLabel);
            rt.SetParent(widgetParent);
            SetLabel(label, rt);

            return rt;
        }
        public RectTransform AddButton(string label, UnityAction handler)
        {
            //setup
            RectTransform rt = Instantiate(data.prefabButton);
            rt.SetParent(widgetParent);
            SetLabel(label, rt);

            //configure
            Button widget = rt.GetComponentInChildren<Button>();
            //callback
            widget.onClick.AddListener(handler);
            return rt;
        }

        public RectTransform AddToggle(string label, OnToggleValueChange handler, bool defaultValue = false)
        {
            //setup
            RectTransform rt = Instantiate(data.prefabToggle);
            rt.SetParent(widgetParent);
            SetLabel(label, rt);

            //configure
            Toggle widget = rt.GetComponentInChildren<Toggle>();
            widget.isOn = defaultValue;

            //callback
            widget.onValueChanged.AddListener(delegate { handler(widget); });
            return rt;
        }

        public RectTransform AddSlider(string label, float min, float max, OnSlider onValueChanged, bool wholeNumbersOnly = false)
        {
            //setup
            RectTransform rt = Instantiate(data.prefabToggle);
            rt.SetParent(widgetParent);
            SetLabel(label, rt);

            //configure
            Slider widget = rt.GetComponentInChildren<Slider>();
            widget.minValue = min;
            widget.maxValue = max;
            widget.wholeNumbers = wholeNumbersOnly;

            //callback
            widget.onValueChanged.AddListener(delegate (float f) { onValueChanged(f); });
            return rt;
        }

        private static GameObject Instantiate(GameObject prefab)
        {
#if UNITY_EDITOR
            return PrefabUtility.InstantiatePrefab(prefab) as GameObject;
#else
            return GameObject.Instantiate(prefab);
#endif
        }

        private static RectTransform Instantiate(RectTransform prefab)
        {
#if UNITY_EDITOR
            return PrefabUtility.InstantiatePrefab(prefab) as RectTransform;
#else
            return GameObject.Instantiate(prefab);
#endif
        }

        private static void SetLabel(string label, RectTransform rt)
        {
            Text widgetText = rt.GetComponentInChildren<Text>();
            if (widgetText != null)
            {
                widgetText.text = label;
            }
        }
    }
}