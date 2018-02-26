using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Camera Movement Class
// Camera follows movement of robber
public class CameraMovement : MonoBehaviour {
	// Attributes
	public GameObject robber;
	public float cameraHeight;
	public float cameraOffset;

	// Use this for initialization
	void Start () {
		cameraHeight = 20f;
		cameraOffset = -20f;
		this.transform.position = new Vector3 (0f, cameraHeight, cameraOffset);
	}
	
	// Update is called once per frame
	void Update () {
		// Update the camera's x and z position to match the robber's movement
		float robberX = robber.transform.position.x;
		float robberZ = robber.transform.position.z;
		float newCameraZ = robberZ + cameraOffset;
		this.transform.position = new Vector3 (robberX, cameraHeight, newCameraZ);
	}
}
