using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Health health;
        public Camera cam;
        private TargetWeapon target;
        /*
        public float velocity = 1f;
        public float turnTime = 0.1f;
        private float angle, targetAngle, turnSmoothVelocity;
        */

        [SerializeField] private InputActionAsset inputActions;
        private InputActionMap playerActionMap;
        private InputAction movement;
        private Vector3 movementVector;
        private NavMeshAgent navMeshAgent;
        private void Awake()
        {
            //target = GetComponent<TargetWeapon>();
            target = FindObjectOfType<TargetWeapon>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            playerActionMap = inputActions.FindActionMap("Player");
            movement = playerActionMap.FindAction("Move");
            movement.started += HandleMovementAction;
            movement.canceled += HandleMovementAction;
            movement.performed += HandleMovementAction;
            movement.Enable();
            playerActionMap.Enable();
            inputActions.Enable();
        }

        private void HandleMovementAction(InputAction.CallbackContext context)
        {
            Vector2 input = context.ReadValue<Vector2>();
            movementVector = new Vector3(input.x, 0, input.y);
            navMeshAgent.velocity = movementVector;
        }

        private void Start()
        {
            health = GetComponent<Health>();
            cam = Camera.main;
        }

        void Update()
        {
            if (health.IsDead()) return;

            if (InteractWithCombat()) return;

            if (InteractWithMovement()) return;
        }

        private bool InteractWithMovement()
        {
            if (!movement.IsInProgress()) return false;

            GetComponent<Mover>().StartMoveAction(movementVector, 1f, 0);
            return true;
        }

        /* BACKUP
        private bool InteractWithMovement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 move = transform.right * horizontal + transform.forward * vertical;

            if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)))
            {
                return false;
            }

            if (move.magnitude >= 0.1f)
            {
                controller.Move(move * velocity * Time.deltaTime);
                GetComponent<Mover>().StartMoveAction(move + transform.position, 1f);
                return true;
            }

            return false;

        }
        */
        private bool InteractWithCombat()
        {
            if (Input.GetKey(KeyCode.J))
            {
                GetComponent<Fighter>().Attack(target.gameObject, 1f); //full speed for the player move when attack
                return true;
            }
            return false;
        }
        /*
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
        */

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }



    }

}
