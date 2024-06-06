using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse : MonoBehaviour
{

    Rigidbody rb;
    public float thrust;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        //Aplico x fuerza al eje y (transform.up)
        rb.AddForce(transform.up * thrust);
        rb.useGravity = true;
    }

    private void OnMouseUp()
    {
        Debug.Log("Suelto el botón izquierdo del ratón.");
    }

    private void OnMouseEnter()
    {
        Debug.Log("Paso el ratón por encima del GameObject.");
    }

    private void OnMouseDrag()
    {
        Debug.Log("Arrastro el cursor.");
    }
}
