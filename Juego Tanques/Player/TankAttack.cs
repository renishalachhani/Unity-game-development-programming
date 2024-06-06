using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAttack : MonoBehaviour
{
    //Aquí voy a meter el prefab de la bala PERO en vez de hacer la variable tipo Gameobject
    //la he declarado Rigidbody porque voy a acceder a su RB y así me ahorro el gamecomponent
    [SerializeField] Rigidbody shellPrefab;
    [SerializeField] Transform fireTransform;//punto de salida de la bala
    [SerializeField] float launchForce;
    [SerializeField] AudioSource audioSource;//Vamos a meter aquí el audioSource que tiene el
    //Gameobject de la escena "FireTransform"

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) Launch();    
    }
    void Launch()
    {
        //Instancio un clone del prefab como rigidbody
        Rigidbody cloneShell = Instantiate(shellPrefab, fireTransform.position,
            fireTransform.rotation) as Rigidbody;

        audioSource.Play();//reproduzco el audiosource que lleva dentro el clip de audio

        //aplico velocidad al clone del prefab con una dirección y una magnitud
        cloneShell.velocity = fireTransform.forward * launchForce;
    }
}
