using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Warp : MonoBehaviour
{
    // Para almacenar el destino del Warp
    public GameObject target;
    public GameObject targetMap;

    void Awake()
    {
        // Nos aseguraremos de que target se ha establecido o lanzaremos error
        Assert.IsNotNull(target);

        // Si queremos podemos esconder los Warps
        GetComponent<SpriteRenderer>().enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        // Actualizamos la posición
        col.transform.position = target.transform.position;

        Camera.main.GetComponent<MainCamera>().SetBound(targetMap);

    }

}