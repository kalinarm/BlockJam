using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace Kimeria.Nyx.UI
{
    public class BaseListUI<B, T> : MonoBehaviour where T : MonoBehaviour
    {
        public Transform listRoot;
        public T prefab;

        public List<T> instances = new List<T>();

        [EditorButton]
        public void CreateUIList(List<B> objs)
        {
            List<T> toRemove = new List<T>(instances);
            for (int i = 0; i < toRemove.Count; i++)
            {
                GameObject.Destroy(toRemove[i].gameObject);
            }
            instances.Clear();

            if (listRoot != null && prefab != null)
            {
                for (int i = 0; i < objs.Count; i++)
                {
                    if (objs[i] == null) continue;
                    T instance = GameObject.Instantiate(prefab, listRoot);
                    instance.gameObject.SetActive(true);
                    instances.Add(instance);
                    Configure(objs[i], instance);
                }
            }
        }
        public virtual void Configure(B obj, T ui)
        {

        }
    }
}

