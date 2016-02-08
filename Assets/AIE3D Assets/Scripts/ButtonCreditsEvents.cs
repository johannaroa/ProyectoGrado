using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonCreditsEvents : MonoBehaviour {

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
		MoveToCredits ();
	}

	void MoveToCredits() {
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

		search.GetComponent<Button> ().enabled = false;
		search.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
		search.GetComponentInChildren<Text>().color = Color.clear;

		credits.GetComponent<Button> ().enabled = false;
		credits.GetComponentInChildren<CanvasRenderer>().SetAlpha(0);
		credits.GetComponentInChildren<Text>().color = Color.clear;

		back.GetComponent<Button> ().enabled = true;
		back.GetComponent<Image>().enabled = true;
		back.GetComponentInChildren<Text>().enabled = true;

		Text scriptText = creditsText.GetComponent<Text>();
		Debug.Log (scriptText.enabled);
		scriptText.enabled = true;

		applyMovement = true;

	}
}
