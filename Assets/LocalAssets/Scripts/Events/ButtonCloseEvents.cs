using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonCloseEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Deactivate (GameObject articlePanel) {
		articlePanel.GetComponent<Image> ().enabled = false;
		Text[] texts = articlePanel.GetComponentsInChildren<Text> ();
		Image[] images = articlePanel.GetComponentsInChildren<Image> ();

		foreach (Image image in images) {
			image.enabled = false;
		}

		foreach (Text text in texts) {
			text.enabled = false;

			if (text.transform.name == "CategoriesContent") {
				text.text = "";
			}
		}

	}

	public void Close() {
		GameObject articlePanel = GameObject.Find ("ArticlePanel");
		Deactivate (articlePanel);
	}

}
