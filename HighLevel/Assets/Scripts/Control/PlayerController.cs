using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health health;
        public CharacterController controller;
        public Camera cam;

        public float velocity = 1f;
        public float turnTime = 0.1f;
        private float angle, targetAngle, turnSmoothVelocity;

        private void Start()
        {
            health = GetComponent<Health>();
            cam = Camera.main;
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;

            if (InteractWithMovement()) return;
        }

        /*
        private bool InteractWithMovement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                
                targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                
                direction = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(direction.normalized * Time.deltaTime * velocity);
                GetComponent<Mover>().StartMoveAction(direction.normalized, 1f);
                return true;
            }
            else return false;
        }
        */

        
        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject, 1f); //full speed for the player move when attack
                }
                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(hit.point,1f); //full speed for the player move
                }
                return true;   
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        


    }

}
