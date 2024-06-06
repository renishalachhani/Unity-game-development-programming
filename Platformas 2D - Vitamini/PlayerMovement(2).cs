using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    //Variables locales
    [Header("Move")]
    [SerializeField]
    private float speed, //aplica la velocidad
                  smoothTime; //tiempo para llegar a la velocidad que queremos

    [Header("Jump")]
    [SerializeField]
    private float jumpForce;

    private bool jumpPressed;

    [Header("Raycast")]
    [SerializeField]
    private Transform groundCheck; //punto de origen del raycast (las patucas de la ardilla)
    [SerializeField]
    private LayerMask groundLayer; //Capa suelo
    [SerializeField]
    private float rayLength; //La longitud del raycast
    [SerializeField]
    private bool isGrounded; //�Est� tocando el suelo o no?

    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI textAcornUI;

    private Rigidbody2D rb2D;
    //la variable que representa la velocidad a la que quiero que se mueva la ardilla
    private Vector2 targetVelocity;
    private Vector2 dampVelocity;

    private SpriteRenderer spriteRenderer;
    private Animator anim;

    //Aqu� guardaremos el n�mero de bellotas que hemos cogido
    private int numAcorn;


    // Start is called before the first frame update
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //h ser� 1, 0 o -1 seg�n si pulso la a (izquierda -1), la d (derecha 1) o nada
        float h = Input.GetAxis("Horizontal");

        //Vector2(x, y);
        //rb2D.velocity.y es un get que coge la velocidad que hay en y
        targetVelocity = new Vector2(h * speed, rb2D.velocity.y);

        //SALTO
        JumpSpace();

        //Raycast
        DrawRaycast();

        //Cambio de Gravedad
        ChangeGravity();

        //ANIMACIONES
        Flip(h);
        Animating(h);
    }

    void FixedUpdate()
    {
        //SmoothDamp es un m�todo que pertenece al struct de Vector2 o Vector3.
        //Este m�todo se encarga de cambiar de forma gradual el valor de Vector2

        //1� par�metro es el valor de la velocidad del vector ACTUAL
        //2� par�metro es el target, el valor al que queremos llegar
        //(A LA QUE QUEREMOS LLEGAR)
        //3� UNITY ME OBLIGA A PONER LA PALABRA RESERVADA REF delante de la variable dampVelocity
        //YO NO QUIERO, UNITY ME OBLIGA
        //4� par�metro es el tiempo que va a tardar en llegar del valor actual
        //al valor objetivo
        rb2D.velocity = Vector2.SmoothDamp(rb2D.velocity, targetVelocity, 
            ref dampVelocity, smoothTime);

        //Llamamos al m�todo al salto mientras hayamos pulstado la tecla espacio
        if (jumpPressed == true)
        {
            Jump();
        }
    }

    //A�adimos al rigidbody del personaje fuerza hacia arriba
    void Jump()
    {
        jumpPressed = false;
        rb2D.AddForce(Vector2.up * jumpForce);
    }

    void JumpSpace()
    {
        //La condici�n para que salte el personaje es:
        //pulsar la barra espaciadora y que isGrounded sea verdadero
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            jumpPressed = true;
        }
    }

    //RAYCAST
    void DrawRaycast()
    {
        //CREAMOS NUESTRO RAYCAST
        //1� Punto de origen
        //2� La direcci�n
        //3� La longitud
        //4� La m�scara con la que queremos que interactue
        isGrounded = Physics2D.Raycast(groundCheck.position, Vector2.down, rayLength, groundLayer);
        //AQU� ES PARA VER EL RAYCAST, PERO A�N NO HEMOS CREADO EL RAYO
        //1� establecemos el punto de origen
        //2� la direcci�n y la longitud
        //3� dibujar con un color el rayo
        Debug.DrawRay(groundCheck.position, Vector2.down * rayLength, Color.red);
    }

    //Cambiemos la gravedad del salto
    void ChangeGravity()
    {
        if (rb2D.velocity.y < 0) //Si la ardilla cae la gravedad ser� m�s pesada
        {
            rb2D.gravityScale = 1.5f;
        }
        else //Si la ardilla asciende la gravedad sea m�s pesada
        {
            rb2D.gravityScale = 1; 
        }
    }

    //Gira la ardilla a izquierda o derecha seg�n hacia donde se mueva
    void Flip(float h)
    {
        //Si estoy pulsando la tecla d
        if (h > 0)
        {
            spriteRenderer.flipX = false;
        }
        //Si estoy pulsando la a
        else if(h < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    //Le daremos los valores a los par�metros creado en el animator
    void Animating(float h)
    {
        if (h != 0)
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        //Si isGrounded es igual a false es porque no estoy tocando el suelo
        //Entonces me puedo hacer bolita
        anim.SetBool("IsJumping", !isGrounded);
    }

    //Atacar al enemigo
    void AttackEnemy(GameObject enemy)
    {
        if (isGrounded == true)
        {
            //Salimos del m�todo si la ardilla est� tocando el suelo
            return;
        }

        //Aqu� salto sobre el enemigo, si no estoy sobre el suelo
        rb2D.AddForce(Vector2.up * jumpForce);
        //Activamos el Trigger llamado Death en el animator, para que haga la animaci�n de bomba de humo
        enemy.GetComponent<Animator>().SetTrigger("Death");
        //Llamamos a Loot, para que salgan las bellotas del cuerpo de la hormiga
        enemy.GetComponent<Enemy>().Loot();
        //Destroy para hacer que el gameobject de la hormiga desaparezca
        Destroy(enemy, 0.3f);
    }

    //Vamos a hacer que nuestra ardilla atrape bellotas
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            AttackEnemy(collision.gameObject);
        }
        else if (collision.collider.CompareTag("Acorn"))
        {
            Destroy(collision.collider.gameObject);
            numAcorn++;
            textAcornUI.text = numAcorn.ToString();
        }
    }
}
