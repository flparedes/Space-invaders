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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		var horizontal = Input.GetAxis ("Horizontal");
		var vertical = Input.GetAxis ("Vertical");

		if (horizontal != 0) {
			// Debug.Log ("Movimiento horizontal: " + horizontal);

			this.transform.Translate(Vector2.right * horizontal * speed, Space.World);
		}

		if (vertical > 0) {
			// Debug.Log ("Pulsado arriba: " + vertical);

			if (canShoot()) {
				disparar();
			}
		}
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

			// clone.velocity = transform.TransformDirection(Vector3.up * laserSpeed);

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
}