using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgregarPuntos : MonoBehaviour {
    public int puntaje = 10;
    void OnDestroy()
    {
        GameController.Score += puntaje;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
