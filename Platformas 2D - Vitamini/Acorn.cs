using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    //Variables globales
    public float force;
    public float forceTorque;

    Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        AddForce();
    }

    void AddForce()
    {
        rb2D.AddForce(Vector2.up * force);
        rb2D.AddForce(Random.Range(-1, 1) * Vector2.right * force);
        //Fuerza de giro
        rb2D.AddTorque(forceTorque);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Si la bellota colisiona con un gameobject con layer Ground
        //Entonces desactivo el isTrigger
        if (collision.gameObject.layer == 3)
        {
            GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}
