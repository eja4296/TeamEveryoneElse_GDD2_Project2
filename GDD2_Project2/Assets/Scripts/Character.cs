using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Character Base Class
abstract public class Character : MonoBehaviour {
	// Attributes
	public float movementSpeed;
	public float rotationSpeed;
	public Vector3 direction;


	// Use this for initialization
	virtual public void Start () {
		movementSpeed = 1f;
		rotationSpeed = 1f;
		direction = new Vector3 (0, 0, 1);
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}

	// Move function
	protected void Move(Vector3 movement){
		this.transform.position += movement;
	}

	// Rotate function
	// Must be overridden
	abstract public void Rotate (Vector3 rotation);

	abstract public void Shoot ();
}
