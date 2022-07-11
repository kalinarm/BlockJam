using UnityEngine;
using System.Collections;

namespace Kimeria.Nyx
{
    public static class TransformExtension
    {
        public static void ResetLocal(this Transform self)
        {
            self.localPosition = Vector3.zero;
            self.localRotation = Quaternion.identity;
            self.localScale = Vector3.one;
        }

        /*public static Transform DeleteAllChilds(this Transform transform)
        {
            while (transform.childCount > 0)
            {
#if UNITY_EDITOR
                if (Application.isPlaying)
                {
                    GameObject.Destroy(transform.GetChild(0).gameObject);
                }
                else
                {
                    GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
                }

#else
                GameObject.Destroy(transform.GetChild(0));
#endif
            }
            return transform;
        }*/

        public static Transform DeleteAllChilds(this Transform transform)
        {
            foreach (Transform child in transform)
            {
#if UNITY_EDITOR
                if (Application.isPlaying)
                {
                    child.gameObject.SetActive(false);
                    GameObject.Destroy(child.gameObject);
                }
                else
                {
                    GameObject.DestroyImmediate(child.gameObject);
                }
#else
            child.gameObject.SetActive(false);
            GameObject.Destroy(child.gameObject);
#endif

            }
            return transform;
        }
    }
}

