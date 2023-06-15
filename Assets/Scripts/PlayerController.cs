using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeedMultiplier = 2f;
    public float groundCheckRadius = 0.2f;
    public Transform groundC;
    public string groundTag = "Ground";
    public LayerMask groundLayer;
    public float jumpForce = 5f; // Fuerza del salto
    public float verticalSpeed = 5f; // Velocidad vertical del personaje

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isGrounded;
    private bool isJumping; // Indica si el personaje está saltando
    private bool isRunning; // Indica si el personaje está corriendo

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Cambiar el parámetro "Horizontal" en el Animator cuando el personaje se mueve
        animator.SetFloat("Horizontal", Mathf.Abs(moveInput));

        // Voltear el sprite si el personaje se mueve hacia la izquierda
        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        // Voltear el sprite si el personaje se mueve hacia la derecha
        else if (moveInput > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Verificar si el personaje está en contacto con el suelo
        isGrounded = Physics2D.OverlapCircle(groundC.position, groundCheckRadius, groundLayer) &&
                     IsGroundWithTag();

        // Salto
        if (isGrounded)
        {
            isJumping = false; // Si el personaje está en el suelo, no está saltando
            // animator.SetBool("Jump", false); // Cambia el parámetro "Jump" a false
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded && !isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true; // El personaje está saltando
                animator.SetBool("Jump", true); // Cambia el parámetro "Jump" a true
                Debug.Log("Saltando");
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isRunning = false;
            animator.SetBool("Run", false); // Cambia el parámetro "Run" a false
        }
        else
        {
            // Cambiar entre Idle y Run al presionar y soltar Shift
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                isRunning = false; // Reiniciar el estado de correr

                if (moveInput != 0)
                {
                    isRunning = true;
                    animator.SetBool("Run", true); // Cambia el parámetro "Run" a true
                }
            }
        }


    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * GetSpeed() * runSpeedMultiplier, rb.velocity.y);
    }

    float GetSpeed()
    {
        return isRunning ? speed * runSpeedMultiplier : speed;
    }

    bool IsGroundWithTag()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundC.position, groundCheckRadius);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(groundTag))
            {
                return true;
            }
        }
        return false;
    }
}