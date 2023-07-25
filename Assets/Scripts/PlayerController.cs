using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float runSpeedMultiplier = 2f;
    public Vector2 groundCheckSize = new Vector2(1f, 0.2f);
    public Transform groundC;
    public string groundTag = "Ground";
    public LayerMask groundLayer;
    public float jumpForce = 5f;
    public float verticalSpeed = 5f;
    [SerializeField] private int vidaPersonaje = 3; // Variable de vida visible en el Inspector
    [SerializeField] private int dañoJugador = 1; // Cantidad de daño que el jugador causa al enemigo
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isGrounded;
    private bool isJumping;
    private bool isRunning;
    private bool isFalling;
    private bool isFrozen; // Variable para indicar si el jugador está congelado
    private float posColX = 3.110583f;
    private float posColY = -2.440398f;

    public PlayerWallClimb pwc;

    [SerializeField] private GameObject collisionNormal;
    [SerializeField] private GameObject collisionClimb;
    [SerializeField] private HUD HUD;
    [SerializeField] private BoxCollider2D playerAttack;
    [SerializeField] private GameObject canvasGameOver; // Referencia al canvas de Game Over

    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private float attackDuration = 0.5f;
    private bool isAttacking = false;
    private bool isAttackCooldown = false;

    private bool isDead; // Variable para verificar si el jugador está muerto
    [SerializeField] private float deathCooldown = 2f; // Tiempo de cooldown después de morir

    public event System.Action OnPlayerDeath;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        canvasGameOver.SetActive(false); // Desactivar el canvas de Game Over al iniciar el juego
        isDead = false; // Inicializar la variable de muerte como falsa al inicio
    }

    private void Update()
    {
        if (isFrozen) // Si el jugador está congelado, no se procesan las entradas de movimiento
            return;

        float moveInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Horizontal", Mathf.Abs(moveInput));

        if (moveInput < 0)
        {
            playerAttack.offset = new Vector2(1.443688f, -2.440398f);
            playerAttack.size = new Vector2(1.404423f, 1.495153f);
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
            playerAttack.offset = new Vector2(posColX, posColY);
            playerAttack.size = new Vector2(1f, playerAttack.size.y);
            spriteRenderer.flipX = false;
        }

        isGrounded = Physics2D.OverlapBox(groundC.position, groundCheckSize, 0f, groundLayer) && IsGroundWithTag();

        if (isGrounded)
        {
            isJumping = false;
            animator.SetBool("Jump", false);
            animator.SetBool("IsFalling", false);
        }
        else if (!isJumping && rb.velocity.y < 0)
        {
            isFalling = true;
            animator.SetBool("IsFalling", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded && !isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                animator.SetBool("Jump", true);
                animator.SetBool("IsFalling", false);
                Debug.Log("Saltando");
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            isRunning = false;
            animator.SetBool("Run", false);
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                isRunning = false;

                if (moveInput != 0)
                {
                    isRunning = true;
                    animator.SetBool("Run", true);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            CausarHerida(dañoJugador);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isAttacking && !isAttackCooldown)
            {
                animator.SetTrigger("Attack");
                StartCoroutine(EnableAttack());
            }
        }
    }

    private void FixedUpdate()
    {
        if (isFrozen) // Si el jugador está congelado, no se aplica velocidad al Rigidbody
            return;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * GetSpeed() * runSpeedMultiplier, rb.velocity.y);
    }

    private float GetSpeed()
    {
        return isRunning ? speed * runSpeedMultiplier : speed;
    }

    private bool IsGroundWithTag()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(groundC.position, groundCheckSize, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag(groundTag))
            {
                return true;
            }
        }
        return false;
    }

    private IEnumerator EnableAttack()
    {
        isAttacking = true;
        isAttackCooldown = true;
        playerAttack.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        playerAttack.enabled = false;
        yield return new WaitForSeconds(attackCooldown - attackDuration);
        isAttacking = false;
        isAttackCooldown = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if (collision.collider.gameObject.tag == "WallC")
        {
            this.enabled = false;
            pwc.enabled = true;

            if (collision.GetContact(0).point.x - transform.position.x > 0)
            {
                pwc.climbSpeed = Mathf.Abs(pwc.climbSpeed);
            }
            else
            {
                pwc.climbSpeed = Mathf.Abs(pwc.climbSpeed) * -1;
            }

            collisionNormal.SetActive(false);
            collisionClimb.SetActive(true);
        }

        if (collision.collider.gameObject.CompareTag("Pua"))
        {
            TrampaPua trampa = collision.collider.gameObject.GetComponent<TrampaPua>();
            if (trampa != null)
            {
                CausarDaño(); // Llama al método CausarDaño() de TrampaPua
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (groundC == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundC.position, new Vector3(groundCheckSize.x, groundCheckSize.y, 0f));
    }

    public void OnFallAnimationEnd()
    {
        if (isFalling)
        {
            animator.SetBool("IsFalling", false);
            isFalling = false;
        }
    }

    public void CausarHerida(int cantidadDaño)
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje -= cantidadDaño;
            HUD.RestaCorazones(vidaPersonaje);

            if (vidaPersonaje <= 0)
            {
                Morir();
            }
            else
            {
                animator.SetTrigger("Hurt");
            }
        }
    }

    private void Morir()
    {
        isFrozen = true;
        isDead = true;
        animator.SetTrigger("Die");
        Debug.Log("Has muerto");
        OnPlayerDeath?.Invoke();
        StartCoroutine(ShowGameOver());
    }

    private IEnumerator ShowGameOver()
    {
        yield return new WaitForSeconds(deathCooldown); // Esperar el tiempo de cooldown
        canvasGameOver.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CausarDaño()
    {
        if (vidaPersonaje > 0)
        {
            vidaPersonaje--;
            HUD.RestaCorazones(vidaPersonaje);

            if (vidaPersonaje <= 0 && !isDead) // Agregar condición para verificar que el jugador no está muerto
            {
                animator.SetTrigger("Die");
                isFrozen = true;
                isDead = true; // Establecer la variable de muerte como verdadera
                Debug.Log("Has muerto");
                OnPlayerDeath?.Invoke();
                StartCoroutine(ShowGameOver()); // Iniciar el cooldown para mostrar el Game Over
            }
            else
            {
                animator.SetTrigger("Hurt");
            }
        }
    }
}