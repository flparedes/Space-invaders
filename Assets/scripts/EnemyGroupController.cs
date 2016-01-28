using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyGroupController : MonoBehaviour {

	private List<EnemyController> enemigos;

	// Use this for initialization
	void Start () {
		enemigos = new List<EnemyController> ();
		EnemyController[] componentes = this.GetComponentsInChildren<EnemyController> ();
		foreach (EnemyController comp in componentes) {
			enemigos.Add(comp);
		}
	}

	public void CambiarDireccion() {
		foreach (EnemyController child in enemigos) {
			child.CambiarDireccion();
		}
	}
}
