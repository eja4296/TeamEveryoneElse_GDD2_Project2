using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop : MonoBehaviour
{
    // The target player
    GameObject player;
    //the path that the cop patrols
    List<GameObject> patrolPath;

    //the chunk that the cop spawns in
    public GameObject parentChunk;

	private CharacterController charController;

	// Use this for initialization
	void Start ()
    {
		charController = this.GetComponent<CharacterController> ();

        player = GameObject.Find("Robber");
	}
	
	// Update is called once per frame
	void Update ()
    {

    }
}
