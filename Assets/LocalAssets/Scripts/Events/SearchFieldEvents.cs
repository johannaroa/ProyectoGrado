using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SearchFieldEvents : MonoBehaviour {

	public GameObject Tree;

	private void PerformSearch(string query)
	{
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.SearchArticle(query));
	}

	void PullLeafs(ArticleSerializable[] leafs) {
		for (int i=0; i < leafs.Length; i++) {

			Debug.Log ("Leaf: " + leafs[i]);
			PullBranch (leafs[i]);
		}
	}

	void PullBranch(ArticleSerializable leaf) {

		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.GetCategories (leaf.categories_link));

		for (int i=0; i < APIRestClient.categories.Length; i++) {

			Debug.Log ("Branch: " + APIRestClient.categories[i]);
			PullTrunk (APIRestClient.categories[i]);
		}
	}

	void PullTrunk(CategorySerializable branch) {
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.GetThematics(branch.thematic_link));
		Debug.Log (APIRestClient.thematics);

	}

	public void BuildTree(ArticleSerializable[] articles) {

		PullLeafs (articles);

	}

	private void ShowTrunk()
	{
	
	
	}

	private void ShowBranch()
	{
	}

	private void ShowLeaf()
	{
	}

	private void ShowCompleteTree()
	{
	}

	private void ShowCompleteTrees()
	{
	}

	private void InitListener() 
	{
		// TODO: Probar si en vez de poner esto aquí, que resultado da usar el evento OnEndEdit (en el GUI)
		InputField input = gameObject.GetComponent<InputField>();
		var se = new InputField.SubmitEvent();
		se.AddListener(PerformSearch);
		input.onEndEdit = se;
	}

	// Use this for initialization
	void Start () 
	{
		InitListener ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (APIRestClient.resultsAPI.Length > 0) {

			BuildTree (APIRestClient.resultsAPI);

		}
	}

}
