using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class Player : MonoBehaviour {

    public float speed = 4f;
    public GameObject initialMap; 
    public GameObject slashPrefab;

    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov;  // Ahora es visible entre los métodos

    CircleCollider2D attackCollider;

    Aura aura;

    bool movePrevent;

    void Awake() {
        Assert.IsNotNull(initialMap);
        Assert.IsNotNull(slashPrefab);
    }

    void Start () {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        //* Recuperamos el collider de ataque y lo desactivamos
        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;  

        Camera.main.GetComponent<MainCamera>().SetBound(initialMap);

        aura = transform.GetChild(1).GetComponent<Aura>();
    }

    void Update () {  

        // Detectamos el movimiento
        Movements ();

        // Procesamos las animaciones
        Animations ();

        // Ataque con espada
        SwordAttack (); 

        // Ataque con rayo maestro
        SlashAttack ();

        // Prevenir movimiento
        PreventMovement ();

    }

    void FixedUpdate () {
        // Nos movemos en el fixed por las físicas
        rb2d.MovePosition(rb2d.position + mov * speed * Time.deltaTime);
    }

    void Movements () {
        // Detectamos el movimiento en un vector 2D
        mov = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
    }

    void Animations () {

        if (mov != Vector2.zero) {
            anim.SetFloat("movX", mov.x);
            anim.SetFloat("movY", mov.y);
            anim.SetBool("walking", true);
        } else {
            anim.SetBool("walking", false);
        }
    }

    void SwordAttack () {

        // Vamos actualizando la posición de la colisión de ataque
        if (mov != Vector2.zero) {
            attackCollider.offset = new Vector2(mov.x/2, mov.y/2);
        }

        // Buscamos el estado actual mirando la información del animador
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("Player_Attack");

        // Detectamos el ataque, tiene prioridad por lo que va abajo del todo
        if ((Input.GetKeyDown("space") || Input.GetKeyDown(KeyCode.X)) && !attacking ){  
            anim.SetTrigger("attacking");
        }

        // Activamos el collider a la mitad de la animación de ataque
        if(attacking) { // El normalized siempre resulta ser un ciclo entre 0 y 1 
            float playbackTime = stateInfo.normalizedTime;

            if (playbackTime > 0.33 && playbackTime < 0.66) attackCollider.enabled = true;
            else attackCollider.enabled = false;
        }

    }

    void SlashAttack () {
        // Buscamos el estado actual mirando la información del animador
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool loading = stateInfo.IsName("Player_Slash");

        // Ataque a distancia
        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.Z)){ 
            anim.SetTrigger("loading");
            aura.AuraStart();
        } else if (Input.GetKeyUp(KeyCode.LeftAlt) || Input.GetKeyUp(KeyCode.Z)){ 
            anim.SetTrigger("attacking");
            if (aura.IsLoaded()) {
                // Para que se mueva desde el principio tenemos que asignar un
                // valor inicial al movX o movY en el edtitor distinto a cero
                float angle = Mathf.Atan2(
                    anim.GetFloat("movY"), 
                    anim.GetFloat("movX")
                ) * Mathf.Rad2Deg;

                GameObject slashObj = Instantiate(
                    slashPrefab, transform.position, 
                    Quaternion.AngleAxis(angle, Vector3.forward)
                );

                Slash slash = slashObj.GetComponent<Slash>();
                slash.mov.x = anim.GetFloat("movX");
                slash.mov.y = anim.GetFloat("movY");
            }
            aura.AuraStop();
            StartCoroutine(EnableMovementAfter(0.4f));
        } 

        // Prevenimos el movimiento mientras cargamos
        if (loading) { 
            movePrevent = true;
        }

    }

    void PreventMovement () {
        if (movePrevent) { 
            mov = Vector2.zero;
        }
    }

    IEnumerator EnableMovementAfter(float seconds){
        yield return new WaitForSeconds(seconds);
        movePrevent = false;
    }
    
}