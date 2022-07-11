using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kimeria.Nyx.VR
{
    public class FollowEyePanel : MonoBehaviour
    {
        [SerializeField] Transform reference;
        [SerializeField] bool blockHorizontal = true;

        [SerializeField] float distanceMax = 5f;
        [SerializeField] float timeMove = 0.2f;

        [SerializeField] Vector3 localPosition = Vector3.forward;
        [SerializeField] Vector3 localRotation = Vector3.zero;
        Vector3 oldWorldPosition;
        Vector3 goal;

        float timer = 0f;

        void OnEnable()
        {
            if (reference == null)
            {
                reference = transform.parent;
            }

            transform.localPosition = localPosition;
            transform.localRotation = Quaternion.Euler(localRotation);

            oldWorldPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (timer < timeMove)
            {
                transform.position = Vector3.Lerp(oldWorldPosition, goal, timer / timeMove);
                UpdateDirection();
                timer += Time.deltaTime;
            }
            else
            {
                goal = GetGoalPos();
                float dist = (goal - oldWorldPosition).sqrMagnitude;
                if (dist > distanceMax * distanceMax)
                {
                    timer = 0f;
                    oldWorldPosition = transform.position;
                }
            }
            
        }

        private void UpdateDirection()
        {
            Vector3 dirH = reference.position - transform.position;
            if (blockHorizontal) dirH.y = 0f;
            transform.LookAt(transform.position - dirH, Vector3.up);
        }

        Vector3 GetGoalPos()
        {
            return reference.TransformPoint(localPosition);
        }
    }
}
