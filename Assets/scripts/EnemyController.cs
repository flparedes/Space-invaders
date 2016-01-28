using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public float speed = 1f;
	private float direccion = 0.1f;

	// Use this for initialization
	void Start () {
		Debug.Log ("Padre: " + this.transform.parent.name);
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
			Debug.Log("Choco con el laser");
			TakeDamage ();
		}
	}

	public void CambiarDireccion() {
		direccion *= -1;
	}

	private void TakeDamage() {
		Destroy (this.gameObject);
	}
}
