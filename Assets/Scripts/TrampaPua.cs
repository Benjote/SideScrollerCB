using UnityEngine;

public class TrampaPua : MonoBehaviour
{
    public int damageAmount = 1; // La cantidad de daño que hará la trampa

    public GameObject jugador; // Referencia al GameObject del jugador

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == jugador)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.CausarDaño(damageAmount);
            }
        }
    }
}