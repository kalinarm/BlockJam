using UnityEngine;
using System.Collections;

namespace Kimeria.Nyx.SimpleBehaviours
{
    public class SimpleMover : MonoBehaviour
    {
        public bool useWorld = false;
        public Vector3 translate;
        public Vector3 rotate;
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(translate * Time.deltaTime, useWorld ? Space.World : Space.Self);
            transform.Rotate(rotate * Time.deltaTime, useWorld ? Space.World : Space.Self);
        }
    }
}
