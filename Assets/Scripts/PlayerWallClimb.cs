using UnityEngine;

public class PlayerWallClimb : MonoBehaviour
{
    public string wallTag = "WallC";
    public string climbAnimationParameter = "WallClimb";
    public float climbSpeed = 0.75f;

    private bool isClimbing = false;
    private Rigidbody2D rb;
    private Animator animator;


    private SpriteRenderer spriteRenderer;
    public PlayerController pc;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("WallClimb", true);
    }

    private void OnDisable()
    {
        animator.SetBool("WallClimb", false);
    }

    private void Update()
    {

        float moveInput = Input.GetAxisRaw("Horizontal");

        // Cambiar el parámetro "Horizontal" en el Animator cuando el personaje se mueve
        animator.SetFloat("Horizontal", Mathf.Abs(moveInput));

        /*
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
        */
        /*
        // Detectar si el jugador est� tocando una pared
        Collider2D wallCollider = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask(wallTag));

        if (wallCollider != null && !isClimbing)
        {
            // El jugador est� tocando una pared, comenzar a trepar
            isClimbing = true;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            animator.SetBool(climbAnimationParameter, true);
        }
        else if (wallCollider == null && isClimbing)
        {
            // El jugador ya no est� tocando la pared, detener la escalada
            isClimbing = false;
            rb.gravityScale = 1f;
            animator.SetBool(climbAnimationParameter, false);
        }

        if (isClimbing)
        {
            // Permitir el movimiento vertical mientras se trepa
            float verticalInput = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, verticalInput * climbSpeed);
        }
        */
    }

    private void FixedUpdate()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(rb.velocity.x, moveInput * climbSpeed);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
        if (collision.collider.gameObject.tag == "WallC")
        {
            this.enabled = false;
            pc.enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if (collision.collider.gameObject.tag == "Ground")
        {
            this.enabled = false;
            pc.enabled = true;
        }
    }
}