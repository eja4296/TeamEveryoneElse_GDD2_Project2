using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetRobberScore : MonoBehaviour {

	public GameObject robber;
	//public Text uiText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		this.GetComponent<Text> ().text = "Score: " + robber.GetComponent<Robber> ().score;
		this.GetComponent<Text>().text += "\nDistance: " + robber.GetComponent<Robber> ().distance.ToString("n1") + "m";
		this.GetComponent<Text>().text += "\nTime: " + robber.GetComponent<Robber> ().time.ToString("n2") + "s";
        this.GetComponent<Text>().text += "\nBullets: " + robber.GetComponent<Robber>().shots;
	}
}
