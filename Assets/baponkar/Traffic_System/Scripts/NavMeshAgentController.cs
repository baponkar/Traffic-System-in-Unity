using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using baponkar;


namespace baponkar.npc.traffic
{

    public class NavMeshAgentController : MonoBehaviour
    {
        NavMeshAgent agent;
        Animator animator;
    
        public float targetDistance;
        [Range(0.0f, 1.0f)]
        [SerializeField]  public float maxSpeed = 0.3f;

        [SerializeField] public Waypoint currentWaypoint;

        [Range(0, 1)]
        public int direction = 0;
    

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            agent.destination = currentWaypoint.GetPosition();
        }

        void Update()
        {
            float speed = Mathf.Clamp(agent.speed, 0f, maxSpeed);

            animator.SetFloat("Speed", speed);

            agent.speed = maxSpeed * 5f;
            agent.acceleration = maxSpeed * 12f;


            targetDistance = Vector3.Distance(agent.transform.position, currentWaypoint.GetPosition());

            if (targetDistance > agent.stoppingDistance)
            {
                agent.destination = currentWaypoint.GetPosition();
            }
            else 
            { 
                bool shouldBranch = false;

                if (currentWaypoint.branches != null && currentWaypoint.branches.Count > 0)
                {
                    shouldBranch = Random.Range(0f, 1f) <= currentWaypoint.branchProbability ? true : false;
                }

                if (shouldBranch)
                {
                    currentWaypoint = currentWaypoint.branches[Random.Range(0, currentWaypoint.branches.Count - 1)];
                }
                else
                {
                    if (direction == 0)
                    {
                        if (currentWaypoint.nextWaypoint != null)
                        {
                            currentWaypoint = currentWaypoint.nextWaypoint;
                        }
                        else
                        {
                            currentWaypoint = currentWaypoint.prevWaypoint;
                            direction = 1;
                        }
                    }

                    if (direction == 1)
                    {
                        if (currentWaypoint.prevWaypoint != null)
                        {
                            currentWaypoint = currentWaypoint.prevWaypoint;
                        }
                        else
                        {
                            currentWaypoint = currentWaypoint.nextWaypoint;
                            direction = 0;
                        }
                    }
                }
                
            }
        }
    }
}
