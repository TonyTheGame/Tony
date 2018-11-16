using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorVida : MonoBehaviour {

    public Sprite vida5;
    public Sprite vida4;
    public Sprite vida3;
    public Sprite vida2;
    public Sprite vida1;
    public Image imagenActual;
    public static ControladorVida controladorVida;
	// Use this for initialization
	void Start () {
        controladorVida = this;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (imagenActual != null)
        {
            if (movimiento.contador == 5) controladorVida.imagenActual.sprite =vida5;
            if(movimiento.contador==4) controladorVida.imagenActual.sprite = vida4;
            if (movimiento.contador == 3) controladorVida.imagenActual.sprite = vida3;
            if (movimiento.contador == 2) controladorVida.imagenActual.sprite = vida2;
            if (movimiento.contador == 1) controladorVida.imagenActual.sprite = vida1;
        }
    }
}
