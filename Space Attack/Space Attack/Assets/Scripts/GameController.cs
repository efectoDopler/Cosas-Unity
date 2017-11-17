using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importa los métodos de la UI de Unity

public class GameController : MonoBehaviour {


	/* Declaración de variables */


	public Transform enemigo;
	public float tOleada = 1f;
	public float tEnemigo = 1f;
	public float tEntreOleada = 1.2f;

	public int enemigosOleada = 8;
	private int enemigosEliminados = 0;

	public Text puntuacion;
	public Text oleada;

	private int vPuntuacion;
	private int vOleada;

	// Use this for initialization
	void Start () {
		// Inicio de la corrutina declarada
		StartCoroutine(soltarEnemigo());
		vPuntuacion = 0;
		vOleada = 0;
	}

	// Corrutina en Unity
	IEnumerator soltarEnemigo(){
		// Inidico que espere 1.5s antes de iniciar la 1º oleada
		yield return new WaitForSeconds(tOleada);
		// Inicio bucle de oleadas infinito
		while(true){
			// Se inicia una nueva oleada tras acabar la anterior
			if (enemigosEliminados < 1){
				// Coloco los enemigos por oleada
				incrementarOleada();
				for (int i = 0; i < enemigosOleada; i++) {
					/* Genero los enemigos en posiciones aleatorias */
					// Genero el rango de 10 unidades ya que si se mira el mapa hay 9 a cada lado en x
					float distancia = Random.Range(10,12);
					// Genero una posición random generando un circulo desde el radio del mapa (2D)
					Vector2 dirRandom = Random.insideUnitCircle;
					/// Normalizo para evitar numeros muy pequeños y los enemigos aparezcan dentro de la vista
					dirRandom.Normalize();
					// Posición del gameController (0,0,0)
					Vector3 posEnemigo = this.transform.position;
					posEnemigo.x += dirRandom.x * distancia;
					posEnemigo.y += dirRandom.y * distancia;
					// Instancio cada enemigo en pantalla
					Instantiate(enemigo, posEnemigo, this.transform.rotation);
					enemigosEliminados++;
					// Paro el tiempo entre creacion de enemigos
					yield return new WaitForSeconds(tEnemigo);
				}

			}
			// Si hay enemigos aún se espera el tiempo entre oleada
			else{
				yield return new WaitForSeconds (tEntreOleada);
			}
		}
	}

	// Metodo que se llama desde controlador enemigo cuando se destruye
	public void eliminarEnemigo(){
		enemigosEliminados--;
		incrementarPuntuancion ();
	}

	/* Metodos que actualizan la información de la UI */

	private void incrementarOleada(){
		vOleada++;
		oleada.text = "Oleada: " + vOleada.ToString ();
	}

	private void incrementarPuntuancion(){
		vPuntuacion++;
		puntuacion.text = "Puntuación: " + vPuntuacion.ToString ();
	}
}
