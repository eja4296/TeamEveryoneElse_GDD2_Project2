using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Bullet class
public class Bullet : MonoBehaviour {
	// Attributes
	public Vector3 direction;
	public float bulletSpeed;
	public int bulletRange;

	private CharacterController charController;

	// Use this for initialization
	void Start () {
		//bulletModel = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		bulletRange = 60;
		bulletSpeed = 50f;
		charController = this.GetComponent<CharacterController> ();
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
		
		charController.Move (direction * Time.deltaTime * bulletSpeed);
		//this.transform.position += direction * Time.deltaTime * bulletSpeed;
		bulletRange--;
	}

	public void OnControllerColliderHit(ControllerColliderHit hit){
		
		if (hit.gameObject.name != "Robber" && hit.gameObject.name != "Cube" && hit.gameObject.name != "Bullet") {
			Destroy (this.gameObject);
			Debug.Log (hit.gameObject.name);
		}
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
