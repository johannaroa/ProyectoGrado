using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour {

	public string url;
	private string query;
	private Vector3 tempPosition;

	void PreloadScene() {

		query = PlayerPrefs.GetString ("query");

		if (query.Length > 0 && PlayerPrefs.GetInt("fromMain") == 0) {
			tempPosition = GameObject.Find ("SearchSpot").transform.position;
			GameObject.Find ("ForestCamera").transform.position = tempPosition;
			GameObject.Find ("SearchField").GetComponent<SearchFieldEvents>().GetArticles(query);
			GameObject.Find ("MenuManager").GetComponent<ButtonSearchEvents> ().SetApplyMovement ();
		}
	}

	// Use this for initialization
	void Start () {
		PreloadScene ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnApplicationQuit() {
		PlayerPrefs.SetString ("query", null);
	}
}
