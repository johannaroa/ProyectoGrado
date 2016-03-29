using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class JSONHelpers {

	public static string WrapToClass(this string keyParent, string data){
		return string.Format("{{ \"{0}\": {1}}}", keyParent, data);
	}

}
[System.Serializable]
public class ArticleSerializable {
	public string titulo;
	public string contenido;
	public string categories_link;
}

[System.Serializable] 
public class ArticleListSerializable { 
	public ArticleSerializable[] articles; 
}

[System.Serializable]
public class CategorySerializable {
	public string nombre;
	public string descripcion;
	public string thematic_link;
}

[System.Serializable]
public class CategoryListSerializable {
	public CategorySerializable[] categories;
}

[System.Serializable]
public class ThematicSerializable {
	public string nombre;
	public string descripcion;
}

[System.Serializable]
public class ThematicListSerializable {
	public ThematicSerializable[] thematics;
}

