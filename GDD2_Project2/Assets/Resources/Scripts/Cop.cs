using System.Collections;
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
		Vector3 distance = player.position - this.transform.position;

		if (distance.magnitude < walkingDistance)
        {
            // Move the enemy towards the player 
			charController.SimpleMove (distance.normalized * smoothTime * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            print("You've been arrested.");
        }
    }
}
