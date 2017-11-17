using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorMenu : MonoBehaviour {

	public void CargarNivel(string nombre){
		// Manejador de escenas
		SceneManager.LoadScene (nombre);
	}

	public void Salir(){

		// Si el juego está en el editor de Unity se cierra así
		#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;

		// Si el juego está en pc, movil o fuera de Unity se cierra así
		#else
			Application.Quit()
		#endif
	}
}
