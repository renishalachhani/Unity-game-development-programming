using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Rigidbody shellEnemyPrefab;
    [SerializeField] Transform fireTransform;
    [SerializeField] float timeBetweenAttacks,
                           launchForce,
                           factorLaunchForce;//vamos a controlar la fuerza de la bala dependiendo
    //de la distancia a la que esté el player

    GameObject player;
    float timer,
          distance;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        ray.origin = transform.position;//el origen del raycast es la posición del enemigo
        ray.direction = transform.forward;//dirección del raycast, eje z del tanque enemigo, es decir,
        //"hacia delante"

        //timer = timer + Time.deltaTime;
        timer += Time.deltaTime;//contador de tiempo

        if(Physics.Raycast(ray, out hit))//si el raycast está chocando con algo
        {
            //si el raycast ha chocado con el player Y YA HA PASADO SUFICIENTE TIEMPO DESDE EL ATAQUE ANTERIOR
            if(hit.collider.CompareTag("Player") && timer >= timeBetweenAttacks)
            {
                distance = hit.distance;//me guardo la distancia al player
                Attack();
            }
        }
        DebugRay();
    }
    void Attack()
    {
        timer = 0; //reseteo el contador de tiempo

        //Calcula la fuerza con la que sale la bala
        float launchForceFinal = launchForce * distance * factorLaunchForce;

        Rigidbody cloneShell = Instantiate(shellEnemyPrefab, fireTransform.position,
            fireTransform.rotation) as Rigidbody;

        cloneShell.velocity = fireTransform.forward * launchForceFinal;

    }
    void DebugRay()
    {
        //Dibujo el raycast, le pongo una longitud de 10 y color rojo
        //ESTO SOLO DIBUJA EL RAYO, NO INFLUYE CON NADA EN LA ESCENA
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);
    }
}
