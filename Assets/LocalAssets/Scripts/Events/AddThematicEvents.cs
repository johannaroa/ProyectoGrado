using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AddThematicEvents : MonoBehaviour {

	private string url;

	// Use this for initialization
	void Start () {
		url = GameObject.Find ("GlobalManager").GetComponent<GlobalManager> ().url;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadWebGUI() {
		PlayerPrefs.SetString ("url", url + "/admin/bosque/revision/add/");
		SceneManager.LoadScene ("WebGUIAdmin");
	}
}
