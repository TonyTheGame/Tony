using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // Variables para gestionar el radio de visión, el de ataque y la velocidad
    public float visionRadius;
    public float attackRadius;
    public float speed;

    // Variables relacionadas con el ataque
    [Tooltip("Prefab de la roca que se disparará")]
    public GameObject rockPrefab;
    [Tooltip("Velocidad de ataque (segundos entre ataques)")]
    public float attackSpeed = 2f;
    bool attacking;

    ///--- Variables relacionadas con la vida
    [Tooltip("Puntos de vida")]
    public int maxHp = 3;
    [Tooltip("Vida actual")]
    public int hp;

    // Variable para guardar al jugador
    GameObject player;

    // Variable para guardar la posición inicial
    Vector3 initialPosition, target;

    // Animador y cuerpo cinemático con la rotación en Z congelada
    Animator anim;
    Rigidbody2D rb2d;

    void Start () {

        // Recuperamos al jugador gracias al Tag
        player = GameObject.FindGameObjectWithTag("Player");

        // Guardamos nuestra posición inicial
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        ///--- Iniciamos la vida
        hp = maxHp;
    }

    void Update () {

        // Por defecto nuestro target siempre será nuestra posición inicial
        target = initialPosition;

        // Comprobamos un Raycast del enemigo hasta el jugador
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, 
            player.transform.position - transform.position, 
            visionRadius, 
            1 << LayerMask.NameToLayer("Default") 
                // Poner el propio Enemy en una layer distinta a Default para evitar el raycast
                // También poner al objeto Attack y al Prefab Slash una Layer Attack 
                // Sino los detectará como entorno y se mueve atrás al hacer ataques
        );

        // Aquí podemos debugear el Raycast
        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);

        // Si el Raycast encuentra al jugador lo ponemos de target
        if (hit.collider != null) {
            if (hit.collider.tag == "Player"){
                target = player.transform.position;
            }
        }

        // Calculamos la distancia y dirección actual hasta el target
        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

        // Si es el enemigo y está en rango de ataque nos paramos y le atacamos
        if (target != initialPosition && distance < attackRadius){
            // Aquí le atacaríamos, pero por ahora simplemente cambiamos la animación
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.Play("Enemy_Walk", -1, 0);  // Congela la animación de andar

            ///-- Empezamos a atacar (importante una Layer en ataque para evitar Raycast)
            if (!attacking) StartCoroutine(Attack(attackSpeed));
        }
        // En caso contrario nos movemos hacia él
        else {
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);

            // Al movernos establecemos la animación de movimiento
            anim.speed = 1;
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.SetBool("walking", true);
        }

        // Una última comprobación para evitar bugs forzando la posición inicial
        if (target == initialPosition && distance < 0.05f){
            transform.position = initialPosition; 
            // Y cambiamos la animación de nuevo a Idle
            anim.SetBool("walking", false);
        }

        // Y un debug optativo con una línea hasta el target
        Debug.DrawLine(transform.position, target, Color.green);
    }

    // Podemos dibujar el radio de visión y ataque sobre la escena dibujando una esfera
    void OnDrawGizmosSelected() {

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }

    IEnumerator Attack(float seconds){
        attacking = true;  // Activamos la bandera
        // Si tenemos objetivo y el prefab es correcto creamos la roca
        if (target != initialPosition && rockPrefab != null) {
            Instantiate(rockPrefab, transform.position, transform.rotation);
            // Esperamos los segundos de turno antes de hacer otro ataque
            yield return new WaitForSeconds(seconds);
        }
        attacking = false; // Desactivamos la bandera
    }

    ///--- Gestión del ataque, restamos una vida
    public void Attacked(){
        if (--hp <= 0) Destroy(gameObject);
    }

    ///---  Dibujamos las vidas del enemigo en una barra 
    void OnGUI() {
        // Guardamos la posición del enemigo en el mundo respecto a la cámara
        Vector2 pos = Camera.main.WorldToScreenPoint (transform.position);

        // Dibujamos el cuadrado debajo del enemigo con el texto
        GUI.Box(
            new Rect(
                pos.x - 20,                   // posición x de la barra
                Screen.height - pos.y + 60,   // posición y de la barra
                40,                           // anchura de la barra    
                24                            // altura de la barra  
            ), hp + "/" + maxHp               // texto de la barra
        );
    }

}