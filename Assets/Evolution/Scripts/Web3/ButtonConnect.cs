using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

using Kimeria.Nyx;

namespace Evo.Web3.UI
{
    public class ButtonConnect : MonoBehaviour
    {
        public UnityEngine.UI.Button button;
        public TMPro.TMP_Text text;

        public Color colorConnected = Color.white;
        public Color colorDisconnected = Color.white;

        private void Start()
        {
            Refresh();
        }

        private void OnEnable()
        {
            Web3Hub.Events.AddListener<Evt.ConnectionStatusChanged>(OnConnectionStatusChanged);
        }
        private void OnDisable()
        {
            Web3Hub.Events.RemoveListener<Evt.ConnectionStatusChanged>(OnConnectionStatusChanged);
        }

        void OnConnectionStatusChanged(Evt.ConnectionStatusChanged evt)
        {
            Refresh();
        }

        void Refresh()
        {
            if (Web3Hub.Instance.IsConnected())
            {
                button.interactable = false;
                text.text = "Connected";
                text.color = colorConnected;
            }else
            {
                button.interactable = true;
                text.text = "Connect";
                text.color = colorDisconnected;
            }
        }
    }
}


