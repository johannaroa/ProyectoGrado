using UnityEngine;
using UnityEngine.Experimental.Networking;
using System.Collections;

public class APIRestClient: ScriptableObject {

	public static ArticleSerializable[] resultsAPI = new ArticleSerializable[0];
	public static CategorySerializable[] categories = new CategorySerializable[0];
	public static ThematicSerializable[] thematics = new ThematicSerializable[0];

	private string url_server;

	public IEnumerator SearchArticle(string query)
	{
		if (resultsAPI.Length > 0) {
			resultsAPI = new ArticleSerializable[0];
		}

		url_server = GameObject.Find ("GlobalManager").GetComponent<GlobalManager> ().url;
		string endPoint = "/forest/v1/articles/?search=";

		UnityWebRequest webRequest = UnityWebRequest.Get(url_server + endPoint + query);
		yield return webRequest.Send ();

		if (!webRequest.isError) {
			ArticleListSerializable articleList = JsonUtility.FromJson<ArticleListSerializable> (
				JSONHelpers.WrapToClass("articles", webRequest.downloadHandler.text)
			);

			resultsAPI = articleList.articles;
		} else {
			Debug.Log (webRequest.error);
		}
	}

	public IEnumerator GetCategories(string url) {

		UnityWebRequest webRequest = UnityWebRequest.Get (url);
		yield return webRequest.Send ();

		if (!webRequest.isError) {
			CategoryListSerializable categoryList = JsonUtility.FromJson<CategoryListSerializable> (
                JSONHelpers.WrapToClass ("categories", webRequest.downloadHandler.text)                                        
            );

			categories = categoryList.categories;
		} else {
			Debug.Log (webRequest.error);
		}
	}

	public IEnumerator GetThematics(string url) {
	
		UnityWebRequest webRequest = UnityWebRequest.Get (url);
		yield return webRequest.Send ();

		if (!webRequest.isError) {
			ThematicListSerializable thematicList = JsonUtility.FromJson<ThematicListSerializable> (
				JSONHelpers.WrapToClass ("thematics", webRequest.downloadHandler.text)                                        
			);

			thematics = thematicList.thematics;
		} else {
			Debug.Log (webRequest.error);
		}
	}

	IEnumerator DeleteArticle() {
	
		url_server = GameObject.Find ("GlobalManager").GetComponent<GlobalManager> ().url;
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
