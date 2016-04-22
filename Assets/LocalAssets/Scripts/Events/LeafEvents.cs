using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LeafEvents : MonoBehaviour {

	void Activate (GameObject articlePanel) {
		articlePanel.GetComponent<Image> ().enabled = true;
		Text[] texts = articlePanel.GetComponentsInChildren<Text> ();

		foreach (Text text in texts) {
			text.enabled = true;
		}
	}

	void Deactivate (GameObject articlePanel) {
		articlePanel.GetComponent<Image> ().enabled = false;
		Text[] texts = articlePanel.GetComponentsInChildren<Text> ();

		foreach (Text text in texts) {
			text.enabled = false;
		}
	}

	void OnMouseDown() {
		GameObject articlePanel = GameObject.Find ("ArticlePanel");
		Activate (articlePanel);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
