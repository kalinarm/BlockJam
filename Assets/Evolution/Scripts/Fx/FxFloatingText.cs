using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

using Kimeria.Nyx.Modules.Genetic;
using Kimeria.Nyx.Tools;
using Kimeria.Nyx.Tools.FSM;

namespace Evo
{
    public class FxFloatingText : IFX
    {
        public string preffix;
        public string suffix;

        public FloatingText prefab;
        public Vector3 offset = Vector3.zero;
        public Color color = Color.green;

        public override void Trigger(Vector3 position, float intensity = 1)
        {
            FloatingText obj = GameObject.Instantiate(prefab, position + offset, prefab.transform.rotation);
            obj.SetText(Text(intensity));
            obj.SetColor(color);
        }

        string Text(float intensity)
        {
            StringBuilder s = new StringBuilder(preffix, 10);
            if (intensityMode == IntensityValue.TRIGGERED)
            {
                s.Append(((int)intensity).ToString());
            }
            s.Append(suffix);
            return s.ToString();
        }
    }
}

