using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionShell;//hace referencia al sistema de part�culas que tiene
    //la bala como hijo
    AudioSource audioSource;
    Renderer rend;
    Collider coll;
    //Cu�nto da�o va a hacer la bala del player
    public int damagePlayer;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rend = GetComponent<Renderer>();
        coll = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        coll.enabled = false;
        rend.enabled = false;//Deshabilito el meshrenderer de la bala as� no se ve por pantalla
        explosionShell.Play();//reproduzco el sistema de part�culas
        audioSource.Play();
        Destroy(gameObject, 0.5f);//destruyo la bala a los dos segundos
    }
}
