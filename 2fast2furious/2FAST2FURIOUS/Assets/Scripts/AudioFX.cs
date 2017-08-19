using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFX : MonoBehaviour {

	public AudioClip[] clips;

	/*
	 * clips[0] = choque
	 *clips[1] = musica de fondo
	*/

	public AudioSource audioSource;

	// Use this for initialization
	void Start () {

		// Cojo el componente de sonido del propio objeto
		audioSource = GetComponent<AudioSource> ();
	}

	public void audioChoque(){
		audioSource.clip = clips[0];
		audioSource.Play();
	}

	public void audioMusica(){
		audioSource.clip = clips[1];
		audioSource.Play();
	}

	public void audioMoneda(){
		audioSource.clip = clips[2];
		audioSource.Play();
	}

	// Update is called once per frame
	void Update () {
		
	}
}
