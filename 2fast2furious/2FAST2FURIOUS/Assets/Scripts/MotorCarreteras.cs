using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorCarreteras : MonoBehaviour {

	public float velocidad;
	private int contador;
	private float tamCalle;
	private Vector3 tamPantalla;
	private bool salirPantalla;

	public GameObject[] callesGo;
	public GameObject calle;
	public GameObject anterior;
	public GameObject nueva;
	public bool inicioJuego;
	public bool finJuego;
	public Camera camara;
	public float cX, cY;

	public GameObject cocheGo;
	public GameObject audioFXGo;
	public AudioFX audioFXScript;
	public GameObject finalGo;

	// Use this for initialization
	void Start () {
		iniciarJuego();
	}
	
	// Update is called once per frame
	void Update () {

		if (inicioJuego && !finJuego) {
			transform.Translate (0 , -3f * velocidad * Time.fixedDeltaTime, 0);
			if (contador > 1) {
				if (anterior.transform.position.y + tamCalle < tamPantalla.y && !salirPantalla) {
					salirPantalla = true;
					destruirCalle ();
				}
			} 
	
			else {
				if ((calle.transform.position.y < tamPantalla.y) && !salirPantalla) {
					salirPantalla = true;
					destruirCalle ();
				}
			}

		}
	}
		
	void iniciarJuego(){
	
		contador = 0;
		velocidad = 3;
		finJuego = false;
		salirPantalla = false;

		cocheGo = GameObject.FindObjectOfType<Coche>().gameObject;
		audioFXGo = GameObject.FindObjectOfType<AudioFX>().gameObject;
		audioFXScript = audioFXGo.GetComponent<AudioFX>();
		finalGo = GameObject.Find("PanelGameOver");
		finalGo.SetActive (false);

		medirPantalla();
		iniciarCalles();

	}

	// Hace aparecer el panel de datos de la partida
	public void juegoTerminado(){
		cocheGo.GetComponent<AudioSource> ().Stop ();
		audioFXScript.audioMusica();
		finalGo.SetActive (true);
	}

	// Obtiene la resolucion de pantalla en Y usando la camara para sacar el tamaño de esta.
	void medirPantalla(){
		tamPantalla = new Vector3(0, camara.ScreenToWorldPoint(new Vector3(0, 0, 0)).y, 0);
	}
	
	// Inicio todas las calles en un array
	void iniciarCalles(){
	
		callesGo = GameObject.FindGameObjectsWithTag("Calle");

		for (int i = 0; i < callesGo.Length; i++) {
			// Apago la calle para no consumir memoria
			callesGo[i].gameObject.SetActive(false);
			// Renombro la calle
			callesGo[i].gameObject.name= "Calle_" + i;
		}

		insertarCalle();

	}
		
	// Inserto una calle del array en el juego
	void insertarCalle(){
	
		contador++;
	
		int seleccion = Random.Range (0, callesGo.Length);

		// Instancio en el objeto calle una de las calles del array
		calle = Instantiate (callesGo[seleccion]);
		calle.SetActive (true);
		calle.name = "Calle" + contador;

		// Asigno a la calle el padre MotorCarreteras
		calle.transform.parent = gameObject.transform;

		posicionarCalle();

	}

	// Posiciono una nueva calle tras la anterior
	void posicionarCalle(){
	
		// Si es la primera calle se pone en la pos 0
		if (contador == 1) 
			calle.transform.position = new Vector3 (0, 0, 0);
	
		// Si hay mas de una calle se engancha al final de la anterior
		else {
			anterior = GameObject.Find("Calle" + (contador - 1));
			nueva = GameObject.Find("Calle" + contador);

			medirCalle ();

			cX = anterior.transform.position.x;
			cY = anterior.transform.position.y + (tamCalle - 2);
			nueva.transform.position = new Vector3 (cX, cY, 0);
		}

	}

	// Mide el tamaño de una calle para colocarla tras la anterior
	void medirCalle(){

		tamCalle = 0;

		/* Obtiene los componentes del gameObject hijo i usando el componente transform (ya que todos los tienen) para 
		 * construir el objeto y conseguir el componente deseado (SpriteRenderer)*/
		for (int i = 0; i < anterior.transform.childCount; i++) {

			// Si la calle tiene componentes del tipo pieza obtiene el tamaño de cada pieza y la suma
			if(contador == 1){
				if (calle.transform.GetChild(i).gameObject.GetComponent<Pieza> () != null)
					tamCalle += calle.transform.GetChild(i).gameObject.GetComponent<Pieza>().
						transform.gameObject.GetComponent<SpriteRenderer> ().bounds.size.y;
			}
			// Si la calle anterior tiene componentes del tipo pieza obtiene el tamaño de cada pieza y la suma
			else
				if (anterior.transform.GetChild(i).gameObject.GetComponent<Pieza> () != null)
					tamCalle +=  anterior.transform.GetChild(i).gameObject.GetComponent<Pieza>().
						transform.gameObject.GetComponent<SpriteRenderer>().bounds.size.y;
		}

	}

	// Destruye la calle que ya no se ve
	void destruirCalle(){
	
		// Destruyo el gameObject
		Destroy(anterior);

		// Aseguro que no hay referencias
		anterior = null;
		insertarCalle ();
		salirPantalla = false;

	}
		
}
