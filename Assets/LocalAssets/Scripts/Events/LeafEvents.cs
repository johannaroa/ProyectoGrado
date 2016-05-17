using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class LeafEvents : MonoBehaviour {

	public Leaf leaf;
	public string leaf_name;
	private string url;

	void Activate (GameObject articlePanel) {
		articlePanel.GetComponent<Image> ().enabled = true;
		Text[] texts = articlePanel.GetComponentsInChildren<Text> ();
		Image[] images = articlePanel.GetComponentsInChildren<Image> ();

		foreach (Image image in images) {
			image.enabled = true;
		}

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

	void OnMouseDown() {
		GameObject articlePanel = GameObject.Find ("ArticlePanel");
		Activate (articlePanel);
		print (PlayerPrefs.GetString("url"));
	}

	// Use this for initialization
	void Start () {
		url = GameObject.Find ("GlobalManager").GetComponent<GlobalManager> ().url;
	
		PlayerPrefs.SetString ("url", url + "/admin/bosque/articulo/" + leaf.id);
		PlayerPrefs.SetInt ("leafId", leaf.id);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
