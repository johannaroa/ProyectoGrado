﻿using UnityEngine;
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
			step = speedCameraMovement * Time.deltaTime;
			objectToMoved.transform.position = Vector3.MoveTowards (objectToMoved.transform.position, objectToFinish.transform.position, step);

			if (objectToMoved.transform.position == objectToFinish.transform.position) {
				// TODO: Separar la activación y desactivación en otro metodo para poder meterlo aquí y que no se rompa la app
				applyMovement = false;
			}
		}
	}

	// TODO: mejorar este método ahora que defina bien el tema de organización de los Scripts
	public void SetApplyMovement() {

		GameObject search = GameObject.Find ("Search");
		GameObject credits = GameObject.Find ("Credits");
		GameObject creditsText = GameObject.Find ("CreditsText");
		GameObject back = GameObject.Find ("Back");

		search.GetComponent<Button> ().enabled = true;
		search.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
		search.GetComponentInChildren<Text>().color = Color.black;

		credits.GetComponent<Button> ().enabled = true;
		credits.GetComponentInChildren<CanvasRenderer>().SetAlpha(1);
		credits.GetComponentInChildren<Text>().color = Color.black;

		back.GetComponent<Button> ().enabled = false;
		back.GetComponent<Image>().enabled = false;
		back.GetComponentInChildren<Text>().enabled = false;

		Text scriptText = creditsText.GetComponent<Text>();
		Debug.Log (scriptText.enabled);
		scriptText.enabled = false;

		applyMovement = true;

	}
}
