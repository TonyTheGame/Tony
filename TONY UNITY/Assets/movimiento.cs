﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class movimiento : MonoBehaviour
{
    public float speed = 4f;

    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov;  // Ahora es visible entre los métodos

    CircleCollider2D attackCollider;
    public GameObject bananaPrefab;
    bool movePrevent;
    bool shootPrevent;
    double coolDownTime = 0.8;
    double nextFireTime = 0;
     public GameObject initialMap;

    [Tooltip("Puntos de vida")]
    public int maxHp = 10;
    [Tooltip("Vida actual")]
    public int hp;

    void Awake()
       {
           Assert.IsNotNull(initialMap);
       }

    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        // Recuperamos el collider de ataque del primer hijo
        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();

        // Lo desactivamos desde el principio, luego
        attackCollider.enabled = false;

          Camera.main.GetComponent<MainCamera>().SetBound(initialMap);
        hp = maxHp;
    }

    void Update()
    {

        Movements();
        Animations();
        MeleeAttack();
        PreventMovement();
        ADCAttack();


    }

    void FixedUpdate()
    {
        // Nos movemos en el fixed por las físicas
        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
    }
    void Movements()
    {
        // Detectamos el movimiento en un vector 2D
        mov = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }
    void Animations()
    {
        if (mov != Vector2.zero)
        {
            anim.SetFloat("movX", mov.x);
            anim.SetFloat("movY", mov.y);
            anim.SetBool("caminar", true);
        }
        else
        {
            anim.SetBool("caminar", false);
        }
    }
    void MeleeAttack()
    {

        // Buscamos el estado actual mirando la información del animador
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("tony_ataque");

        // Detectamos el ataque, tiene prioridad por lo que va abajo del todo
        if (Input.GetKeyDown("space") && !attacking)
        {
            anim.SetTrigger("atacando");
        }

        // Vamos actualizando la posición de la colisión de ataque
        if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x / 2, mov.y / 2);

        // Activamos el collider a la mitad de la animación de ataque
        if (attacking)
        {
            float playbackTime = stateInfo.normalizedTime;
            if (playbackTime > 2 && playbackTime < 3) attackCollider.enabled = true;
            else attackCollider.enabled = false;
        }
     
    }
    void ADCAttack()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool loading = stateInfo.IsName("tony_poder");

        
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (Time.time > nextFireTime)
            {
                anim.SetTrigger("atacando");

            // Para que se mueva desde el principio tenemos que asignar un
            // valor inicial al movX o movY en el edtitor distinto a cero
            float angle = Mathf.Atan2(
                anim.GetFloat("movY"),
                anim.GetFloat("movX")
            ) * Mathf.Rad2Deg;

            GameObject slashObj = Instantiate(
                bananaPrefab, transform.position,
                Quaternion.AngleAxis(angle, Vector3.forward)
            );

            Slash slash = slashObj.GetComponent<Slash>();
            slash.mov.x = anim.GetFloat("movX");
            slash.mov.y = anim.GetFloat("movY");
            nextFireTime = Time.time + coolDownTime;
                
            }
              
        }

        }
            
    void PreventMovement()
    {
        if (movePrevent)
        {
            mov = Vector2.zero;
        }
    }
    IEnumerator EnableMovementAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        movePrevent = false;
    }
    public void Attacked()
    {
        if (--hp <= 0) Destroy(gameObject);

    }

    ///---  Dibujamos las vidas del enemigo en una barra 
    void OnGUI()
    {
        // Guardamos la posición del enemigo en el mundo respecto a la cámara
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);

        // Dibujamos el cuadrado debajo del enemigo con el texto
        GUI.Box(
            new Rect(
                pos.x - 20,                   // posición x de la barra
                Screen.height - pos.y + 60,   // posición y de la barra
                40,                           // anchura de la barra    
                24                            // altura de la barra  
            ), hp + "/" + maxHp               // texto de la barra
        );
       /* GUI.Box(
            new Rect(
                pos.x - 100,                   // posición x de la barra
                Screen.height - pos.y + 200,   // posición y de la barra
                80,                           // anchura de la barra    
                30                            // altura de la barra  
            ), "puntos"                // texto de la barra
        );*/
    }
  
    
}