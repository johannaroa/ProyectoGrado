using UnityEngine;
using UnityEngine.Experimental.Networking;
using System.Collections;

public class APIRestClient: ScriptableObject {

	public static ArticleSerializable[] resultsAPI = new ArticleSerializable[0];
	public static CategorySerializable[] categories = new CategorySerializable[0];
	public static ThematicSerializable[] thematics = new ThematicSerializable[0];

	private string url_server = "http://localhost:8000";

	public IEnumerator SearchArticle(string query)
	{
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

	public IEnumerator AddArticle() 
	{
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
