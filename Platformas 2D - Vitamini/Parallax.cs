using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float distanceX; //La distancia que se encuentra hasta el fondo
    public float smoothingX; //Velocidad

    Transform cam;
    Vector3 previousCamPos; //Guarda la posici�n de la c�mara en el frame anterior

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform; //Inicializo la variable transform a la c�mara principal
        previousCamPos = cam.position; //Guardar la posici�n de la c�mara en el frame anterior
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
            //previousCamPos.x = posici�n de la c�mara en el frame anterior 
            //cam.position.x = posici�n de la c�mara en el frame actual
            //Si la c�mara se ha movido desde el frame anterior y lo multiplicamos por distanceX
            //si la c�mara no se ha movido, parallax ser� cero
            float parallaxX = (previousCamPos.x - cam.position.x) * distanceX;

            //Calcular la posici�n a la que queremos mover el background (solo lo moveremos en x)
            //A x le sumemos el valor de parallaxX
            Vector3 backgroundTargetPosX = new Vector3(transform.position.x + parallaxX,
                transform.position.y, transform.position.z);

            //Lerp es la interpolaci�n lineal para llegar de un punto a otro a velocidad constante
            //Aplico el Lerp para tener un movimiento suave guapo molon :)
            //1� Posici�n actual
            //2� La posici�n a la que quiero llegar (si ParallaxX es 0 ambas ser�n la misma)
            //3� La velocidad a la que quiero que llegue desde el punto y al punto a
            transform.position = Vector3.Lerp(transform.position, backgroundTargetPosX,
                smoothingX * Time.deltaTime);

            //Guardar la posici�n de la c�mara para comprobarla en el siguiente frame
            previousCamPos = cam.position;
        }
    }
}
