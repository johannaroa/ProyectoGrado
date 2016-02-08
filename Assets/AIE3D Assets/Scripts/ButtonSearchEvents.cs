using UnityEngine;
using System.Collections;

public class ButtonSearchEvents : MonoBehaviour {

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
		MoveToMainSearch ();
	}

	void MoveToMainSearch() {
		if (applyMovement == true) {
			step = speedCameraMovement * Time.deltaTime;
			objectToMoved.transform.position = Vector3.MoveTowards (objectToMoved.transform.position, objectToFinish.transform.position, step);

			if (objectToMoved.transform.position == objectToFinish.transform.position) {
				// TODO: Separar la activación y desactivación en otro metodo para poder meterlo aquí y que no se rompa la app
				applyMovement = false;
			}
		}
	}

	public void SetApplyMovement() {

		applyMovement = true;

	}
}
