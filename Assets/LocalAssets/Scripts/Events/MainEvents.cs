using UnityEngine;
using System.Collections;

public class MainEvents : MonoBehaviour {

	public GameObject searchSpot;
	public GameObject forestCamera;
	private Vector3 tempPosition;

	void PreloadScene() {
		searchSpot = GameObject.Find ("SearchSpot");
		forestCamera = GameObject.Find ("ForestCamera");

		tempPosition = searchSpot.transform.position;
		forestCamera.transform.position = tempPosition;

		PlayerPrefs.GetString ("query");
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
