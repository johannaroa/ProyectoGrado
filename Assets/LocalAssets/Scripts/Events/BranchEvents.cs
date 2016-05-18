using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BranchEvents : MonoBehaviour {

	public Branch branch;
	public string branch_name;
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
				text.text = branch.name;
			} else
				if (text.transform.name == "DescriptionContent") {
					text.text = branch.description;	
				} else
					if (text.transform.name == "CategoriesContent") {
						text.text = branch.trunk.name;
					}
		}
	}

	void OnMouseDown() {
		GameObject categoryPanel = GameObject.Find ("CategoryPanel");
		Activate (categoryPanel);
	}

	// Use this for initialization
	void Start () {
		url = GameObject.Find ("GlobalManager").GetComponent<GlobalManager> ().url;

		PlayerPrefs.SetString ("url", url + "/admin/bosque/categoria/" + 1);
		PlayerPrefs.SetInt ("branchId", branch.id);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
