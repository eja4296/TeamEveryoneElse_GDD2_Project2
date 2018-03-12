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
		    
		this.transform.position = new Vector3 (0f, cameraHeight, cameraOffset);
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = robber.transform.position + new Vector3(0f, cameraHeight, cameraOffset);
        /*Vector3 currentTarget = robber.transform.position;

		Vector3 distance = currentTarget - this.transform.position;

		distance.y = 0f;
		distance.z += cameraOffset;

		if (distance.magnitude > 2) {
			

			distance.Normalize ();

			distance *= Time.deltaTime;

			this.transform.position += distance * 20f;
		}
        */

        /*
		if (distance.magnitude > 5) {
			distance.Normalize ();
			this.transform.position += distance * Time.deltaTime * 5f;


			float robberX = robber.transform.position.x;
			float robberZ = robber.transform.position.z;
			float newCameraZ = robberZ + cameraOffset;
			Vector3 targetVec = new Vector3 (robberX, cameraHeight, newCameraZ);
			//Vector3 velocity = Vector3.zero;
			//this.transform.position = Vector3.SmoothDamp (this.transform.position, targetVec, ref velocity, 10f);
			this.transform.position = targetVec * Time.deltaTime;

		}
		*/

    }
}
