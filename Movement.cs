using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Variables globales
    public int speed,
               turnSpeed;

    float h,
          v;

    // Update is called once per frame
    void Update()
    {
        InputPlayer();
        MovementPlayer();
    }

    //Método propio para mapear las teclas de nuestro teclado
    void InputPlayer()
    {
        h = Input.GetAxis("Horizontal"); //-1 a la A, 0 si no me muevo, 1 a la D
        v = Input.GetAxis("Vertical"); //-1 a la S, 0 si no me muevo, 1 a la W
    }

    void MovementPlayer()
    {
        transform.Translate(Vector3.forward * v * speed * Time.deltaTime);
        transform.Rotate(Vector3.up * h * turnSpeed * Time.deltaTime);
    }
}
