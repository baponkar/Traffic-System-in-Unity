using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace baponkar.npc.traffic
{
    public class TrafficGenerator : MonoBehaviour
    {
        #region variables
        public GameObject[] trafficPrefabs;

        public Transform waypointsTransform;

        Waypoint[] waypoints;

        int direction = 0;

        float generateInterval = 0.1f;
        float timer = 0;

        public int maxTraffic = 10;

        [Range(0f,1f)]
        public float minSpeed = 0.3f;
        [Range(0f, 1f)]
        public float maxSpeed = 0.7f;
        #endregion
        public void Start()
        {
            waypoints = waypointsTransform.GetComponentsInChildren<Waypoint>();
            timer = generateInterval;
        }

        public void Update()
        {
            timer -= Time.deltaTime;

            if(timer <=0)
            {
                if(maxTraffic > 0)
                {
                    Generate(trafficPrefabs[Random.Range(0, trafficPrefabs.Length - 1)],
                        waypoints[Random.Range(0, waypoints.Length - 1)],
                        Random.Range(minSpeed, maxSpeed), Mathf.RoundToInt(Random.Range(0.0f, 1.0f)));

                    maxTraffic--;

                }
                timer = generateInterval;
            }
        }

        void Generate(GameObject obj, Waypoint waypoint, float maxSpeed, int direction)
        {
            var traffic = Instantiate(obj, transform.parent, true);
            traffic.transform.parent = transform;
            traffic.transform.localPosition = waypoint.GetPosition();
            NavMeshAgentController cont = traffic.transform.GetComponent<NavMeshAgentController>();
            cont.maxSpeed = maxSpeed;
            cont.direction = direction;
            cont.currentWaypoint = waypoint;
        }
    }

}
