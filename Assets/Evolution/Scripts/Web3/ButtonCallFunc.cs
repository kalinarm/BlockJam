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
    public class ButtonCallFunc : MonoBehaviour
    {
        public UnityEngine.UI.Button button;
        public TMPro.TMP_Text text;

        public Color colorConnected = Color.white;
        public Color colorDisconnected = Color.white;

        bool checkClaim = false;

        private void Start()
        {
            button.onClick.AddListener(ClaimToken);
            CheckIfConnected();
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
            CheckIfConnected();
        }

        async void CheckIfHasClaimToken()
        {
            if (checkClaim)
            {
                return;
            }
            checkClaim = true;
            text.text = "Checking";

            bool result = await Web3Hub.Instance.HasClaimToken(Web3Hub.Instance.GoldToken);
            if (result)
            {
                button.interactable = false;
                text.text = "Already Claimed";
            }else
            {
                button.interactable = true;
                text.text = "Claim your free Tokens";
            }
            checkClaim = true;
        }

        async void ClaimToken()
        {
            Web3Hub hub = Web3Hub.Instance;
            await hub.ClaimToken(hub.GoldToken);
            await hub.GetBalance(hub.GoldToken);
            Kimeria.Nyx.UI.ModalPanelTMP.ShowMessage("You have claimed your 100 tokens", TokenClaimed);
        }
        void TokenClaimedRunning()
        {
            button.interactable = false;
            text.text = "Pending";
        }
        void TokenClaimed()
        {
            button.interactable = false;
            text.text = "Already Claimed";
        }

        void CheckIfConnected()
        {
            if (Web3Hub.Instance.IsConnected())
            {
                button.interactable = true;
                text.text = "Claim your free Tokens";
                text.color = colorConnected;
                CheckIfHasClaimToken();
            }
            else
            {
                button.interactable = false;
                text.text = "Connect first";
                text.color = colorDisconnected;
            }
        }
    }
}


