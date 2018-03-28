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

    //last known position of the player
    Vector3 lastKnownPos;

    //current active speed
    float curSpeed;
    public float patrolSpeed;
    public float pursuitSpeed;
    public float patrolSightRadius;
    public float alertSightRadius;
    public float sightAngle;

    //how long it takes cops to go back to patrol mode
    float alertTimer;
    public float maxAlertTime;
    //how long it takes to recognize that the cop needs to pursue the robber
    float recognitionTimer;
    public float maxRecognitionTime;
    
    //how long before the cop gives up on chasing you
    float pursuitTimer;
    public float maxPursuitTime;
    public Light flashLight;

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
        //initalize timers
        alertTimer = maxAlertTime;
        recognitionTimer = maxRecognitionTime;
        flashLight.range = alertSightRadius;
        flashLight.spotAngle = sightAngle + 3;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //handle action state of the cop
        switch (currentState) {
            //looping back and forth through path
            case CopState.patrolling:
                //set flashlight
                if (flashLight.enabled == true) {
                    flashLight.enabled = false;
                }
                //check for player
                float angle = Vector3.Angle(transform.forward, player.transform.position - transform.position);
                
                if (Vector3.Distance(player.transform.position, transform.position) < patrolSightRadius && angle < sightAngle) {
                    lastKnownPos = player.transform.position;
                    currentState = CopState.alert;
                }
                //set speed
                if (patrolSpeed != curSpeed)
                    curSpeed = patrolSpeed;

                //if we reached a target
                if (curTargetPoint == null) {
                    //change index based on movement direction
                    if (forwards) {
                        curPatrolPoint++;
                    } else {
                        curPatrolPoint--;
                    }
                    //Debug.Log(curPatrolPoint);
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
                    charController.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.Normalize(curTargetPoint.position - transform.position)), Time.deltaTime * 5f));
                    charController.MovePosition(transform.position + normalizedDirection * curSpeed * Time.deltaTime);
                }
                break;
            //chasing player
            case CopState.pursuit:
                Debug.Log("Pursuit");
                //set speed
                if(curSpeed < pursuitSpeed)
                    curSpeed = pursuitSpeed;

                angle = Vector3.Angle(transform.forward, player.transform.position - transform.position);
                if (Vector3.Distance(player.transform.position, transform.position) < alertSightRadius && angle < sightAngle) {          
                    lastKnownPos = player.transform.position;
                    pursuitTimer = maxPursuitTime;
                }else {
                    pursuitTimer -= Time.deltaTime;
                    if(pursuitTimer <= 0f) {
                        pursuitTimer = maxPursuitTime;
                        currentState = CopState.patrolling;
                    }
                }
                if(Vector3.Distance(transform.position, lastKnownPos) < .5f) {
                    charController.MoveRotation(Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 45f * Time.deltaTime, 0f)));
                }else {
                    //move towards the player's last known position
                    Vector3 playerDirection = Vector3.Normalize(lastKnownPos - transform.position);
                    charController.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.Normalize(lastKnownPos - transform.position)), Time.deltaTime * 15f));
                    charController.MovePosition(transform.position + playerDirection * curSpeed * Time.deltaTime);
                }

                break;

            //sighted something, moving towards it
            case CopState.alert:
                Debug.Log("Alerted");
                //set flashlight
                if(flashLight.enabled == false) {
                    flashLight.enabled = true;
                }
                //check if player is in line of sight
                angle = Vector3.Angle(transform.forward, player.transform.position - transform.position);
                if (Vector3.Distance(player.transform.position, transform.position) < alertSightRadius && angle < sightAngle) {
                    
                    recognitionTimer -= Time.deltaTime;

                    //reset alert timer if we see the player again
                    alertTimer = maxAlertTime;
                    //update the last known position of the player
                    lastKnownPos = player.transform.position;
                    //time allowed to recognize the player for pursuit
                    if(recognitionTimer <= 0f) {
                        recognitionTimer = maxRecognitionTime;
                        currentState = CopState.pursuit;
                    }
                }
                //if we're out of alert time, go back to patrolling
                alertTimer -= Time.deltaTime;
                if(alertTimer <= 0f) {
                    alertTimer = maxAlertTime;
                    currentState = CopState.patrolling;
                }

                //move towards the player's last known position
                Vector3 direction = Vector3.Normalize(lastKnownPos - transform.position);
                charController.MoveRotation(Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.Normalize(lastKnownPos - transform.position)), Time.deltaTime * 10f));
                charController.MovePosition(transform.position + direction * curSpeed * Time.deltaTime);
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
