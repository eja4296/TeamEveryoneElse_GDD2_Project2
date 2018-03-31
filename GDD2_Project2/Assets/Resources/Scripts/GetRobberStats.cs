using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRobberStats : MonoBehaviour {
	int playerScore;
	float playerDistance;
	float playerTime;

	// Use this for initialization
	void Start () {
		playerScore = PlayerPrefs.GetInt ("score");
		playerDistance = PlayerPrefs.GetFloat ("distance");
		playerTime = PlayerPrefs.GetFloat ("time");
		this.GetComponent<Text> ().text = "Score: " + playerScore;
		this.GetComponent<Text> ().text += "\nDistance: " + playerDistance.ToString("n1") + "m";
		this.GetComponent<Text> ().text += "\nTime: " + playerTime.ToString("n2") + "s";

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
