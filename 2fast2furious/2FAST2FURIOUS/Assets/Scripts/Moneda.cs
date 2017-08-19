using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneda : MonoBehaviour {

	public GameObject cronometroGo;
	public Cronometro cronometroScript;
	public GameObject audioFXGo;
	public AudioFX audioFXScript;

	// Use this for initialization
	void Start () {
		cronometroGo = GameObject.FindObjectOfType<Cronometro>().gameObject;
		cronometroScript = cronometroGo.GetComponent<Cronometro> ();
		audioFXGo = GameObject.FindObjectOfType<AudioFX> ().gameObject;
		audioFXScript = audioFXGo.GetComponent<AudioFX> ();
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.GetComponent<Coche> () != null) {
			audioFXScript.audioMoneda();
			cronometroScript.tiempo += 10;
			Destroy (this.gameObject);
		}
	}

}
