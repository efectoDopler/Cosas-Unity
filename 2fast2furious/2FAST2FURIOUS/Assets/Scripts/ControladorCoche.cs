using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCoche : MonoBehaviour {

	public Camera camara;
	public GameObject cocheGo;
	public GameObject motorCarreterasGo;
	public MotorCarreteras motorCarreteras;

	public float anguloGiro, velocidad;
	Vector3 tamPantalla;
	public bool empezar;

	// Use this for initialization
	void Start () {

		// Aquí cojo el coche como objeto para probar otra forma de acceder a él
		cocheGo = GameObject.FindObjectOfType<Coche>().gameObject;
		motorCarreterasGo = GameObject.Find ("MotorCarreteras");
		motorCarreteras = motorCarreterasGo.GetComponentInChildren<MotorCarreteras> ();
		velocidad = 8;
		anguloGiro = 12;
		medirPantalla ();
	}

	// Mide el vector X de la pantalla para hacer que el coche no la atraviese
	void medirPantalla(){
		tamPantalla = new Vector3(camara.ScreenToWorldPoint(new Vector3(0, 0, 0)).x, 0 , 0);
	}
	
	// Update is called once per frame
	void Update () {
		empezar = motorCarreteras.inicioJuego;
		float giro = 0;

		if (empezar) {

			// Si el coche no trata de salirse de la pantalla gira bien
			if ((tamPantalla.x < (cocheGo.transform.position.x - 1.4f)) && (-tamPantalla.x > (cocheGo.transform.position.x + 1.4f))) {
				transform.Translate (Vector2.right * Input.GetAxis ("Horizontal") * velocidad * Time.deltaTime);
				giro = Input.GetAxis ("Horizontal") * anguloGiro;
			}
				
			// Si el coche trata de salirse de la pantalla gira para el otro lado
			else if(tamPantalla.x > (cocheGo.transform.position.x - 1.4f) || (-tamPantalla.x < (cocheGo.transform.position.x + 1.4f)) ) {
				transform.Translate (new Vector2(-3, 0) * Input.GetAxis ("Horizontal") * (velocidad+1f) * Time.deltaTime);
				giro = Input.GetAxis ("Horizontal") * -anguloGiro;
			}
				
			// Quaternion.Euler permite hacer una rotacion con efecto, no algo automático
			cocheGo.transform.rotation = Quaternion.Euler (0, 0, -giro);
		}
	}

}
