using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TrunkEvents : MonoBehaviour {

	public Trunk trunk;
	public string trunk_name;
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
				text.text = trunk.name;
			} else
				if (text.transform.name == "DescriptionContent") {
					text.text = trunk.description;	
				}
		}
	}

	void OnMouseDown() {
		GameObject thematicPanel = GameObject.Find ("ThematicPanel");
		Activate (thematicPanel);
	}

	// Use this for initialization
	void Start () {
		url = GameObject.Find ("GlobalManager").GetComponent<GlobalManager> ().url;

		PlayerPrefs.SetString ("url", url + "/admin/bosque/tematica/" + 1);
		PlayerPrefs.SetInt ("trunkId", trunk.id);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
