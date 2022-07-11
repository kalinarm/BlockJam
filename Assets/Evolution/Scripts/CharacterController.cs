using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kimeria.Nyx;

namespace Evo
{
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] GameData gameData;

        public Animator animator;

        public float walkSpeed = 1f;

        [SerializeField] string animRun = "run";

        Vector3 scale;
        Vector3 move = Vector3.zero;

        [SerializeField] bool useMouse = true;
        [SerializeField] float mouseMarge = 20f;
        [SerializeField] float mouseSpeed = 1f;

        private void Start()
        {
            scale = transform.localScale;
        }
        private void Update()
        {
            move.x = Input.GetAxis("Horizontal");
            move.z = Input.GetAxis("Vertical");

            if (useMouse)
            {
                Vector2 mPos = Input.mousePosition;
                if (mPos.x < mouseMarge)
                {
                    move.x -= mouseSpeed;
                }
                else if (mPos.x > Screen.width - mouseMarge)
                {
                    move.x += mouseSpeed;
                }
            }


            LimitVelOutOfZone();

            if (Mathf.Abs(move.x) > 0.1f)
            {
                Vector3 s = transform.localScale;
                s.x = scale.x * Mathf.Sign(move.x);
                transform.localScale = s;
            }

            animator.SetBool(animRun, move.sqrMagnitude > 0.01f);
        }

        private void LimitVelOutOfZone()
        {
            Vector3 p = transform.position;
            if (p.z >= gameData.maxZ)
            {
                move.z = Mathf.Min(move.z, 0f);
            }
            if (p.z <= gameData.minZ)
            {
                move.z = Mathf.Max(move.z, 0f);
            }
            if (p.x >= gameData.maxX)
            {
                move.x = Mathf.Min(move.x, 0f);
            }
            if (p.x <= gameData.minX)
            {
                move.x = Mathf.Max(move.x, 0f);
            }
        }

        private void FixedUpdate()
        {
            move = Vector3.ClampMagnitude(move, 1f);
            transform.Translate(move * walkSpeed * Time.fixedDeltaTime);
        }
    }
}

