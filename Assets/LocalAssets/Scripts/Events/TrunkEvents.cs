using UnityEngine;
using System.Collections;

public class TrunkEvents : MonoBehaviour {

	public Trunk trunk;
	public string trunk_name;
	private string url;

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
