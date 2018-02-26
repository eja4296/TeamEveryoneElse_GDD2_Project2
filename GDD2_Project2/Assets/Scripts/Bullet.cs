using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet class
public class Bullet : MonoBehaviour {
	// Attributes
	Vector3 velocity;
	public Vector3 direction;
	public float bulletSpeed;
	public int bulletRange;

	// Use this for initialization
	void Start () {
		//bulletModel = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		velocity = this.transform.forward;
		bulletRange = 60;
		bulletSpeed = 50f;
	}
	
	// Update is called once per frame
	void Update () {
		// Move the bullet each frame
		Move ();
		// If the bullet reaches it's max range, destory it
		if (bulletRange <= 0) {
			Destroy (this.gameObject);
		}
	}

	// Update bullet's position, increment range counter
	public void Move(){
		this.transform.position += direction * Time.deltaTime * bulletSpeed;
		bulletRange--;
	}
}
