using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SearchFieldEvents : MonoBehaviour {

	public GameObject Tree;
	private bool InitProcess = false;

	private void PerformSearch(string query)
	{
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.SearchArticle(query));

		InitProcess = true;
	}

	void PullBranch(ArticleSerializable leaf) {

		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.GetCategories (leaf.categories_link));
	}

	void PullTrunk(CategorySerializable branch) {
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.GetThematics(branch.thematic_link));
	}

	public IEnumerator BuildStructureTree(ArticleSerializable[] articles) {

		for (int i=0; i < articles.Length; i++) {

			Debug.Log ("Leaf: " + articles[i].titulo + " " + articles[i].categories_link);
			PullBranch (articles[i]);
			yield return new WaitForSeconds (1f);

			for (int j=0; j < APIRestClient.categories.Length; j++) {

				Debug.Log ("Branch: " + APIRestClient.categories[j].nombre);
				PullTrunk (APIRestClient.categories[j]);
				yield return new WaitForSeconds (1f);

				Debug.Log (APIRestClient.thematics[0].nombre);
			}
		}

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
		if (APIRestClient.resultsAPI.Length > 0 && InitProcess == true) {

			InitProcess = false;
			StartCoroutine(BuildStructureTree (APIRestClient.resultsAPI));

		}
	}

}
