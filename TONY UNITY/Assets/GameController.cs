using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameController : MonoBehaviour {
    public static int score = 0;
    public string scoreString="score";
    public Text textScore;
    public static GameController gameController;
    void Awake()
    {
        gameController = this;
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (textScore != null)
        {
            textScore.text = scoreString + score.ToString();
        }
	}
}
