using UnityEngine;
using System.Collections;

public class FijarBordes : MonoBehaviour {

    public GameObject bordeIzquierdo;
    public GameObject bordeDerecho;
    public float tasaRefresco = 0.5f;

	// Use this for initialization
	void Start () {
        // Se invoca la función Posicionar bordes inmediatamente
        // y se programa su repetición cada tasaRefresco segundos.
        InvokeRepeating("PosicionarBordes", 0, tasaRefresco);
    }

    private void PosicionarBordes()
    {
        // Se recupera la cámara principal
        Camera camera = Camera.main;

        bordeIzquierdo.transform.position = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight / 2, camera.nearClipPlane));
        bordeDerecho.transform.position = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight / 2, camera.nearClipPlane));
    }
}
