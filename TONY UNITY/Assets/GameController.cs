using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {

    public static int Score = 0;
    public string ScoreString = "PUNTAJE: ";
    public Text TextScore;
    public static GameController Gamecontroller;

    void Awake()
    {
        Gamecontroller = this;
    }

	void Update ()
    {

		if(TextScore!=null)
        {
            TextScore.text = ScoreString + Score.ToString();
        }
    }
}
