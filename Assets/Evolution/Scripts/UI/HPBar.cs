using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kimeria.Nyx;

namespace Evo
{
    public class HPBar : MonoBehaviour
    {
        public HP target;

        [SerializeField] public Slider bar;
        [SerializeField] TMP_Text hpMax;
        [SerializeField] TMP_Text hp;
        [SerializeField] Vector3 offset = Vector3.one;

        [SerializeField] bool destroyIfNullTarget = true;
        
        public void SetValue(HP target)
        {
            this.target = target;
        }

        private void RefreshPosition()
        {
            transform.position = target.transform.position + offset;
        }

        private void Update()
        {
            if (target == null)
            {
                GameObject.Destroy(gameObject);
                return;
            }
            Refresh();
            RefreshPosition();
        }

        void Refresh()
        {
            bar.value = target.HpNorm;
            if (hpMax != null) hpMax.text = target.MaxHp.ToString();
            if (hp != null) hp.text = target.Hp.ToString();
        }
    }
}

