using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] pointsObjects; //Aqu� coloco los puntos por donde la hormiga va a patrullar
    public int speedWalking; //La velocidad de la hormiga mientras patrulla
    public GameObject acornPrefab; //Indicamos el prefab que vamos a instanciar

    Vector2[] points; //Array con la POSICI�N de la patrulla
    Vector3 posToGo;

    [Header("Player")]
    public float distanceToPlayer; //La distancia a la que le voy a indicar que detecte a la ardilla
    public int speedAttack; //Cuando vea a la ardilla incrementa su velocidad
    public GameObject player;
    public int speedAnimation;

    int speed;
    int i;

    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //speed = 3;
        speed = speedWalking; //Aqu� inicializo la variable speed

        points = new Vector2[pointsObjects.Length];

        for (int i = 0; i < pointsObjects.Length; i++)
        {
            points[i] = pointsObjects[i].position;
        }
        //Inicializamos el posToGo cogiendo la priemra casilla del array
        posToGo = points[0]; //points[0] es el primer caj�n de mi array
    }

    // Update is called once per frame
    void Update()
    {
        //Vamoh a dibujah el raycast
        //Debug.DrawLine(transform.position, player.transform.position, Color.red);

        //Si el player est� en el rango, lo persigo, en caso contrario sigo patrullando
        //Si la distancia entre hormiga y ardilla es menor o igual que la distancia a la que yo
        //quiero que se detecte, entro en modo ataque
        if(Vector2.Distance(transform.position, player.transform.position) <= distanceToPlayer)
        {
            AttackPlayer();
        }
        //En caso contrario, la distancia es mayor y la hormiga solo patrulla
        else
        {
            ChangeTargetPos();
        }        

        //Cambiar el valor del vector desde el valor actual al que queramos
        //Mueves un objeto, en este caso el enemigo, de una posici�n a otra
        //1� La posici�n actual
        //2� Le indico la posici�n a la que debe ir
        //3� Le doy una velocidad
        transform.position = Vector3.MoveTowards(transform.position, posToGo, speed * Time.deltaTime);

        Flip();
    }

    //Para que la hormiga pueda patrullar
    void ChangeTargetPos()
    {
        speed = speedWalking; //Aqu� vuelve a patrullar y lo dejo a su velocidad normal
        animator.speed = 1; //La animaci�n no est� acelerada, vaya a su velocidad normal

        if (transform.position == posToGo) //Cuando la hormiga llegue a la primera posici�n
        {
            //Me muevo entre los cajones de mi array
            if (i == points.Length -1) //Compruebo si estoy en el �ltimo elemento de mi array
            {
                i = 0; //Si estoy en la �ltima posici�n del array, voy a la primera posici�n del array
            }
            else //Como no estoy en el �ltimo caj�n, me mueve al siguiente con un m�s uno
            {
                i++;
            }

            //Actualizo la nueva posici�n a la que tiene que ir la hormiga
            posToGo = points[i];
        }
    }

    void Flip()
    {
        if (posToGo.x > transform.position.x)
        {
            spriteRenderer.flipX = true; //va hacia la derecha
        }
        else if (posToGo.x < transform.position.x)
        {
            spriteRenderer.flipX = false; //va hacia la izquierda
        }
    }

    //Vamos a perseguir ARDILLAS
    void AttackPlayer()
    {
        speed = speedAttack; //Incrementar su velocidad
        animator.speed = speedAnimation; //Aumentamos los samples de la animaci�n para que vaya m�s r�pido
        posToGo = new Vector2(player.transform.position.x, posToGo.y); //Perseguimos el transform del player en x
    }

    //Vamos a hacer que salgan bellotas de la hormiga
    public void Loot()
    {
        GetComponent<CircleCollider2D>().enabled = false; //Le quito el collider a la hormiga
        //Las bellotas se quedan atrapadas en el collider

        for (int i = 0; i < Random.Range(1, 4); i++)
        {
            GameObject acornClone = Instantiate(acornPrefab, transform.position, transform.rotation);
        }
    }
}
