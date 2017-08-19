using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Fundido : MonoBehaviour {

	public Image fundido;
	public string[] escenas;

	// Use this for initialization
	void Start () {
		fundido.CrossFadeAlpha (0, 0.5f, false);
	}

	// Hace un fundido de una escena a otra
	public void FadeOut(int s){
		fundido.CrossFadeAlpha (1, 0.3f, false);
		StartCoroutine (cambioEscena(escenas[s]));
	}

	// Corrutina que hace el cambio de escena
	IEnumerator cambioEscena(string escena){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene(escena);
	}
}
