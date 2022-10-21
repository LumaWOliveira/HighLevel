using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] private float maxSpeed = 6f;

        private NavMeshAgent navMeshAgent;
        private Health health;

        [SerializeField] private InputActionAsset inputActions;

        [SerializeField]
        [Range(0, 0.99f)]
        private float smoothing = 0.25f;
        [SerializeField] private float targetLerpSpeed = 1;

        private Vector3 targetDirection;
        private float lerpTime = 0;
        private Vector3 lastDirection;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination, float speedFraction, int index)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction, index);
        }

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination, speedFraction);
        }

        public void MoveTo(Vector3 movementVector, float speedFraction, int index)
        {
            movementVector.Normalize();
            if (movementVector != lastDirection)
            {
                lerpTime = 0;
            }

            lastDirection = movementVector;
            targetDirection = Vector3.Lerp(targetDirection, movementVector, Mathf.Clamp01(lerpTime * targetLerpSpeed * (1 - smoothing)));

            navMeshAgent.Move(targetDirection * navMeshAgent.speed * speedFraction * Time.deltaTime);

            Vector3 lookDirection = movementVector;

            if (lookDirection != Vector3.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), Mathf.Clamp01(lerpTime * targetLerpSpeed * (1 - smoothing)));
            }

            lerpTime += Time.deltaTime;
            navMeshAgent.isStopped = false;
        }

        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
            navMeshAgent.isStopped = false;

        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }


        private void UpdateAnimator()
        {

            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;

            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
