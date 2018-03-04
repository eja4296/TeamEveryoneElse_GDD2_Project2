using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopScript : MonoBehaviour
{
    public float seekWeight;            // the weight of the cop, which will influence the rate at it will accelerate when seeking
    public float maxSpeed;              // the max speed of the cop
    public GameObject seekTarget;       // the targeted object - the robber/player
    Vehicle vehicleScript;              // the script of the movements
    Vector3 steer;                      // the steering vectors that are returned from the methods inside the Vehicle script
    public GameObject manager;          // the object that will be the "control center" of all the Vehicle script movements

	// Use this for initialization
	void Start ()
    {
        vehicleScript = this.GetComponent<Vehicle>();       // looks for the Vehicle script in the Vehicle object
        vehicleScript.maxSpeed = this.maxSpeed;             // sets the Vehicle object's max speed to this object's max speed
        vehicleScript.mass = this.seekWeight;               // sets the Vehicle object's mass to this object's weight of the cob (refer to the attribute comment)
        vehicleScript.target = this.seekTarget;             // sets the Vehicle object's target (the player) to this object's target (should still be the player)

        manager = GameObject.FindGameObjectWithTag("manager");      // the agent manager will have a tag called "manager"
	}
	
	// Update is called once per frame
	void Update ()
    {
        vehicleScript.target = this.seekTarget;         // constantly updates the target (had to do this when there were mutliple targets, but now that there is one this may change

        steer = vehicleScript.Pursue(seekTarget.transform.position, seekTarget.transform.position - this.transform.position);       // steer will have the vector of the target's position
        vehicleScript.ApplyForce(steer);            //will now chase the position of the target
	}
}
