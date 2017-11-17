using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour {

	private int vida = 2;
	private GameController controlador;
	public Transform explosion;
	public AudioClip sonidoGolpe;

	void Start () {
		controlador = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
	}

	// Método que gestiona la colision del objeto que choca
	void OnTriggerEnter2D(Collider2D collider){
		// Si el objeto se llama laser entra y opera
		if(collider.name.Contains ("laser")){
			Laser laser = collider.gameObject.GetComponent<Laser>();
			vida -= laser.daño;
			Destroy(laser.gameObject);
			// Pido que suene una vez el golpe
			GetComponent<AudioSource>().PlayOneShot(sonidoGolpe);
			// Si la vida de la nave llega a cero, se destruye
			if (vida < 1){
				Transform aux = this.gameObject.transform;
				Destroy(this.gameObject);
				Destroy(explosion.gameObject);
				// Decremento en uno el número de enemigos disponibles en la oleada
				controlador.eliminarEnemigo();
				// Instancio una explosion
				if(explosion){
					Transform explosionT = (Transform)Instantiate(explosion, aux.position, aux.rotation);
					GameObject explosionGO = explosionT.gameObject;
					// Destruyo la explosión tras un segundo y medio para que no genere bucle
					Destroy (explosionGO, 2.2f);
				}
			}
				
			
		}
	}
}
