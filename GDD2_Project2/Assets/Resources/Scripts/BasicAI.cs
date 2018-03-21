using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAI : MonoBehaviour
{
    public Transform target;        // enemy's target
    public int moveSpeed = 3;       // enemy's move speed
    public float range = 30f;       // enemy's range within the target will be detected
    public int rotationSpeed = 3;   // enemy's speed of turning
    public float stop = 0f;
    private Transform myTransform;  // enemy's current transform

	void Awake ()
    {
        target = GameObject.FindWithTag("Player").transform;    // target the player
        myTransform = transform;        // cache transform data for easy access/performance
	}
	
	// Update is called once per frame
	void Update ()
    {
        float distance = Vector3.Distance(myTransform.position, target.position);

        if (distance <= range)
        {
            // look
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

            if (distance > stop)
                myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }
	}
}
