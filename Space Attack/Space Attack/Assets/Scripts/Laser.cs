using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {


	/* Declaración de métodos */


	public float duracion = 3.0f;
	public float velocidad = 5.5f;
	public int daño = 1;

	// Use this for initialization
	void Start () {
		// Se borra el gameObject y no el this ya que sino se queda en pantalla la imagen
		Destroy (this.gameObject, duracion);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate (Vector3.up * velocidad * Time.deltaTime);
	}
}
