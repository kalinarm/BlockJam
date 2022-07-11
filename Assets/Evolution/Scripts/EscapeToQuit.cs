using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;
using Kimeria.Nyx.UI;

namespace Evo
{
    public class EscapeToQuit : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ModalPanelTMP.ShowMessage("Are you sure you want to return to menu ? ", QuitEffective, delegate { });
            }
        }

        void QuitEffective()
        {
            Game.Instance.BackToMenu();
        }
    }
}

