using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class FloatingText : MonoBehaviour
    {
        public TextMesh textMesh;
        public void SetText(string text)
        {
            textMesh.text = text;
        }
        public void SetColor(Color color)
        {
            textMesh.color = color;
        }
    }
}

