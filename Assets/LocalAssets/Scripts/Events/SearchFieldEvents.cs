﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SearchFieldEvents : MonoBehaviour {

	public GameObject PrefabTree;
	public GameObject PrefabTrunk;
	public GameObject PrefabBranch;
	public GameObject PrefabLeaf;
	public GameObject PrefabLiana;
	public GameObject PrefabLoading;

	private List<Leaf> leaves;
	// Valores no permitidos como vertices firmes: 0, 1, 11, 12, 22, 23, 33, 34
	// 25, 35, estos si quedan ubicados y se puede clickear pero quedan feos
	int[] ValidVerticesChoices = new int[] {2, 3, 4, 5, 6, 7, 8, 9, 10, 13, 14, 15, 16, 17, 18, 19, 20, 21, 24, 26, 27, 28, 29, 30, 31, 32, 36, 37, 38, 39, 40, 41, 42, 43};

	enum StatusBuildTree {Failed, Completed, Init, InProcess, Idle};
	StatusBuildTree status = StatusBuildTree.Idle;

	// TODO: atendiendo al principio de la ocultación de la información, este métode debería recibir un parametro que no sea de tipo enum
	// precisamente para ocultar el tipo de estructura de datos que se maneja por debajo
	private void SetStatus(StatusBuildTree new_status) {
		status = new_status;
	}

	private void DestroyPrevForest() {
		GameObject[] forest = GameObject.FindGameObjectsWithTag("GenerateTree");
		foreach(GameObject tree in forest) {
			Destroy (tree);
		}
	}

	public void GetArticles(string query)
	{
		DestroyPrevForest ();
		PlayerPrefs.SetString ("query", query);
		PlayerPrefs.SetInt ("fromMain", 1);

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
		leaves = new List<Leaf> ();

		for (int i = 0; i < articles.Length; i++) {

			GetCategories (articles[i]);
			yield return new WaitForSeconds (1f);

			for (int j = 0; j < APIRestClient.categories.Length; j++) {

				GetThematic (APIRestClient.categories[j]);
				yield return new WaitForSeconds (1f);

				TreePlus tree = new TreePlus ("arbol");

				Trunk trunk = new Trunk (APIRestClient.thematics [0].id, APIRestClient.thematics [0].nombre, APIRestClient.thematics[0].descripcion, tree);

				Branch branch = new Branch (APIRestClient.categories [j].id, APIRestClient.categories [j].nombre, APIRestClient.categories[j].descripcion, trunk);

				Leaf leaf = FindExistingArticle(leaves, articles [i].id);
				if (leaf == null) {
					leaf = new Leaf (articles [i].id, articles [i].titulo, articles [i].contenido, branch);
					leaves.Add (leaf);
				} else {
					leaf.branchs.Add(branch);
				}
					
			}
		}

		SetStatus(StatusBuildTree.Completed);
	}

	private Leaf FindExistingArticle(List<Leaf> leaves, int leaf_id) {
		for(int i = 0; i < leaves.Count; i++) {
			if (leaves[i].id == leaf_id) {
				return leaves [i];
			}
		}
		return null;
	}

	private GameObject ShowTree(Vector3 coordinates) {
		GameObject tree = (GameObject)Instantiate (PrefabTree, coordinates, Quaternion.identity);

		return tree;
	}

	private GameObject ShowTrunk(Trunk new_trunk, Vector3 coordinates, GameObject tree)
	{	
		
		GameObject trunk = (GameObject)Instantiate (PrefabTrunk, coordinates, PrefabTrunk.transform.rotation);
		trunk.name = trunk.name + "-id" + new_trunk.id;
		trunk.transform.SetParent (tree.transform);

		TrunkEvents trunkEvents = trunk.GetComponent<TrunkEvents> ();
		trunkEvents.trunk = new_trunk;
		trunkEvents.trunk_name = new_trunk.name;

		return trunk;
	}

	private GameObject ShowBranch(Branch new_branch, Vector3 coordinates, Vector3 branchAngles, GameObject trunk)
	{

		GameObject branch = (GameObject)Instantiate (PrefabBranch, coordinates, PrefabBranch.transform.rotation);

		branch.name = branch.name + "-id" + new_branch.id;
		branch.transform.eulerAngles = branchAngles;
		branch.transform.SetParent (trunk.transform);
	
		BranchEvents branchEvents = branch.GetComponent<BranchEvents> ();
		branchEvents.branch = new_branch;
		branchEvents.branch_name = new_branch.name;

		return branch;
	}

	private GameObject ShowLeaf(Leaf new_leaf, Vector3 coordinates, Vector3 branchAngles, GameObject branch)
	{
		Vector3[] vertices = branch.GetComponent<MeshFilter>().mesh.vertices;
		int VerticeRandom = ValidVerticesChoices[Random.Range (0, ValidVerticesChoices.Length)];
		// print ("Vexter index: " + vertice_random + " Leaf name: " + new_leaf.name);

		Vector3 vectorGlobal = branch.transform.TransformPoint (vertices [VerticeRandom]);
		vectorGlobal.y = vectorGlobal.y + 0.4f;

		GameObject leaf = (GameObject)Instantiate (
			PrefabLeaf,
			vectorGlobal,
			PrefabLeaf.transform.rotation
		);
		leaf.name = leaf.name + "-id" + new_leaf.id;

		Vector3 temporal_angles = leaf.transform.eulerAngles;
		temporal_angles.y = branchAngles.y;
		leaf.transform.eulerAngles = temporal_angles;
		leaf.transform.SetParent (branch.transform);

		LeafEvents leafEvents = leaf.GetComponent<LeafEvents> ();
		leafEvents.leaf = new_leaf;
		leafEvents.leaf_name = new_leaf.name;

		return leaf;
	}

	private void JoinByLiana(Leaf leaf){

		if (leaf.branchs.Count > 1) {
			int index = 1;
			GameObject firstLeaf;
			List<GameObject> leaves_flow = new List<GameObject> ();
			GameObject[] leavesWithTag = GameObject.FindGameObjectsWithTag ("Leaf");

			foreach(GameObject leafWithTag in leavesWithTag) {
				if (leafWithTag.name == "Leaf3(Clone)-id" + leaf.id) {
					leaves_flow.Add (leafWithTag);
				}
			}
				
			Vector3[] points = new Vector3[2];
			firstLeaf = leaves_flow [0];
			points [0] = firstLeaf.transform.position;

			while (index < leaves_flow.Count) {

				GameObject liana = (GameObject)Instantiate (PrefabLiana);
				liana.transform.SetParent (firstLeaf.transform);
				points [1] = leaves_flow [index].transform.position;
				liana.GetComponent<LineRenderer> ().SetPositions (points);

				index += 1;
			}
		}
	}

	public IEnumerator ShowForest()
	{
		Trunk temporalTrunk = null;
		Branch temporalBranch = null;
		GameObject currentTree = null, currentTrunk = null, currentBranch = null;
		TrunkEvents existingTrunk = null;
		Vector3 trunkCoordinates = Vector3.zero, branchCoordinates = Vector3.zero, branchAngles = Vector3.zero;

		foreach(Leaf leaf in leaves) {

			foreach (Branch branch in leaf.branchs) {
				// print (leaf.name + " # " + branch.name + " > " + branch.trunk.name);

				if (temporalTrunk == null || temporalTrunk.id != branch.trunk.id) {

					if (currentTrunk != null) {
						existingTrunk = FindExistingTrunk (branch.trunk);
					}

					if (existingTrunk == null) {
						trunkCoordinates = GenerateTrunkCoordinates ();

						currentTree = ShowTree (trunkCoordinates);
						currentTrunk = ShowTrunk (branch.trunk, trunkCoordinates, currentTree);

						temporalTrunk = branch.trunk;
					} else {
						trunkCoordinates = existingTrunk.transform.position;

						currentTrunk = existingTrunk.gameObject;
						temporalTrunk = branch.trunk;
					}
			
				}
					
				if (temporalBranch == null || temporalBranch.id != branch.id) {
					BranchEvents existingBranch = FindExistingBranch (currentTrunk, branch);

					if (existingBranch == null) {
						branchCoordinates = GenerateBranchCoordinates (trunkCoordinates);
						branchAngles = GenerateBranchRotation ();

						currentBranch = ShowBranch (branch, branchCoordinates, branchAngles, currentTrunk);
						temporalBranch = branch;
					} else {
						branchCoordinates = existingBranch.transform.position;
						branchAngles = existingBranch.transform.eulerAngles;

						currentBranch = existingBranch.gameObject;
						temporalBranch = branch;
					}
						
				}
					
				ShowLeaf (leaf, GenerateLeafCoordinates(branchCoordinates), branchAngles, currentBranch);
			}

			JoinByLiana (leaf);
		}
		SetStatus (StatusBuildTree.Idle);
		RemoveSpinner ();
		yield return new WaitForSeconds (1f);
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

		Vector3 eulerAngles = PrefabBranch.transform.eulerAngles;
		eulerAngles.y = y;

		return eulerAngles;
	
	}

	private Vector3 GenerateLeafCoordinates(Vector3 branchCoordinates) {
		float x = branchCoordinates.x + 1f;
		float y = branchCoordinates.y + 3.53f;
		float z = branchCoordinates.z + 1f;

		Vector3 coordinates = new Vector3 (x, y, z);

		return coordinates;
	}

	private TrunkEvents FindExistingTrunk (Trunk trunk) {
		TrunkEvents[] trunkEvents = GameObject.FindObjectsOfType<TrunkEvents>();

		foreach(TrunkEvents trunkEvent in trunkEvents) {
			// print ("ID: " + trunkEvent.trunk.id + "ID param trunk: " + trunk.id);

			if (trunkEvent.trunk.id == trunk.id) {
				return trunkEvent;
			}
		}
		return null;
	}

	private BranchEvents FindExistingBranch (GameObject trunk, Branch branch) {
		BranchEvents[] branchEvents = trunk.transform.GetComponentsInChildren <BranchEvents>();

		foreach(BranchEvents branchEvent in branchEvents) {
			if (branchEvent.branch.id == branch.id) {
				return branchEvent;
			}
		}
		return null;
	}

	void ShowSpinner() {
		Transform ForestCameraTransform = GameObject.Find ("ForestCamera").transform;

		Instantiate(PrefabLoading, ForestCameraTransform.position + ForestCameraTransform.forward * 10, ForestCameraTransform.rotation);

		GameObject searchSpot = GameObject.Find ("SearchField");
		searchSpot.GetComponentInChildren<Image> ().enabled = false;
		searchSpot.GetComponentInChildren<Text> ().enabled = false;
	}

	void RemoveSpinner() {

		Destroy (GameObject.Find("Loading(Clone)"));

		GameObject searchSpot = GameObject.Find ("SearchField");
		searchSpot.GetComponentInChildren<Image> ().enabled = true;
		searchSpot.GetComponentInChildren<Text> ().enabled = true;
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
			ShowSpinner ();
		}

		if ((int)status == 1) {
			StartCoroutine(ShowForest ());
		}
			
	}

}
