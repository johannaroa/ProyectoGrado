using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SearchField : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// TODO: Probar si en vez de poner esto aquí, que resultado da usar el evento OnEndEdit (en el GUI)
		InputField input = gameObject.GetComponent<InputField>();
		var se = new InputField.SubmitEvent();
		se.AddListener(SubmitSearch);
		input.onEndEdit = se;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void SubmitSearch(string query)
	{
		StartCoroutine (PerformSearch(query));
	}

	// TODO: este método ira en la clase APIRestClient
	IEnumerator PerformSearch(string query) {
	
		WWW w = new WWW("http://localhost:8000/forest/v1/articles/?search="+ query);
		yield return w;

		yield return new WaitForSeconds(1f);

		Debug.Log(w.text);
	}
}
