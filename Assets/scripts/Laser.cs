using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	
	public float laserSpeed = 10f;
	public float laserLive = 10f;

	// Use this for initialization
	void Start () {
		// Kills the game object in laserLive seconds after loading the object
		Destroy (this.gameObject, laserLive);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.Translate(Vector2.up * Time.deltaTime * laserSpeed, Space.World);
	}
}
