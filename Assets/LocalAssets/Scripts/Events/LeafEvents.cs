using UnityEngine;
using System.Collections;

public class LeafEvents : MonoBehaviour {

	public GameObject articlePanel;

	void OnMouseDown() {

		GameObject canvas = GameObject.Find ("Canvas");
		GameObject articlePanelInstance = (GameObject)Instantiate (articlePanel);
		articlePanelInstance.transform.SetParent (canvas.transform);

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
