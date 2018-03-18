using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class EnemyAI : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State
        {
            PATROL,
            CHASE
        }

        public State state;
        private bool alive;

        // Patrol Variables
        public GameObject[] waypoints;
        private int waypointInd = 0;
        public float patrolSpeed = 0.5f;

        // Chase Variables
        public float chaseSpeed = 1f;
        public GameObject target;

        // Use this for initialization
        void Start()
        {
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = EnemyAI.State.PATROL;
            alive = true;

            StartCoroutine("FSM");
        }

        void Update()
        {
            Vector3 dir = (target.transform.position - transform.position).normalized;

            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(transform.position, transform.forward, out hit, 50))
            {
                if (hit.transform.gameObject.tag == "obstacle")
                {
                    dir += hit.normal * 1000;
                    Debug.DrawLine(transform.position, hit.point, Color.blue);
                }
            }

            Vector3 leftR = transform.position;
            Vector3 rightR = transform.position;

            leftR.x -= 1;
            rightR.x += 1;

            if (Physics.Raycast(leftR, transform.forward, out hit, 50))
            {
                if (hit.transform.gameObject.tag == "obstacle")
                {
                    Debug.DrawLine(leftR, hit.point, Color.red);
                    dir += hit.normal * 1000;
                }
            }

            if (Physics.Raycast(rightR, transform.forward, out hit, 50))
            {
                if (hit.transform.gameObject.tag == "obstacle")
                {
                    Debug.DrawLine(rightR, hit.point, Color.red);
                    dir += hit.normal * 1000;
                }
            }

            Quaternion rot = Quaternion.LookRotation(dir);

            transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime);

            if (state == EnemyAI.State.PATROL)
                transform.position += transform.forward * patrolSpeed * Time.deltaTime;
            else
                transform.position += transform.forward * chaseSpeed * Time.deltaTime;
        }

        IEnumerator FSM()
        {
            while(alive)
            {
                switch (state)
                {
                    case State.PATROL:
                        Patrol();
                        break;
                    case State.CHASE:
                        Chase();
                        break;
                }

                yield return null;
            }
        }

        void Patrol()
        {
            agent.speed = patrolSpeed;

            if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
            {
                agent.SetDestination(waypoints[waypointInd].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
            {
                waypointInd++;

                if (waypointInd >= waypoints.Length)
                    waypointInd = 0;
            }
            else
                character.Move(Vector3.zero, false, false);
        }

        void Chase()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }

        void OnTriggerEnter(Collider col)
        {
            if (col.tag == "Player")
                state = EnemyAI.State.CHASE;
        }

        void OnTriggerExit(Collider col)
        {
            if (col.tag == "Player")
                state = EnemyAI.State.PATROL;
        }

        void OnCollisionEnter(Collision col)
        {
            if (col.gameObject.tag == "Player")
                Destroy(col.gameObject);
        }
    }
}
