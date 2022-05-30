using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace baponkar
{
    public class CharacterNavigationController : MonoBehaviour
    {
        CharacterController controller;

        public bool reachedDestination = false;
        public Vector3 destination;
        public float remainingDistance;

        public float rotationSpeed = 120f;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        private float playerSpeed = 2.0f;
        private float jumpHeight = 1.0f;
        private float gravityValue = -9.81f;

        public float stoppingDistance;

        
        void Awake()
        {
            controller = GetComponent<CharacterController>();
        }

        
        void Update()
        {
            
            remainingDistance = Vector3.Distance(transform.position, destination);
            if(remainingDistance < stoppingDistance)
            {
                reachedDestination = true;
            }
            else
            {
                reachedDestination = false;
            }

            Move();
        }

        public void SetDestination(Vector3 destination)
        {
            this.destination = destination;
        }

        void Move()
        {
            if(remainingDistance > 0.1f)
            {
                groundedPlayer = controller.isGrounded;
                if (groundedPlayer && playerVelocity.y < 0)
                {
                    playerVelocity.y = 0f;
                }

                Vector3 move = destination - transform.position;
                controller.Move(move * Time.deltaTime * playerSpeed);

                if (move != Vector3.zero)
                {
                    gameObject.transform.forward = move;
                    Quaternion currentRotation = Quaternion.Euler(transform.rotation.eulerAngles);
                    Quaternion targetRotation = Quaternion.LookRotation(move);
                    transform.rotation = Quaternion.Slerp(currentRotation,targetRotation, Time.deltaTime * rotationSpeed);
                }

                // Changes the height position of the player..
                // if (Input.GetButtonDown("Jump") && groundedPlayer)
                // {
                //     playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                // }

                playerVelocity.y += gravityValue * Time.deltaTime;
                controller.Move(playerVelocity * Time.deltaTime);
            }
        }

    }
}
