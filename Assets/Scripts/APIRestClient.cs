using UnityEngine;
using System.Collections;

public class APIRestClient : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine("AskWebservice");
	}

	// Update is called once per frame
	void Update () {

	}

	IEnumerator AskWebservice(){
	    /*  Si el parámetro hubiera que pasarlo por POST lo haríamos así:
	    WWWForm form = new WWWForm();
	    form.AddField("ip", playerIPAddress);
	    WWW w = new WWW("http://freegeoip.net/json/",form);
	    */

	    // Pasamos parámetro por GET
	    WWW w = new WWW("http://localhost:8000/forest/v1/articles/");
	    yield return w;

	    yield return new WaitForSeconds(1f);

	    Debug.Log(w.text);
	}
}
