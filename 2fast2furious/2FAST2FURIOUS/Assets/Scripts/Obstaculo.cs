using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstaculo : MonoBehaviour {

	public GameObject cronometroGo;
	public Cronometro cronometroScript;

	public GameObject audioFXGo;
	public AudioFX audioFXScript;

	void Start(){

		cronometroGo = GameObject.FindObjectOfType<Cronometro>().gameObject;
		cronometroScript = cronometroGo.GetComponent<Cronometro> ();

		audioFXGo = GameObject.FindObjectOfType<AudioFX> ().gameObject;
		audioFXScript = audioFXGo.GetComponent<AudioFX> ();

	}


	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Coche> () != null) {
			audioFXScript.audioChoque ();

			cronometroScript.tiempo -= 15;
			Destroy (this.gameObject);
		}
	}


}
