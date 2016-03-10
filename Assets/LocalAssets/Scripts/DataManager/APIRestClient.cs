using UnityEngine;
using UnityEngine.Experimental.Networking;
using System.Collections;

public class APIRestClient: ScriptableObject {

	private string url_server = "http://localhost:8000";

	public IEnumerator SearchArticle(string query){
		string endPoint = "/forest/v1/articles/?search=";

		UnityWebRequest webRequest = UnityWebRequest.Get(url_server + endPoint + query);
		yield return webRequest.Send ();

		if (webRequest.isError) {
			Debug.Log (webRequest.error);
		} else {

			Debug.Log (Helpers.WrapToClass("articles", webRequest.downloadHandler.text));

			ArticleListSerializable articleList = JsonUtility.FromJson<ArticleListSerializable> (
				Helpers.WrapToClass(webRequest.downloadHandler.text, "articles")
			);
			Debug.Log (articleList.articles[0].titulo);
		}
	}

	public IEnumerator AddArticle() {
		string endPoint = "/forest/v1/articles/";

		WWWForm form = new WWWForm ();
		form.AddField("name", "Test");

		UnityWebRequest webRequest = UnityWebRequest.Post (url_server + endPoint, form);
		yield return webRequest.Send ();

		if (webRequest.isError) {
			Debug.Log (webRequest.error);
		} else {
			Debug.Log ("Post completed!");
		}
	}

	IEnumerator AddCategory() {
		yield return null;
	}

	IEnumerator AddTheme() {
		yield return null;
	}

	IEnumerator UpdateArticle() {

		string endPoint = "";

		string myData = "";
		UnityWebRequest webRequest = UnityWebRequest.Put (url_server + endPoint, myData);
		yield return webRequest.Send() ;

		if(webRequest.isError) {
			Debug.Log(webRequest.error);
		}
		else {
			Debug.Log("Put completed!");
		}
	}

	IEnumerator UpdateCategory() {
		yield return null;
	}

	IEnumerator UpdateTheme() {
		yield return null;
	}

	IEnumerator DeleteArticle() {
	
		string endPoint = "";

		UnityWebRequest webRequest = UnityWebRequest.Delete (url_server + endPoint);
		yield return webRequest.Send ();

		if(webRequest.isError) {
			Debug.Log(webRequest.error);
		}
		else {
			Debug.Log("Delete completed!");
		}
	}

	IEnumerator DeleteCategory() {
		yield return null;
	}

	IEnumerator DeleteTheme() {
		yield return null;
	}

}
