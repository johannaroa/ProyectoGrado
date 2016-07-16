using UnityEngine;
using System.Collections;

public class ButtonDeleteEvents : MonoBehaviour {

	public void DeleteLeaf () {
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		int id = PlayerPrefs.GetInt("leafId");
		StartCoroutine (apiRestClient.DeleteArticle(id));

		GameObject[] leavesWithTag = GameObject.FindGameObjectsWithTag ("Leaf");

		foreach(GameObject leafWithTag in leavesWithTag) {
			if (leafWithTag.name == "Leaf3(Clone)-id" + id) {
				Destroy(leafWithTag);
			}
		}

	}

	public void DeleteBranch() {
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;
	
		int id = PlayerPrefs.GetInt ("branchId");
		StartCoroutine (apiRestClient.DeleteCategory(id));

		Destroy(GameObject.Find ("Branch1(Clone)-id" + id));
	}

	public void DeleteTrunk() {
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		int id = PlayerPrefs.GetInt ("trunkId");
		StartCoroutine (apiRestClient.DeleteThematic(id));

		Destroy(GameObject.Find ("Trunk(Clone)-id" + id));
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
