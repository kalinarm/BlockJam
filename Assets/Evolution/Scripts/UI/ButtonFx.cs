using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Evo
{
    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class ButtonFx : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] UnityEngine.UI.Button button;
        [SerializeField] Fx fxOnClick;
        [SerializeField] Fx fxOnEnter;


        private void Start()
        {
            if (button ==null)
            {
                button = GetComponent<UnityEngine.UI.Button>();
            }
            button.onClick.AddListener(OnClick);
        }
        void OnClick()
        {
            if (!button.interactable) return;
            fxOnClick?.Trigger();
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (!button.interactable) return;
            fxOnEnter?.Trigger();
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            if (!button.interactable) return;
        }
    }
}

