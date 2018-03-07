﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop : MonoBehaviour
{
    // The target player
    public Transform player;        

    // The distance that the cop will walk towards the player
    public float walkingDistance = 10.0f;

    // The time it will take for the enemy to get to the player
    public float smoothTime = 10.0f;

    // Vector3 used to store velocity of enemy
    private Vector3 smoothVelocity = Vector3.zero;

	private CharacterController charController;

	// Use this for initialization
	void Start ()
    {
		charController = this.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Look at the player
        transform.LookAt(player);

        // Calculate distance between player
        //float distance = Vector3.Distance(transform.position, player.position);

		Vector3 distance = player.position - this.transform.position;

		if (distance.magnitude < walkingDistance)
        {
            // Move the enemy towards the player with smooth damp
            //transform.position = Vector3.SmoothDamp(transform.position, player.position, ref smoothVelocity, smoothTime);
			charController.SimpleMove (distance.normalized * smoothTime * Time.deltaTime);
        }
    }
}
