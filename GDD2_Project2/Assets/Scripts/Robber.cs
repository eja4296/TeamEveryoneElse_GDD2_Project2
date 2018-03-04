using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Robber class
// Inherit Character base class
public class Robber : Character {
	// Attributes
	public Camera camera;
	public float height;

	public List<GameObject> bullets;
	public bool shooting;
	public GameObject bulletPrefab;

	private CharacterController charController;

	// Use this for initialization
	override public void Start () {
		movementSpeed = 500f;
		rotationSpeed = 3f;
		direction = new Vector3 (0, 0, 1);
		height = 0.5f;
		bullets = new List<GameObject> ();
		shooting = false;
		charController = this.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();

		// Update bullets
		if (shooting && bullets.Count > 0) {
			for (int i = 0; i < bullets.Count; i++) {
				if (bullets [i] == null) {
					bullets.RemoveAt (i);
				}
			}
		} else {
			shooting = false;
		}
	}

	// Get User Input
	void GetInput(){
		// Get the mouse position and find where it hits in the game
		Vector3 mousePos = Input.mousePosition;
		Ray mouseRay = camera.ScreenPointToRay (mousePos);
		RaycastHit hit;

		if (Physics.Raycast (mouseRay, out hit, 100f)) {
			Vector3 targetDirection = hit.point;
			Vector3 newDirection = targetDirection - this.transform.position;

			// Don't move or rotate the robber if it is already at the mouse's position (or close to it)
			if (newDirection.magnitude > 2) {
				targetDirection.y = height;
				Rotate (targetDirection);
			}
		}

		if (Input.GetMouseButtonDown (0)) {
			Shoot ();
		}
			
		// Handle Movement
		// Forward (W)
		if(Input.GetKey(KeyCode.W)){
			Vector3 moveVec = new Vector3(0f, 0f, 1f) * movementSpeed;
			Move (moveVec);
		}
		// Backward (S)
		if(Input.GetKey(KeyCode.S)){
			Vector3 moveVec = new Vector3(0f, 0f, 1f) * -movementSpeed;
			Move (moveVec);
		}
		// Left (A)
		if(Input.GetKey(KeyCode.A)){
			Vector3 moveVec = new Vector3(1f, 0f, 0f) * -movementSpeed;
			Move (moveVec);
		}
		// Right (D)
		if(Input.GetKey(KeyCode.D)){
			Vector3 moveVec = new Vector3(1f, 0f, 0f) * movementSpeed;
			Move (moveVec);
		}

	}

	override public void Move(Vector3 movement){
		charController.SimpleMove (movement * Time.deltaTime);
	}

	// Rotate the robber based on mouse
	override public void Rotate(Vector3 rotation){
		this.transform.LookAt(rotation);	
	}

	// Shoot bullets
	override public void Shoot(){
		Vector3 bulletPosition = new Vector3 (this.transform.position.x, 0.5f, this.transform.position.z);
		GameObject newBullet = GameObject.Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
		newBullet.transform.position += (this.transform.forward * 1.25f);
		newBullet.AddComponent<Bullet> ();
		newBullet.GetComponent<Bullet> ().direction = new Vector3(this.transform.forward.x, 0f, this.transform.forward.z);
		bullets.Add (newBullet);
		shooting = true;
	}
		

}
