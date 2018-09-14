using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPollo1 : MonoBehaviour {

    public GameObject pollo;
    int contador = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Score >= 630 && contador == 0)
        {
            contador++;
            Instantiate(pollo, transform.position, transform.rotation);

        }

    }
}
