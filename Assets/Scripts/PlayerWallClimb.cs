using System.Collections;
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
    [SerializeField] private float climbEndTime;
    Coroutine endOfClimb = null;

    [SerializeField] private GameObject collisionNormal;
    [SerializeField] private GameObject collisionClimb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (endOfClimb != null) StopCoroutine(endOfClimb);
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
            Debug.Log("Hola Mundo", collision.collider.gameObject);
            if(endOfClimb != null) StopCoroutine(endOfClimb);
            endOfClimb = StartCoroutine(ExitClimbMode(collision));
        }
    }

    
    public IEnumerator ExitClimbMode(Collision2D collision)
    {
        yield return new WaitForSeconds(climbEndTime);
        ExitMode();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);
        if (collision.collider.gameObject.tag == "Ground")
        {
            ExitMode();
        }
    }

    public void ExitMode()
    {
        collisionNormal.SetActive(true);
        collisionClimb.SetActive(false);
        this.enabled = false;
        pc.enabled = true;
    }
}