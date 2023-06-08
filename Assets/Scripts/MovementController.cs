using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float speed = 5f; // velocidad de movimiento del personaje
    public float jumpForce = 5f; // fuerza del salto del personaje
    public int maxJumps = 2; // número máximo de saltos permitidos

    private Rigidbody2D rb;
    private bool isGrounded = true; // verifica si el personaje está en el suelo
    private int jumpsRemaining; // saltos restantes disponibles

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // obtenemos la referencia al Rigidbody2D del personaje
        jumpsRemaining = maxJumps; // inicializamos el contador de saltos restantes
    }

    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal"); // obtenemos la entrada horizontal (izquierda/derecha)

        // actualizamos la posición del transform del personaje
        transform.position += Vector3.right * horizontalMovement * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded) // si el personaje está en el suelo
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // aplicamos el impulso del salto
                jumpsRemaining = maxJumps - 1; // restamos uno al contador de saltos restantes
            }
            else if (jumpsRemaining > 0) // si el personaje está en el aire y aún quedan saltos disponibles
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce); // aplicamos el impulso del salto
                jumpsRemaining--; // reducimos el contador de saltos restantes
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsRemaining = maxJumps - 1; // cuando toca el suelo, reiniciamos el contador de saltos restantes
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
