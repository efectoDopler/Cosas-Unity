using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 // Heredo de la clase que controla el menu principal
public class PausarJuego : ControladorMenu {

	// Es estática porque solo existe una instancia de la variable
	public static bool pausa;
	public static bool opciones;

	public GameObject menuPausa;
	public GameObject menuOpciones;

	// Use this for initialization
	void Start () {
		pausa = false;
		opciones = false;
		// Cierro el menu de pausa
		menuPausa.SetActive (false);
		menuOpciones.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		// Si se pulsa el boton escape sale el menú de pausa
		if (Input.GetKeyDown("escape")) {
			if (pausa) {
				pausa = false;
				// Digo que el tiempo del juego se pone a 1, osea que se mueve
				Time.timeScale = 1;
			} 
			else {	
				pausa = true;
				// Digo que el tiempo del juego se pone a 0, osea que no ocurre nada
				Time.timeScale = 0;
			}

			menuPausa.SetActive (pausa);
		}
	}

	public void Continuar(){
		pausa = false;
		menuPausa.SetActive (pausa);
		Time.timeScale = 1;
	}

	public void Opciones(){
		opciones = !opciones;
		menuOpciones.SetActive (opciones);
		menuPausa.SetActive (!opciones);
	}

	public void AumentarCalidad(){
		QualitySettings.IncreaseLevel ();
	}

	public void DisminuirCalidad(){
		QualitySettings.DecreaseLevel ();
	}
}
