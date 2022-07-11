using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kimeria.Nyx;

namespace Evo
{
    public class UIGamePanel : MonoBehaviour
    {
        static Dictionary<string, UIGamePanel> panels = new Dictionary<string, UIGamePanel>();

        [Header("General")]
        public string id;
        public bool startEnabled = false;

        [Header("Refs")]
        public UnityEngine.UI.Button buttonClose;

        public static void OpenPanel(string name)
        {
            if (!panels.TryGetValue(name, out UIGamePanel p))
            {
                return;
            }
            p.Open();
        }
        public static void ClosePanel(string name)
        {
            if (!panels.TryGetValue(name, out UIGamePanel p))
            {
                return;
            }
            p.Close();
        }
        public static void SwitchPanel(string name)
        {
            if (!panels.TryGetValue(name, out UIGamePanel p))
            {
                return;
            }
            if (p.IsOpened()) p.Close();
            else p.Open();
        }

        private void Awake()
        {
            if (buttonClose != null)
            {
                buttonClose.onClick.AddListener(Close);
            }
            if (panels.ContainsKey(id))
            {
                Debug.Log($"Already a game panel with name {id}", this);
                return;
            }
            panels.Add(id, this);
            gameObject.SetActive(startEnabled);
        }
        public void Open()
        {
            gameObject.SetActive(true);
            Opened();
        }
        public void Close()
        {
            gameObject.SetActive(false);
            Closed();
        }
        public void SwitchOpen()
        {
            if (gameObject.activeSelf) Close();
            else Open();
        }

        public bool IsOpened()
        {
            return gameObject.activeSelf;
        }

        protected virtual void Opened()
        {

        }
        protected virtual void Closed()
        {

        }
    }
}

