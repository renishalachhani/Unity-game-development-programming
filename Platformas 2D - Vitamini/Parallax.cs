using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float distanceX; //La distancia que se encuentra hasta el fondo
    public float smoothingX; //Velocidad

    Transform cam;
    Vector3 previousCamPos; //Guarda la posición de la cámara en el frame anterior

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform; //Inicializo la variable transform a la cámara principal
        previousCamPos = cam.position; //Guardar la posición de la cámara en el frame anterior
    }

    // Update is called once per frame
    void Update()
    {
        EffectParallax();
    }

    void EffectParallax()
    {
        //!=0 se movera todo el porque distanceX nunca va a ser 0
        if (distanceX != 0)
        {
            //previousCamPos.x = posición de la cámara en el frame anterior 
            //cam.position.x = posición de la cámara en el frame actual
            //Si la cámara se ha movido desde el frame anterior y lo multiplicamos por distanceX
            //si la cámara no se ha movido, parallax será cero
            float parallaxX = (previousCamPos.x - cam.position.x) * distanceX;

            //Calcular la posición a la que queremos mover el background (solo lo moveremos en x)
            //A x le sumemos el valor de parallaxX
            Vector3 backgroundTargetPosX = new Vector3(transform.position.x + parallaxX,
                transform.position.y, transform.position.z);

            //Lerp es la interpolación lineal para llegar de un punto a otro a velocidad constante
            //Aplico el Lerp para tener un movimiento suave guapo molon :)
            //1º Posición actual
            //2º La posición a la que quiero llegar (si ParallaxX es 0 ambas serán la misma)
            //3º La velocidad a la que quiero que llegue desde el punto y al punto a
            transform.position = Vector3.Lerp(transform.position, backgroundTargetPosX,
                smoothingX * Time.deltaTime);

            //Guardar la posición de la cámara para comprobarla en el siguiente frame
            previousCamPos = cam.position;
        }
    }
}
