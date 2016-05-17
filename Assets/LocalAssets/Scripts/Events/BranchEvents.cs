using UnityEngine;
using System.Collections;

public class BranchEvents : MonoBehaviour {

	public Branch branch;
	public string branch_name;
	private string url;

	// Use this for initialization
	void Start () {
		url = GameObject.Find ("GlobalManager").GetComponent<GlobalManager> ().url;

		PlayerPrefs.SetString ("url", url + "/admin/bosque/categoria/" + 1);
		PlayerPrefs.SetInt ("branchId", branch.id);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
