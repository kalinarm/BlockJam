using UnityEngine;
using System.Collections;

namespace Kimeria.Nyx.SimpleBehaviours
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
