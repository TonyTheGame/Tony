using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
public class Aura : MonoBehaviour
{

    // Tiempo de precarga
    public float waitBeforePlay;

    Animator anim;
    Coroutine manager;
    bool loaded;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void AuraStart()
    {
        manager = StartCoroutine(Manager());
        anim.Play("Aura_quieta");
    }

    public void AuraStop()
    {
        StopCoroutine(manager);
        anim.Play("Aura_Jugando");
        loaded = false;
    }

    // Método para comprobar si ya hemos cargado suficiente
    public IEnumerator Manager()
    {
        yield return new WaitForSeconds(waitBeforePlay);
        anim.Play("Aura_Jugando");
        loaded = true;
    }

    // Método para comprobar si ya hemos cargado suficiente
    public bool IsLoaded()
    {
        return loaded;
    }
}