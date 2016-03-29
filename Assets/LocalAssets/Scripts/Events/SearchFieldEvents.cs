using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SearchFieldEvents : MonoBehaviour {

	public GameObject Tree;
	private bool InitProcess = false;
	private List<Tree> trees = new List<Tree> ();

	private void GetArticles(string query)
	{
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.SearchArticle(query));

		InitProcess = true;
	}

	void GetCategories(ArticleSerializable leaf) {

		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.GetCategories (leaf.categories_link));
	}

	void GetThematic(CategorySerializable branch) {
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.GetThematics(branch.thematic_link));
	}

	public IEnumerator BuildStructureTree(ArticleSerializable[] articles) {

		for (int i=0; i < articles.Length; i++) {

			GetCategories (articles[i]);
			yield return new WaitForSeconds (1f);

			for (int j=0; j < APIRestClient.categories.Length; j++) {

				GetThematic (APIRestClient.categories[j]);
				yield return new WaitForSeconds (1f);

				Leaf leaf = new Leaf (articles [i].titulo, articles [i].id);

				Branch branch = FindExistingCategory (trees, APIRestClient.categories [j].id);

				if (branch == null) {
					branch = new Branch (APIRestClient.categories [j].nombre, APIRestClient.categories [j].id, leaf);
				}

				Trunk trunk = FindExistingThematic (trees, APIRestClient.thematics [0].id);

				if (branch == null) {
					trunk = new Trunk (APIRestClient.thematics [0].nombre, APIRestClient.thematics [0].id, branch);
				} 

				Tree tree = new Tree (trunk);

				trees.Add (tree);

				Debug.Log (
					"TRUNK: " + APIRestClient.thematics [0].nombre + " BRANCH: " + APIRestClient.categories [j].nombre + " LEAF: " + articles [i].titulo 
				);

			}
		}

		Debug.Log (trees.Count);

	}

	private void FindExistingCategory(List<Tree> trees, int branch_id) {
		for (int i = 0; trees.Count <= i; i++) {
			for (int j = 0; trees[i].trunk.branchs.Count <= j; j++) {
				if (trees[i].trunk.branchs[j].id == branch_id) {
					return trees [i].trunk.branchs [j];
				}
			}
		}

		return null;
	}

	private void FindExistingThematic(List<Tree> trees, int trunk_id) {
		
		for(int i=0; trees.Count <= i; i++) {

			if (trees [i].trunk.id == trunk_id) {
				return trees [i].trunk;
			}

			return null;
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
		se.AddListener(GetArticles);
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
