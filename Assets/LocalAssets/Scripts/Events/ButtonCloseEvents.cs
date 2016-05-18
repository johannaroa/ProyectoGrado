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

	void Deactivate (GameObject panel) {
		panel.GetComponent<Image> ().enabled = false;
		Text[] texts = panel.GetComponentsInChildren<Text> ();
		Image[] images = panel.GetComponentsInChildren<Image> ();

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

	public void CloseLeafPanel() {
		GameObject panel = GameObject.Find ("ArticlePanel");
		Deactivate (panel);
	}

	public void CloseBranchPanel() {
		GameObject panel = GameObject.Find ("CategoryPanel");
		Deactivate (panel);
	}

	public void CloseTrunkPanel() {
		GameObject panel = GameObject.Find ("ThematicPanel");
		Deactivate (panel);
	}
}
