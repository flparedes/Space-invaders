using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed = 1f;
	private float direccion = 0.1f;

	public GameObject laser;
	public GameObject cannon;
	public float probabilidadDisparo = 0.25f;

	// Use this for initialization
	void Start () {
		Debug.Log ("Padre: " + this.transform.parent.name);
		InvokeRepeating("disparar", 3, 1);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(Vector2.right * direccion * speed, Space.World);
	}
		
	void OnCollisionEnter2D(Collision2D collision2D) {
		//Debug.Log ("OnCollisionEnter2D.");
		gestionarColision(collision2D.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log ("OnTriggerEnter2D.");
		gestionarColision(other.gameObject);
	}

	private void gestionarColision(GameObject objColisionado)
	{
		// Debug.Log("Colisión con " + objColisionado.name + ". Su tag es: " + objColisionado.tag);

		//If the object we collided with was a Border.
		if (objColisionado.tag == Constantes.BorderTag)
		{
			//Debug.Log("Choco con el borde");
			//this.cambiarDireccion();
			EnemyGroupController parentController = this.transform.parent.GetComponent<EnemyGroupController>();
			parentController.CambiarDireccion ();
		}
		else if (objColisionado.tag == Constantes.LaserTag) {
			Laser laser = objColisionado.GetComponent<Laser> ();
			Debug.Log("Choco con el laser. Origen: " + laser.origen);
			if (!this.name.Equals (laser.origen)) {
				TakeDamage ();
			}
		}
	}

	public void CambiarDireccion() {
		direccion *= -1;
	}

	private void TakeDamage() {
		Destroy (this.gameObject);
	}

	private void disparar() {
		// Si el número generado aleatoriamente es menor que la probabilidad se dispara
		if (Random.value <= probabilidadDisparo) {
			Debug.Log ("Disparando Laser");

			GameObject clone = Instantiate (laser, cannon.transform.position, cannon.transform.rotation) as GameObject;
			Laser laserInstance = clone.GetComponent<Laser> ();
			laserInstance.origen = this.name;
			clone.name = "LaserEnemigo" + Time.time;
		}
	}
}
