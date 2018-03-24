using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableUIPanel : MonoBehaviour {

	public GameObject controlsPanel;

	// Use this for initialization
	void Start () {
		controlsPanel = GameObject.Find ("ControlsPanel");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnClick(){
		//controlsPanel.gameObject.SetActive (false);
		controlsPanel.GetComponent<CanvasGroup> ().alpha = 0;
		controlsPanel.GetComponent<CanvasGroup> ().interactable = false;
		controlsPanel.GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}
}
