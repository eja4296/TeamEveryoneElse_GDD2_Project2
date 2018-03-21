using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCam : MonoBehaviour {
    Camera minimapCam;
    Vector2 screenSize;
    public int camSize;
    // Use this for initialization
    void Start () {
        screenSize = new Vector2(Screen.width, Screen.height);
        minimapCam = transform.GetComponent<Camera>();
        minimapCam.pixelRect = new Rect(0,0, screenSize.x * .2f, screenSize.x * .2f);
        //minimapCam.orthographicSize = camSize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
