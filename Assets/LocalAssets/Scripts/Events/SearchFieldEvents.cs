using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SearchFieldEvents : MonoBehaviour {

	public GameObject Tree;

	private void SubmitSearch(string query)
	{
		APIRestClient apiRestClient = ScriptableObject.CreateInstance ("APIRestClient") as APIRestClient;

		StartCoroutine (apiRestClient.SearchArticle(query));	
	}

	private void InitListener() 
	{
		// TODO: Probar si en vez de poner esto aquí, que resultado da usar el evento OnEndEdit (en el GUI)
		InputField input = gameObject.GetComponent<InputField>();
		var se = new InputField.SubmitEvent();
		se.AddListener(SubmitSearch);
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

			foreach (ArticleSerializable lol in APIRestClient.resultsAPI) {
				Debug.Log (lol.titulo + lol.contenido);
				Instantiate (Tree);
			}

		}
	}

}
