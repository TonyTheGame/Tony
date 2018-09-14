using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBossNivelUno : MonoBehaviour {
    public GameObject Dona;
    int contador = 0;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.Score >=230&&contador==0)
             {
            contador ++;
                Instantiate(Dona, transform.position, transform.rotation);

             }
             
             }
        }

