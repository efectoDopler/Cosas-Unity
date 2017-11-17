using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private Transform player;
	public float velocidad = 0.92f;

	// Use this for initialization
	void Start () {
		// Obtengo la posicion del player
		player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		// Obtengo las coordenadas concretas del player menos las mias
		Vector3 direccion = player.position -  this.transform.position;

		// Se normaliza el vector para que no tenga infinitos decimales
		direccion.Normalize ();

		// Muevo al objeto hacia la posicion calculada con una velocidad
		this.transform.position += direccion * velocidad * Time.deltaTime;
	}
}
