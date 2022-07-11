using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class WrapperGamePanel : MonoBehaviour
    {
        public string idToCall;

        [EditorButton]
        public void Open()
        {
            UIGamePanel.OpenPanel(idToCall);
        }

        [EditorButton]
        public void Close()
        {
            UIGamePanel.ClosePanel(idToCall);
        }

        [EditorButton]
        public void Switch()
        {
            UIGamePanel.SwitchPanel(idToCall);
        }
    }
}

