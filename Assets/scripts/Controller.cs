using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public float speed = 1f;
	public float shootDelay = 1f;

	public GameObject laser;
	public GameObject leftCannon;
	public GameObject rightCannon;

	private bool laserShooted = false;
	private float shootTime;
	private bool leftCannonShooted = false;
    private bool bordeIzq = false;
    private bool bordeDcho = false;
    private float verticalAxisValue = 0f;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var horizontal = Input.GetAxis ("Horizontal");
		var vertical = Input.GetAxis ("Vertical");

		if (horizontal != 0) {
            // Debug.Log ("Movimiento horizontal: " + horizontal);
            bool mover = true;
            if ((horizontal > 0 && bordeDcho) || (horizontal < 0 && bordeIzq))
            {
                mover = false;
            }

            if (mover) {
                this.transform.Translate(Vector2.right * horizontal * speed, Space.World);
            }
		}

        // Si ha pulsado arriba y su valor es mayor o igual que el anterior.
		if (vertical > 0 && vertical >= verticalAxisValue) {
			// Debug.Log ("Pulsado arriba: " + vertical);

			if (canShoot()) {
				disparar();
			}
		}

        // Se actualiza el valor del eje vertical para la siguiente llamada.
        verticalAxisValue = vertical;
    }

	private bool canShoot() {
		bool canShoot = false;

		if (!laserShooted) {
			canShoot = true;
		} else {
			Debug.Log ("shootTime: " + shootTime + ". Tiempo actual: " + Time.time);
			if (shootTime + shootDelay < Time.time) {
				canShoot = true;
				laserShooted = false;
			}
		}

		return canShoot;
	}

	private void disparar() {
		if (!laserShooted) {
			Debug.Log ("Disparando Laser");

			Vector3 position = this.laserPosition();

			GameObject clone = Instantiate (laser, position, this.transform.rotation) as GameObject;
            clone.name = "Laser" + Time.time;

			shootTime = Time.time;
			laserShooted = true;
		}
	}

	private Vector3 laserPosition() {
		Vector3 position = this.transform.position;
		if (leftCannon != null && rightCannon != null) {
			if (leftCannonShooted) {
				position = rightCannon.transform.position;
			} else {
				position = leftCannon.transform.position;
			}
			leftCannonShooted = !leftCannonShooted;
		}
		return position;
	}

    void OnCollisionEnter2D(Collision2D collision2D) {
        gestionarColision(collision2D.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        gestionarColision(other.gameObject);
    }

    private void gestionarColision(GameObject objColisionado)
    {
        Debug.Log("Colisión Trigger con " + objColisionado.name + ". Su tag es: " + objColisionado.tag);

        //If the object we collided with was a Border.
        if (objColisionado.tag == Constantes.BorderTag)
        {
            Debug.Log("Entro en el if");
			bordeIzq = objColisionado.name.Equals(Constantes.BordeIzqName);
			bordeDcho = objColisionado.name.Equals(Constantes.BordeDchoName);
        }

        Debug.Log("bordeIzq: " + bordeIzq + ". bordeDcho: " + bordeDcho);
    }

    void OnCollisionExit2D(Collision2D collision2D)
    {
        gestionarSalidaColision(collision2D.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        gestionarSalidaColision(other.gameObject);
    }

    private void gestionarSalidaColision(GameObject objColisionado)
    {
        //If the object we exit colliding with was a Border.
		if (objColisionado.tag == Constantes.BorderTag)
        {
            bordeIzq = false;
            bordeDcho = false;
        }
    }
}
