using UnityEngine;
using System.Collections;

public class ButtonAddEvents : MonoBehaviour {

	public GameObject panel;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void ActivatePanel() {
		panel.SetActive (true);
	}
}
