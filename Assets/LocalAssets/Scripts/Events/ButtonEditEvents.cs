using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonEditEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadWebGUI() {
		SceneManager.LoadScene ("WebGUIAdmin");
	}
}
