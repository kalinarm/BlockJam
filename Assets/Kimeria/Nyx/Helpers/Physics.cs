using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Security.Cryptography;

namespace Kimeria.Nyx.Helpers
{
    public static class PhysicsHelper
    {
        public static T DetectUnderMouse<T>(int layerMask, float distanceMax = 1000f) where T : MonoBehaviour
        {
            RaycastHit[] hits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            hits = Physics.RaycastAll(ray, distanceMax, layerMask);
            foreach (var hit in hits)
            {
                T comp = hit.collider.GetComponent<T>();
                if (comp != null) return comp;
            }
            return null;
        }
        public static T DetectUnderMouse2D<T>(int layerMask, float distanceMax = Mathf.Infinity) where T : MonoBehaviour
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var hits = Physics2D.GetRayIntersectionAll(ray, distanceMax, layerMask);
            foreach (var hit in hits)
            {
                T comp = hit.collider.GetComponent<T>();
                if (comp != null) return comp;
            }
            return null;
        }
    }
}

