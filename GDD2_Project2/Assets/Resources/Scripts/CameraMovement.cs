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
    public GameObject target;

    public float rotateSpeed = 5;
    //Vector3 offset;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        //offset = target.transform.position - transform.position;
        this.transform.position = new Vector3 (0f, cameraHeight, cameraOffset);
	}
    //look camera code from code.tutsplus.com 
    void Update()
    {
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;

        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

        target.transform.Rotate(0, horizontal, 0);

        float desiredAngle = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = target.transform.position - (rotation* new Vector3(0f, -cameraHeight, cameraOffset));

        transform.LookAt(target.transform);
    }

}
