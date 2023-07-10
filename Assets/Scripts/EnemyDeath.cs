using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public Animator anim;
    public Collider2D enemyCollider;

    private void Start()
    {
        anim = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
    }

    public void TakeDamage()
    {
        // Reproduce la animación de golpe
        anim.SetTrigger("Hit");
    }

    public void Die()
    {
        // Desactiva el collider del enemigo
        enemyCollider.enabled = false;

        // Reproduce la animación de muerte
        anim.SetTrigger("Death");

        // Desactiva otros componentes que ya no se necesitan
        // Por ejemplo, un controlador de movimiento, inteligencia artificial, etc.

        // Inicia una coroutine para destruir el objeto después de un tiempo
        StartCoroutine(DestroyAfterDelay(3f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Espera el tiempo especificado
        yield return new WaitForSeconds(delay);

        // Destruye el objeto enemigo
        Destroy(gameObject);
    }
}
