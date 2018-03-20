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

	public bool moving;

	private CharacterController charController;
	public AudioSource[] robberAudio;
	private AudioSource walkAudio;
	private AudioSource shootAudio;

	public Animator animator;

	// Use this for initialization
	override public void Start () {
		movementSpeed = 500f;
		rotationSpeed = 3f;
		direction = new Vector3 (0, 0, 1);
		height = 1.25f;
		bullets = new List<GameObject> ();
		shooting = false;
		charController = this.GetComponent<CharacterController> ();
		robberAudio = this.GetComponents<AudioSource> ();
		walkAudio = robberAudio [1];
		shootAudio = robberAudio [0];
		walkAudio.loop = true;
		moving = false;
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
		moving = false;

		// Get the mouse position and find where it hits in the game
		/*Vector3 mousePos = Input.mousePosition;
		Ray mouseRay = camera.ScreenPointToRay (mousePos);
		RaycastHit hit;

		if (Physics.Raycast (mouseRay, out hit, 100f)) {
			Vector3 targetDirection = hit.point;
			Vector3 newDirection = targetDirection - this.transform.position;

			// Don't move or rotate the robber if it is already at the mouse's position (or close to it)
			if (newDirection.magnitude > 2) {
				targetDirection.y = 0f;
				Rotate (targetDirection);
			}
		}
        */
		if (Input.GetMouseButtonDown (0)) {
			Shoot ();
		}
			
		// Handle Movement
		// Forward (W)
		if(Input.GetKey(KeyCode.W)){
			Vector3 moveVec = transform.forward * movementSpeed;
			Move (moveVec);
			moving = true;
			animator.Play("Move");
		}
		// Backward (S)
		if(Input.GetKey(KeyCode.S)){
			Vector3 moveVec = transform.forward  * -movementSpeed;
			Move (moveVec);
			moving = true;
			animator.Play("Move");
		}
		// Left (A)
		if(Input.GetKey(KeyCode.A)){
			Vector3 moveVec = transform.right * -movementSpeed;
			Move (moveVec);
			moving = true;
			animator.Play("Move");
		}
		// Right (D)
		if(Input.GetKey(KeyCode.D)){
			Vector3 moveVec = transform.right * movementSpeed;
			Move (moveVec);
			moving = true;
			animator.Play("Move");
		}

		if (moving && walkAudio.isPlaying == false) {
			walkAudio.Play ();
		}
		else if (!moving) {
			walkAudio.Stop ();
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
		Vector3 bulletPosition = new Vector3 (this.transform.position.x, height, this.transform.position.z);
		GameObject newBullet = GameObject.Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
		newBullet.transform.position += (this.transform.forward);
		newBullet.AddComponent<Bullet> ();
		newBullet.GetComponent<Bullet> ().direction = new Vector3(this.transform.forward.x, 0f, this.transform.forward.z);
		bullets.Add (newBullet);
		shooting = true;
		shootAudio.Play ();
	}
}
