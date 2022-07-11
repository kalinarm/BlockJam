using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kimeria.Nyx.SimpleBehaviours
{
    public class Rotator : MonoBehaviour
    {
        public float speed = 1f;
        public Vector3 rotateVector = Vector3.up;
        public Space space = Space.Self;

        void Update()
        {
            transform.Rotate(rotateVector, speed * Time.deltaTime, space);
        }
    }
}