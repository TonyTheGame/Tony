using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgregarScore : MonoBehaviour {
    public int puntaje = 10;

    // Use this for initialization
    private void OnDestroy()
    {
        GameController.Score += puntaje;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
