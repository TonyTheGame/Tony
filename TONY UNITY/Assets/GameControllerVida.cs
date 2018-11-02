using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerVida : MonoBehaviour {

    public static int vida = 0;
    public string vidaString = "vida";
    public Text TextVida;
    public static GameControllerVida gameControllerVida;

    void Awake()
    {
        gameControllerVida = this;
    }

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        if (TextVida != null) {
            //TextVida.text
                }// terminar , me quede aca xd 7:30 https://www.youtube.com/watch?v=o387QqTrogA
    }
}
