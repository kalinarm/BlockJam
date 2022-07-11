using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using TMPro;
namespace Kimeria.Nyx.UI
{
    public class SwitchTimeScale : MonoBehaviour
    {
        [SerializeField] Image icon;
        [SerializeField] Sprite normal;
        [SerializeField] Sprite modex2;

        bool isx2 = false;

        private void Start()
        {
            Set();
        }

        private void OnDestroy()
        {
            Time.timeScale = 1f;
            Set();
        }
        private void OnDisable()
        {
            isx2 = false;
            Set();
        }
        public void Switch()
        {
            isx2 = !isx2;
            Set();
        }

        void Set()
        {
            Time.timeScale = isx2 ? 2f : 1f;
            Refresh();
        }

        void Refresh()
        {
            icon.sprite = isx2 ? modex2 : normal;
        }
    }
}

