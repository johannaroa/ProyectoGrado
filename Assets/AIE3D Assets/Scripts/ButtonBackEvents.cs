using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonBackEvents : MonoBehaviour {

	public float speedCameraMovement = 20;
	public GameObject objectToMoved;
	public GameObject objectToFinish;

	private bool applyMovement = false;
	private float step;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MoveToMainMenu ();
	}

	void MoveToMainMenu() {
		if (applyMovement == true) {

			Debug.Log ("Eso es monda");

			step = speedCameraMovement * Time.deltaTime;
			objectToMoved.transform.position = Vector3.MoveTowards (objectToMoved.transform.position, objectToFinish.transform.position, step);
		}

	}

	// TODO: mejorar este método ahora que defina bien el tema de organización de los Scripts
	public void SetApplyMovement() {

//		GameObject search = GameObject.Find ("Search");
//		GameObject credits = GameObject.Find ("Credits");
//		GameObject creditsText = GameObject.Find ("CreditsText");
//
//		search.SetActive (false);
//		credits.SetActive (false);
//
//		Text scriptText = creditsText.GetComponent<Text>();
//		scriptText.enabled = true;

		applyMovement = true;

	}
}
