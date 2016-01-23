using UnityEngine;
using System.Collections;

public class AnimalProperties
{
	public Sprite miSprite;
	public string Nombre;
	public string SaludoAnimal;
	public Color ColorAnimal;

	public AnimalProperties(string nombreNuevo)
	{
		this.Nombre = nombreNuevo;
	}

	public void Habla(string texto)
	{
		Debug.Log(texto + SaludoAnimal);
	}

	public void Callate(TextMesh texto)
	{
		texto.text = "sssh";
	}
}
