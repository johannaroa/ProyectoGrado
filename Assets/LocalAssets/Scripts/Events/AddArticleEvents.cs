using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AddArticleEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LoadWebGUI() {
		PlayerPrefs.SetString ("url", "http://ec2-54-213-189-135.us-west-2.compute.amazonaws.com/admin/bosque/articulo/add/");
		SceneManager.LoadScene ("WebGUIAdmin");
	}
}
