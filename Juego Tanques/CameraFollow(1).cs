using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    Transform player;
    [SerializeField]
    float smoothing;

    Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.position; //Distancia entre cámara y player
    }

    void FixedUpdate()
    {
        Vector3 posCamera = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, posCamera, 
            smoothing * Time.deltaTime); //Aquí aplicamos el movimiento
    }
}
