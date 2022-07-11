using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kimeria.Nyx;

namespace Evo.Web3
{
    public class Web3Panel : UIGamePanel
    {
        public TMP_Text texAccount;

        private void OnEnable()
        {
            Refresh();
        }

        void Refresh()
        {
            if (Web3Hub.Instance == null) return;
            texAccount.text = Web3Hub.Instance.account;
        }
    }
}


