using UnityEngine;
using System.Collections;

public class Dog : AnimalProperties
{
	private float fuerzaSalto;

	public Dog(string nuevoNombre, string raza) : base(nuevoNombre)
	{
		SaludoAnimal = "¡GUAU!";
		ColorAnimal = Color.red;
		fuerzaSalto = 200;
		Debug.Log(raza);
	}

	public void Saltar()
	{
		Debug.Log (Nombre + " le dio por saltar." + fuerzaSalto);
	}
}
