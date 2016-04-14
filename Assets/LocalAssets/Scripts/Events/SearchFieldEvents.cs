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

	private void ShowTrunk(string name, Vector3 coordinates)
	{	
		
		Instantiate (Trunk, coordinates, Trunk.transform.rotation);
	}

	private void ShowBranch(string name, Vector3 coordinates, Vector3 branchAngles)
	{
		GameObject branch = (GameObject)Instantiate (Branch, coordinates, Branch.transform.rotation);
		branch.transform.eulerAngles = branchAngles;
	}

	private void ShowLeaf(Vector3 coordinates, Vector3 angles)
	{
		// TODO: ubicar las hojas dentro de su respectiva rama. Esta es la tarea que sigue...
		GameObject leaf = (GameObject)Instantiate (Leaf, coordinates, Leaf.transform.rotation);
		Vector3 lol = leaf.transform.eulerAngles;
		lol.y = angles.y;
		leaf.transform.eulerAngles = lol;
	}

	private void ShowCompleteTrees()
	{

		foreach(Tree tree in trees) {
			Debug.Log("TrunkX: " + tree.trunk.name);
			Vector3 TrunkCoordinates = GenerateTrunkCoordinates ();
			ShowTrunk (tree.trunk.name, TrunkCoordinates);

			foreach (Branch branch in tree.trunk.branchs) {
				Debug.Log("BranchX: " + branch.name);
				Vector3 branchCoordinates = GenerateBranchCoordinates (TrunkCoordinates);
				Vector3 branchAngles = GenerateBranchRotation ();
				ShowBranch (branch.name, branchCoordinates, branchAngles);

				foreach (Leaf leaf in branch.leafs) {
					Debug.Log("LeafX: " + leaf.name);
					ShowLeaf (GenerateLeafCoordinates(branchCoordinates), branchAngles);
				}
			}
		}
		SetStatus (StatusBuildTree.Idle);
	}

	private Vector3 GenerateTrunkCoordinates() {
		float x = Random.Range (-90f, -10f);
		float z = Random.Range (100f, 200f);

		Vector3 coordinates = new Vector3 (x, 0f, z);

		return coordinates;
	}

	private Vector3 GenerateBranchCoordinates(Vector3 trunkCoordinates) {
		float y = Random.Range (0f, 4f);

		Vector3 coordinates = new Vector3 (trunkCoordinates.x, y, trunkCoordinates.z);

		return coordinates;
	}

	private Vector3 GenerateBranchRotation() {
		float y = Random.Range (0f, 350f);

		Vector3 eulerAngles = Branch.transform.eulerAngles;
		eulerAngles.y = y;

		return eulerAngles;
	
	}

	private Vector3 GenerateLeafCoordinates(Vector3 branchCoordinates) {
		// float x = branchCoordinates.x + 1f;
		float y = branchCoordinates.y + 3.53f;

		Vector3 coordinates = new Vector3 (branchCoordinates.x, y, branchCoordinates.z);

		return coordinates;
	}

	private void GenerateLeafRotation() {
		//c ll

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
