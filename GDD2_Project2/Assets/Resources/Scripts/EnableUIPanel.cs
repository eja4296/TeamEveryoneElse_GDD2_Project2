using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableUIPanel : MonoBehaviour {
	public GameObject controlsPanel;

	// Use this for initialization
	void Start () {
		controlsPanel = GameObject.Find ("ControlsPanel");
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnClick(){
		//controlsPanel.gameObject.SetActive (true);
		controlsPanel.GetComponent<CanvasGroup> ().alpha = 1;
		controlsPanel.GetComponent<CanvasGroup> ().interactable = true;
		controlsPanel.GetComponent<CanvasGroup> ().blocksRaycasts = true;
	}
}
