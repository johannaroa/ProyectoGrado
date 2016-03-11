using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class ArticleSerializable {
	
	public string titulo;
	public string contenido;
	// public int[] categoryId;
}

[System.Serializable] 
public class ArticleListSerializable { 
	public ArticleSerializable[] articles; 
}

public static class JSONHelpers {
	
	public static string WrapToClass(this string keyParent, string data){
		return string.Format("{{ \"{0}\": {1}}}", keyParent, data);
	}

}