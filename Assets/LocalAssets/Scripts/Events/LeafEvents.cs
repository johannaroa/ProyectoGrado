using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class LeafEvents : MonoBehaviour {

	public Leaf leaf;
	public string leaf_name;

	void Activate (GameObject articlePanel) {
		articlePanel.GetComponent<Image> ().enabled = true;
		Text[] texts = articlePanel.GetComponentsInChildren<Text> ();

		foreach (Text text in texts) {
			text.enabled = true;

			if (text.transform.name == "TitleContent") {
				text.text = leaf.name;
			} else
				if (text.transform.name == "DescriptionContent") {
					text.text = leaf.description;	
				} else
					if (text.transform.name == "CategoriesContent") {
						foreach (Branch branch in leaf.branchs) {
							text.text += "* " + branch.name + "\n";
						}
					}
		}
	}

	void Deactivate (GameObject articlePanel) {
		articlePanel.GetComponent<Image> ().enabled = false;
		Text[] texts = articlePanel.GetComponentsInChildren<Text> ();

		foreach (Text text in texts) {
			text.enabled = false;

			if (text.transform.name == "CategoriesContent") {
				text.text = "";
			}
		}

	}

	void OnMouseEnter() {
		GameObject articlePanel = GameObject.Find ("ArticlePanel");
		Activate (articlePanel);
	}

	void OnMouseExit() {
		GameObject articlePanel = GameObject.Find ("ArticlePanel");
		Deactivate (articlePanel);
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
