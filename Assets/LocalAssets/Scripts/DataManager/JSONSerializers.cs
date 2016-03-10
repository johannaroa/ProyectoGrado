using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class ArticleSerializable {
	
	public string titulo;
	public string contenido;
	// public int categoryId;
}

[System.Serializable] 
public class ArticleListSerializable { 
	public ArticleSerializable[] articles; 
}

public static class Helpers {
	
	public static string WrapToClass(this string source, string topClass){
		return string.Format("{{ \"{0}\": {1}}}", topClass, source);
	}

}