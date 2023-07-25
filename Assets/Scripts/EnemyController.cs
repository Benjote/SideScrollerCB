using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int maxHealth = 3;
    public float attackDistance = 2f;
    public float attackCooldown = 2f;
    public float attackDamage = 1f;
    public float moveSpeed = 2f;

    public Animator animator;
    public Transform playerTransform;

    private int currentHealth;
    private bool isDead;
    private bool isAttacking;
    private bool isHitting;
    private bool isMoving;

    private void Start()
    {
        currentHealth = maxHealth;
        isDead = false;
        isAttacking = false;
        isHitting = false;
        isMoving = false;
    }

    private void Update()
    {
        if (isDead)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= attackDistance)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                StartCoroutine(AttackPlayer());
            }
        }
        else if (distanceToPlayer > attackDistance && !isHitting)
        {
            isMoving = true;
            MoveTowardsPlayer();
        }
        else
        {
            isMoving = false;
        }

        if (isMoving && !isHitting && !isAttacking)
            animator.SetBool("IsMoving", true);
        else
            animator.SetBool("IsMoving", false);
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        FlipSprite(direction.x);
    }

    private IEnumerator AttackPlayer()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f); // Time of the attack animation

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackDistance)
        {
            // Apply damage to player
            PlayerController playerController = playerTransform.GetComponent<PlayerController>();
            if (playerController != null)
            {
                playerController.CausarHerida((int)attackDamage);
                isHitting = true;
                animator.SetTrigger("Hit");
                yield return new WaitForSeconds(1f); // Time of the hit animation
                isHitting = false;
            }
        }

        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
    }

    public void TakeDamage(int damageAmount)
    {
        if (!isDead)
        {
            currentHealth -= damageAmount;
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                animator.SetTrigger("Hurt");
            }
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        Destroy(gameObject, 1f); // Destroy the enemy after 1 second to allow the death animation to finish
    }

    private void FlipSprite(float direction)
    {
        if (direction > 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);
        else if (direction < 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
