using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAD : MonoBehaviour
{

    [Tooltip("Velocidad de movimiento en unidades del mundo")]
    public float speed;

    GameObject player;   // Recuperamos al objeto jugador 
    GameObject dona;
    Rigidbody2D rb2d;    // Recuperamos el componente de cuerpo rígido
    Vector3 target, dir; // Vectores para almacenar el objetivo y su dirección

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();
        dona = GameObject.FindGameObjectWithTag("Dona");

        // Recuperamos posición del jugador y la dirección normalizada
        Vector3 v = new Vector3(1, 1);
        dir = v;
    }

    void FixedUpdate()
    {
        // Si hay un objetivo movemos la roca hacia su posición
        rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Si chocamos contra el jugador o un ataque la borramos
        if (col.transform.tag == "Player" || col.transform.tag == "Attack")
        {
            if (col.tag == "Player") col.SendMessage("Attacked");
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible()
    {
        // Si se sale de la pantalla borramos la roca
        Destroy(gameObject);
    }
}