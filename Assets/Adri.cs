using UnityEngine;
using System.Collections;

public class Adri : MonoBehaviour {


	public Transform cube1;
	public Transform cube2;
	private LineRenderer line;

	protected void Update()
	{
		float distance = Vector3.Distance(cube1.position, cube2.position);

		transform.position = cube1.position + 0.5f * (cube2.position - cube1.position);
		transform.localScale = new Vector3(distance, 1f, 1f);
		transform.LookAt(cube2);
	}

//	public Transform startMarker;
//	public Transform endMarker;
//	public float speed = 1.0F;
//	private float startTime;
//	private float journeyLength;
//
//	void Start() {
//		startTime = Time.time;
//		journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
//	}
//	void Update() {
//		float distCovered = (Time.time - startTime) * speed;
//		float fracJourney = distCovered / journeyLength;
//		transform.position = Vector3.Lerp(startMarker.position, endMarker.position, 0.8f);
//	}

//	public Transform first;
//	public Transform last;
//
//	// Use this for initialization
//	void Start () {
//		Vector3 objectScale = transform.localScale;
//		float distance = Vector3.Distance(last.position, first.position);
//		print (distance);
//		Vector3 newScale = new Vector3(objectScale.x, distance, objectScale.z);
//		transform.localScale = Vector3.Lerp(first.position, last.position, 0.5f);
//	}
//	
//	// Update is called once per frame
//	void Update () {
//
//	}
}
