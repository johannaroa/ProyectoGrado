using UnityEngine;
using System.Collections;

public class BackFromPanelEvents : MonoBehaviour {

	public GameObject panel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void DeactivatePanel() {
		panel.SetActive (false);
	}
}
