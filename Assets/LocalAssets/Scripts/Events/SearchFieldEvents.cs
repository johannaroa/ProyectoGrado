using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SearchFieldEvents : MonoBehaviour {

	public GameObject Trunk;
	public GameObject Branch;
	public GameObject Leaf;
	private List<Tree> trees = new List<Tree> ();

	enum StatusBuildTree {Failed, Completed, Init, InProcess, Idle};
	StatusBuildTree status = StatusBuildTree.Idle;

	Vector3 coordenates;
	Quaternion cam_rotation;

	// TODO: atendiendo al principio de la ocultación de la información, este métode debería recibir un parametro que no sea de tipo enum
	// precisamente para ocultar el tipo de estructura de datos que se maneja por debajo
	private void SetStatus(StatusBuildTree new_status) {
		status = new_status;
	}

	private void GetArticles(string query)
	{
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.SearchArticle(query));

		SetStatus(StatusBuildTree.Init);
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

		for (int i = 0; i < articles.Length; i++) {

			GetCategories (articles[i]);
			yield return new WaitForSeconds (1f);

			for (int j = 0; j < APIRestClient.categories.Length; j++) {

				GetThematic (APIRestClient.categories[j]);
				yield return new WaitForSeconds (1f);

				Leaf leaf = new Leaf (articles [i].titulo, articles [i].id);

				Branch branch = FindExistingCategory (trees, APIRestClient.categories [j].id);

				if (branch == null) {
					branch = new Branch (APIRestClient.categories [j].nombre, APIRestClient.categories [j].id, leaf);
				} else {
					branch.leafs.Add (leaf);
				}

				Trunk trunk = FindExistingThematic (trees, APIRestClient.thematics [0].id);

				if (trunk == null) {
					trunk = new Trunk (APIRestClient.thematics [0].nombre, APIRestClient.thematics [0].id, branch);
					Tree tree = new Tree (trunk);
					trees.Add (tree);
				} else {

					Branch existing_branch = trunk.branchs.Find (x => branch.id == x.id);

					if (existing_branch == null) {
						trunk.branchs.Add (branch);
					}
				}
//
//				Debug.Log (
//					"TRUNK: " + APIRestClient.thematics [0].nombre + " BRANCH: " + APIRestClient.categories [j].nombre + " LEAF: " + articles [i].titulo 
//				);

			}
		}

		SetStatus(StatusBuildTree.Completed);
	}

	private Branch FindExistingCategory(List<Tree> trees, int branch_id) {

		for (int i = 0; i < trees.Count; i++) {
			for (int j = 0; j < trees[i].trunk.branchs.Count; j++) {
				if (trees[i].trunk.branchs[j].id == branch_id) {
					return trees [i].trunk.branchs [j];
				}
			}
		}

		return null;
	}

	private Trunk FindExistingThematic(List<Tree> trees, int trunk_id) {
		
		for(int i = 0; i < trees.Count; i++) {
			if (trees [i].trunk.id == trunk_id) {
				return trees [i].trunk;
			}
		}
		return null;
	}

	private void ShowTrunk(string name)
	{
		Instantiate (Trunk, coordenates, Trunk.transform.rotation);
	}

	private void ShowBranch()
	{
		Instantiate (Branch, coordenates, cam_rotation);
	}

	private void ShowLeaf()
	{
		Instantiate (Leaf, coordenates, cam_rotation);
	}

	private void ShowCompleteTrees()
	{

		SetCameraValues ();

		foreach(Tree tree in trees) {
			Debug.Log("TrunkX: " + tree.trunk.name);
			ShowTrunk (tree.trunk.name);

			foreach (Branch branch in tree.trunk.branchs) {
				Debug.Log("BranchX: " + branch.name);
				ShowBranch ();

				foreach (Leaf leaf in branch.leafs) {
					Debug.Log("LeafX: " + leaf.name);
					ShowLeaf ();
				}
			}
		}
		SetStatus (StatusBuildTree.Idle);
	}

	private void SetCameraValues () {
		GameObject camera = GameObject.Find ("ForestCamera");
		coordenates = camera.transform.position + camera.transform.forward * 30;
		coordenates.y = 0f;
		cam_rotation = camera.transform.rotation;
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
		if (APIRestClient.resultsAPI.Length > 0 && (int)status == 2) {

			StartCoroutine(BuildStructureTree (APIRestClient.resultsAPI));
			SetStatus (StatusBuildTree.InProcess);
		}

		if ((int)status == 1) {
		
			ShowCompleteTrees ();

		}
	}

}
