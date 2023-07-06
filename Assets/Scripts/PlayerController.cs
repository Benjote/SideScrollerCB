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

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool isGrounded;
    private bool isJumping;
    private bool isRunning;
    private bool isFalling;

    public PlayerWallClimb pwc;

    [SerializeField] private GameObject collisionNormal;
    [SerializeField] private GameObject collisionClimb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        animator.SetFloat("Horizontal", Mathf.Abs(moveInput));

        if (moveInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveInput > 0)
        {
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
            animator.SetBool("Fall", true);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded && !isJumping)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
                animator.SetBool("Jump", true);
                animator.SetBool("Fall", false);
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
    }

    private void OnDrawGizmosSelected()
    {
        if (groundC == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundC.position, new Vector3(groundCheckSize.x, groundCheckSize.y, 0f));
    }

    // Método llamado desde el evento de animación en el último frame de la animación "Fall"
    public void OnFallAnimationEnd()
    {
        if (isFalling)
        {
            animator.SetBool("Fall", false);
            isFalling = false;
        }
    }
}