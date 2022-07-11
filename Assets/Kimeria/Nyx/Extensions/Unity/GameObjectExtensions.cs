using UnityEngine;
using System.Collections;

namespace Kimeria.Nyx
{
    public static class GameObjectExtensions
    {
        public static GameObject CreateChild(this GameObject self, string name)
        {
            GameObject obj = new GameObject(name);
            obj.transform.SetParent(self.transform);
            obj.transform.ResetLocal();
            return obj;
        }

        public static T GetOrCreateComponent<T>(this GameObject self) where T : Component
        {
            T r = self.GetComponent<T>();
            if (r == null)
            {
                r = self.AddComponent<T>();
            }
            return r;
        }

        public static T CreateChildWithComponent<T>(this GameObject self, string name) where T : Component
        {
            GameObject go = new GameObject(name);
            T r = go.AddComponent<T>();
            if (self.GetComponent<RectTransform>() != null)
            {
                go.AddComponent<RectTransform>();
            }
            go.transform.SetParent(self.transform);
            return r;
        }

        public static T GetOrCreateGOWithComponent<T>(this GameObject self, string name) where T : Behaviour
        {
            T r = self.GetComponentInChildren<T>();
            if (r == null)
            {
                GameObject go = new GameObject(name);
                go.transform.SetParent(self.transform);
                r = go.AddComponent<T>();
            }
            return r;
        }

        public static T GetOrCreateGoWithComponent<T>(string name) where T : MonoBehaviour
        {
            T r = GameObject.FindObjectOfType<T>();
            if (r == null)
            {
                GameObject go = new GameObject(name);
                return go.AddComponent<T>();
            }
            else
            {
                return r;
            }
        }

        public static GameObject GetOrCreateChild(this GameObject self, string name)
        {
            Transform childT = self.transform.Find(name);
            if (childT == null)
            {
                childT = new GameObject(name).transform;
                childT.SetParent(self.transform);
            }
            return childT.gameObject;
        }
    }
}
