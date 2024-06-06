using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonPrefab : MonoBehaviour
{
    //Aquí le indico que prefab es el que quiero clonar
    public GameObject prefabAcorn;
    //Indico la posición donde quiero que aparezca el clon
    public Transform transformChild;
    //Variable para darle fuerza a nuestra bellotita
    public float thrust;


    private void Start()
    {
        InvokeRepeating("CreateAcorn", 5, 2);
    }
    // Update is called once per frame
    void Update()
    {        
        if (Input.GetMouseButtonDown(0))
        {            
            CreateAcorn();
        }
    }

    void CreateAcorn()
    {
        //Quiero crear clones de la bellota
        GameObject cloneAcorn = Instantiate(prefabAcorn, transformChild.position, transformChild.rotation);
        Rigidbody rbAcorn = cloneAcorn.GetComponent<Rigidbody>();

        Destroy(cloneAcorn, 5);

        rbAcorn.AddForce(transform.forward * thrust);
        rbAcorn.AddForce(transform.up * 50);
    }
}
