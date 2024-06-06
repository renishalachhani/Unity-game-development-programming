using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 offset; //distancia inicial entre c�mara y el player
    public float smoothTargetTime; //Tiempo de retardo que tarda la c�mara en ir siguiendo a Vitamini

    Vector3 smoothDampVelocity; //Unity me obliga, yo no quiero
    
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position; //cojo la distancia inicial entre c�mara y player
    }

    // Update is called once per frame
    void Update()
    {
        //1� Posici�n actual
        //2� Perserguir a la ardilla, con cierto margen de distancia (la distancia inicial de c�mara y vitamini)
        //3� Unity me obliga
        //El tiempo de retardo para llegar a la velocidad que queremos
        transform.position = Vector3.SmoothDamp(transform.position, player.position + offset,
            ref smoothDampVelocity, smoothTargetTime);
    }
}
