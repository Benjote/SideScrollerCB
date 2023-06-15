using UnityEngine;

public class PlayerWallClimb : MonoBehaviour
{
    public string wallTag = "WallC";
    public string climbAnimationParameter = "WallClimb";
    public float climbSpeed = 5f;

    private bool isClimbing = false;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Detectar si el jugador está tocando una pared
        Collider2D wallCollider = Physics2D.OverlapCircle(transform.position, 0.2f, LayerMask.GetMask(wallTag));

        if (wallCollider != null && !isClimbing)
        {
            // El jugador está tocando una pared, comenzar a trepar
            isClimbing = true;
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            animator.SetBool(climbAnimationParameter, true);
        }
        else if (wallCollider == null && isClimbing)
        {
            // El jugador ya no está tocando la pared, detener la escalada
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
    }
}