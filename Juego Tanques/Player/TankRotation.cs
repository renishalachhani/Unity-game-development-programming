using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankRotation : MonoBehaviour
{
    [SerializeField]
    public int turnSpeed = 5;
    public Transform ob; //Torreta del tanque está como hijo

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        //giro la torreta con el input
        ob.Rotate(Vector3.up * 40 * Time.deltaTime * h);

        if (Input.GetKey(KeyCode.E))
        {
            Turn();
        }
        if (Input.GetKey(KeyCode.E))
        {
            ob.SetParent(this.transform);
        }
    }

    void Turn()
    {
        ob.SetParent(null);
        //Creo una rotación en base al eje Z de la torreta, dibujandola
        Quaternion rot = Quaternion.LookRotation(ob.forward);
        //Giro el tanque para que se alinee con la torreta, alineamos el z del tanque
        //con el Z de la torreta
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, turnSpeed * Time.deltaTime);
    }
}
