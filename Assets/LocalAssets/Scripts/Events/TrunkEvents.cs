using UnityEngine;
using System.Collections;

public class TrunkEvents : MonoBehaviour {

	public Trunk trunk;
	public string trunk_name;

	// Use this for initialization
	void Start () {
		PlayerPrefs.SetString ("url", "http://ec2-54-213-189-135.us-west-2.compute.amazonaws.com/admin/bosque/tematica/" + 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
