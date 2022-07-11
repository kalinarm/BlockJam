using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Kimeria.Nyx;

namespace Evo.Web3
{
    public class TokenVariableUI : MonoBehaviour
    {
        public string id;
        public TokenData variable;
        public TMP_Text text;

        //Unknown,
        //   Updating,
        //   Updated,
        //   Error
        public List<GameObject> displays = new List<GameObject>();

        private void Start()
        {
            Refresh();
        }

        private void OnEnable()
        {
            Web3Hub.Events.AddListener<Evt.VariableChanged>(OnVariableChanged);
        }
        private void OnDisbale()
        {
            Web3Hub.Events.RemoveListener<Evt.VariableChanged>(OnVariableChanged);
        }

        void OnVariableChanged(Evt.VariableChanged evt)
        {
            if (evt.variable != variable) return;
            Refresh();
        }

        void Refresh()
        {
            displays[0].SetActive(variable.State == TokenData.VariableState.Unknown);
            displays[1].SetActive(variable.State == TokenData.VariableState.Updating);
            displays[2].SetActive(variable.State == TokenData.VariableState.Updated);
            displays[3].SetActive(variable.State == TokenData.VariableState.Error);

            if (variable.State == TokenData.VariableState.Updated)
            {
                text.text = variable.GetIntValue().ToString();
            }else
            {
                text.text = "?";
            }
        }
    }
}


