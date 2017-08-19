using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cronometro : MonoBehaviour {

	public GameObject motorCarreteraGo;
	public MotorCarreteras motorCarreteraScript;

	public GameObject cocheGo;

	public float tiempo;
	public float distancia;

	// Se usan importnate UnityEngine.UI para capturar los elementos de la UNI
	public Text txtTiempo;
	public Text txtDistancia;
	public Text txtFinal;

	// Use this for initialization
	void Start () {
		
		// Busco el objeto llamado MotorCarreteras para guardarlo en una variable
		cocheGo = GameObject.Find("Coche");
		motorCarreteraGo = GameObject.Find ("MotorCarreteras");

		// Cargo el script C# llamado MotorCarreteras almacenado en el objeto
		motorCarreteraScript = motorCarreteraGo.GetComponent<MotorCarreteras> ();

		tiempo = 120;
		distancia = 0;
		txtTiempo.text = "2:00";
		txtDistancia.text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		calcularTiempoDistancia();
	}

	// Calcula la distancia y el tiempo recorrido cada segundo de juego
	void calcularTiempoDistancia(){

		bool contar = motorCarreteraScript.inicioJuego;

		if (contar) {
			
			distancia += Time.deltaTime * motorCarreteraScript.velocidad;
			tiempo -= Time.deltaTime * 1;
			txtDistancia.text = ((int)distancia).ToString() + " metros";

			int min = (int)tiempo / 60;
			int segundos = (int)tiempo % 60;

			if (segundos < 0)
				segundos = 0;

			//PadLeft dice que quiere dos digitos, y en caso de no poder rellenar mete un 0
			txtTiempo.text = min.ToString() +":" + segundos.ToString().PadLeft(2, '0');

			// Si la cuenta atrás llega a 0 el juego para
			if((int)tiempo <= 0) {
				txtDistancia.enabled = false;
				txtTiempo.enabled = false;
				motorCarreteraScript.inicioJuego = false;
				motorCarreteraScript.juegoTerminado ();
				txtFinal.text = ((int)distancia).ToString() + "m";
				cocheGo.gameObject.GetComponent<AudioSource> ().Stop ();
			}
		}	
	}
}
