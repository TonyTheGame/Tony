using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnNivelUno : MonoBehaviour {
    public GameObject Enemigo;
    double TiempoSpawn;

    // Use this for initialization
    void Start () {
        TiempoSpawn = Time.time + 5;

    }
	
	// Update is called once per frame
	void Update () {
        if (GameController.Score >= 30 &&GameController.Score<230)
        {
            if (TiempoSpawn < Time.time)
            { //This checks wether real time has caught up to the timer
                Instantiate(Enemigo, transform.position, transform.rotation); //This spawns the emeny
                TiempoSpawn = Time.time + 15; // THIS (15) IS THE SECONDS (TIME) IN WHICH THE ENEMY IS GOING TO SPAWN FOR THE SECOND TIME 
            }
        }

    }
}
