using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuentaAtras : MonoBehaviour {

	public GameObject motorCarreteraGo;
	public MotorCarreteras motorCarretera;
	public Sprite[] numeros;
	public float[] pos;

	public GameObject contadorGo;
	public SpriteRenderer contadorComp;

	public GameObject controladorCocheGo;
	public GameObject cocheGo;
	public AudioClip inicio;

	// Use this for initialization
	void Start () {
		InicioComponentes();
	}

	void InicioComponentes(){

		motorCarreteraGo = GameObject.Find("MotorCarreteras");
		motorCarretera = motorCarreteraGo.GetComponent<MotorCarreteras>();

		contadorGo = GameObject.Find("Contador");
		contadorComp = contadorGo.GetComponent<SpriteRenderer>();

		controladorCocheGo = GameObject.Find("ControladorCoche");
		cocheGo = GameObject.Find("Coche");

		IniciarCuentaAtras();
	}

	void IniciarCuentaAtras(){
		StartCoroutine(contar());
	}

	// Corrutina encarga de llevar a cabo el contador y cambiar los sprites
	IEnumerator contar(){

		// Arrancar motor
		controladorCocheGo.GetComponent<AudioSource>().Play();
		yield return new WaitForSeconds(2);

		// 3
		contadorComp.sprite = numeros[0];
		this.gameObject.transform.Rotate(0, 0, pos[0]);
		this.gameObject.GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (1);

		// 2
		contadorComp.sprite = numeros[1];
		this.gameObject.transform.Rotate(0, 0, pos[1]*2);
		this.gameObject.GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (1);

		// 1
		contadorComp.sprite = numeros[2];
		this.gameObject.transform.Rotate(0, 0, 29);
		this.gameObject.GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (1);

		// GO!
		contadorComp.sprite = numeros[3];
		this.gameObject.GetComponent<AudioSource> ().clip = inicio;
		this.gameObject.GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (1f);

		contadorGo.SetActive (false);
		cocheGo.gameObject.GetComponent<AudioSource>().Play();
		motorCarretera.inicioJuego = true;

	}

	// Update is called once per frame
	void Update () {
		
	}
}
