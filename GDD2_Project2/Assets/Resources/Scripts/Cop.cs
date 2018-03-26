using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop : MonoBehaviour
{
    // The target player
    GameObject player;

    //the path that the cop patrols
    public List<Transform> patrolPath = new List<Transform>();
    bool forwards;
    Transform curTargetPoint;
    int curPatrolPoint;

    //current active speed
    float curSpeed;
    public float patrolSpeed;
    //the chunk that the cop spawns in
    //public GameObject parentChunk;
    public Transform spawnPoint;

	public Rigidbody charController;

    enum CopState {
        patrolling,
        alert,
        pursuit,
        attack
    }
    CopState currentState;
	// Use this for initialization
	void Start ()
    {
		charController = this.GetComponent<Rigidbody> ();
        player = GameObject.Find("Robber");
        currentState = CopState.patrolling;
        curPatrolPoint = 0;
        forwards = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //handle action state of the cop
        switch (currentState) {
            //looping back and forth through path
            case CopState.patrolling:
                //set speed
                if(patrolSpeed != curSpeed)
                    curSpeed = patrolSpeed;

                //if we reached a target
                if (curTargetPoint == null) {
                    //change index based on movement direction
                    if (forwards) {
                        curPatrolPoint++;
                    } else {
                        curPatrolPoint--;
                    }
                    Debug.Log(curPatrolPoint);
                    curTargetPoint = patrolPath[curPatrolPoint];
                    
                }
                //if reached target
                else if (Vector3.Distance(transform.position, curTargetPoint.position) < .5f) {
                    curTargetPoint = null;
                    
                    //bounce off of edges of list
                    if(curPatrolPoint >= patrolPath.Count - 1 && forwards == true) {
                        forwards = false;
                    }else if (curPatrolPoint <= 0 && forwards == false) {
                        forwards = true;
                    }
                }
                
                if(curTargetPoint != null) {
                    Vector3 normalizedDirection = Vector3.Normalize(curTargetPoint.position - transform.position);
                    charController.MoveRotation(Quaternion.LookRotation(Vector3.Normalize(curTargetPoint.position - transform.position)));
                    charController.MovePosition(transform.position + normalizedDirection * curSpeed * Time.deltaTime);
                }
                break;
            //chasing player
            case CopState.pursuit:
                break;
            //sighted something, moving towards it
            case CopState.alert:
                break;
            //within attacking range of robber
            case CopState.attack:
                break;
        }
    }
    public void CreatePatrolPath() {
        int PathDepth = Random.Range(3, 7);
        Transform curPoint = spawnPoint;
        for(int i = 0; i < PathDepth; i++) {
            Waypoint curPointWayPoint = curPoint.gameObject.GetComponent<Waypoint>();
            if (!patrolPath.Contains(curPoint)) {
                patrolPath.Add(curPoint);
                //curPoint.GetComponent<MeshRenderer>().enabled = true;
            }else {
                curPoint = curPointWayPoint.links[Random.Range(0, curPointWayPoint.links.Count)].transform;
                i--;
            }
            curPoint = curPointWayPoint.links[Random.Range(0, curPointWayPoint.links.Count)].transform;
        }
    }
    //sets the parent chunk of the cop
    /*public void SetParent(GameObject newChunk) {
        parentChunk = newChunk;
    }*/
    //takes waypoint from a chunk and sets it as the cop's spawn point
    public void SetSpawnPoint(GameObject wayPoint) {
        spawnPoint = wayPoint.transform;
    }
}
