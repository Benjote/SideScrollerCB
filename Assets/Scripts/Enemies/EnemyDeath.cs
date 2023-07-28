using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public Animator animator;
    public int vidaMaxima = 5;
    private int vidaActual;
    public float deathCooldown = 5f;

    private bool isDead = false;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead || !other.CompareTag("PlayerAttack")) return; // Si el enemigo est� muerto o la colisi�n no es con el PlayerAttack, no hacemos nada

        // Causar da�o al enemigo al colisionar con el PlayerAttack
        CausarHerida(1);
    }

    public void CausarHerida(int cantidadDanio)
    {
        if (vidaActual > 0)
        {
            vidaActual -= cantidadDanio;
            animator.SetTrigger("Hurt");

            if (vidaActual <= 0)
            {
                Morir();
            }
        }
    }

    public void Morir()
    {
        isDead = true;
        animator.SetTrigger("IsDeath");
        // Destruir el enemigo despu�s de un tiempo (opcional)
        Destroy(gameObject, deathCooldown);
    }
}