using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	/* Declaración de variables */

	private float velocidad;
	private Vector3 ultimaPos;

	public Transform laser;
	public float distanciaDisparo = 3f;
	public float recargaDisparo = 2f;
	private float contadorRecarga = 0.0f;

	public AudioClip sonidoDisparo;
	private AudioSource audioSource;

	/* Declaración métodos Engine */


	// Use this for initialization
	void Start(){
		// Inicio que el juego arranque normal si se ha reiniciado desde el menu pausado (Script pausarJuego)
		Time.timeScale = 1;

		velocidad = 0.0f;
		ultimaPos = new Vector3();
		audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update(){
		// Si el escript pausarJuego no esta en pausa se puede mover
		if (!PausarJuego.pausa) {
			Rotar();
			Mover();
			Disparar();
		}
	}
		

	/* Declaración métodos propios */


	//Rota la nave para que esta mire al puntero
	void Rotar(){
		// Guardo en un vector la posicion del raton
		Vector3 posMapa = Input.mousePosition;
		// Transformo esta posición a las coordenadas globales del mundo
		posMapa = Camera.main.ScreenToWorldPoint(posMapa);
		// Obtengo la posición de la nave
		Vector3 posNave = this.transform.position;
		// Obtengo la diferencia de posiciones entre la nave y el raton
		float posX = posNave.x - posMapa.x;
		float posY = posNave.y - posMapa.y;
		/* Consigo el ángulo de giro resultante con la libreria Mathf
		Como viene en radianes, se multiplica por Rad2Deg para pasarlo a grados */
		float angulo = Mathf.Atan2(posY, posX) * Mathf.Rad2Deg;
		// Realizo el giro, solo gira la Z. Se le suma 90 para compensar el giro
		Quaternion rotacion = Quaternion.Euler(new Vector3(0,0,angulo+90));
		this.transform.rotation = rotacion;
	}
		
	void Mover(){
		if (Input.GetMouseButton (0)) {
			velocidad = 2.5f;
			// Obtengo las coordenadas globales del mundo del ratón
			ultimaPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			ultimaPos.z = 0f;
			// Muevo la nave desde mi posicion hasta las coordenadas clicadas a la velocidad puesta en delta
			this.transform.position = Vector3.MoveTowards (this.transform.position, ultimaPos, velocidad * Time.deltaTime);
		} 
		// Si no se ha clicado, la nave se mueve con la inercia hasta que se para o llega al punto
		else {
			this.transform.position = Vector3.MoveTowards (this.transform.position, ultimaPos, velocidad * Time.deltaTime);
			velocidad *= 0.99f;
		}
	}

	void Disparar(){
		// Si se ha hecho click derecho y el contador es igual o menor a 0 dispara
		if (Input.GetMouseButtonDown (1) && contadorRecarga <= 0f) {
			contadorRecarga = recargaDisparo;
			LanzarDisparo ();
		} 
		if(contadorRecarga > 0f)
			contadorRecarga -= Time.deltaTime;
	}

	void LanzarDisparo(){
		// Posicion de la nave
		Vector3 posLaser = this.transform.position;
		float anguloRotar = this.transform.localEulerAngles.z;
		// Transformo los angulos a radianes y asigno la pos
		posLaser.x += Mathf.Cos(anguloRotar * Mathf.Deg2Rad) * distanciaDisparo;
		posLaser.y += Mathf.Sin(anguloRotar * Mathf.Deg2Rad) * distanciaDisparo;
		// Creo un rayo delante de la nave
		audioSource.PlayOneShot(sonidoDisparo);
		Instantiate (laser, posLaser, this.transform.rotation);
	}
		
}
