using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float turnSpeed;
    [SerializeField] AudioClip idleClip,
                               drivingClip;

    float horizontal,
          vertical;

    Rigidbody rb;
    AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()//Se ejecuta con cada frame, no es estable
    {
        //LOS INPUT SIEMPRE VAN EN EL UPDATE
        InputPlayer();
        AudioEngine();
    }
    void InputPlayer()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }
    private void FixedUpdate()//Se ejecuta cada 0.02 segundos
    {
        Move();
        Turn();
    }
    void Move()
    {
        //El movimiento que le quiero aplicar al tanque
        //Vector movement (x,y,z) = dirección de movimiento * input * velocidad * Time.deltaTime;
        Vector3 movement = transform.forward * vertical * speed * Time.deltaTime;

        //Aplicar el movimiento:
        //A la posición actual del tanque le sumo el vector movimiento
        rb.MovePosition(transform.position + movement);
    }
    void Turn()
    {
        //calculo los grados que quiero girar
        float turn = horizontal * turnSpeed * Time.deltaTime;

        //Calculo el giro que quiero aplicar
        //Unity trata las rotaciones internamente como quaternions (giros con 4 ejes)
        //Quaternion.Euler devuelve un quaternions a partir de una rotación en grados (los
        //grados que conocemos)
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);

        //Aplico el giro, multiplico mi rotación actual * rotación que quiero aplicar
        rb.MoveRotation(transform.rotation * turnRotation);
    }

    void AudioEngine()
    {
        if(vertical != 0 || horizontal != 0) //el tanque se está moviendo o rotando
        {
            if(audioSource.clip != drivingClip)
            {
                audioSource.clip = drivingClip;
                audioSource.Play();
            }           
        }
        else
        {
            if(audioSource.clip != idleClip)
            {
                audioSource.clip = idleClip;
                audioSource.Play();
            }            
        }
    }
}
