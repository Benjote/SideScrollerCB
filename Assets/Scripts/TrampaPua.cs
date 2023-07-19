using UnityEngine;

public class TrampaPua : MonoBehaviour
{
    public int damageAmount = 1; // La cantidad de daño que hará la trampa

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.CausarDaño(); // Corregir el nombre del método aquí
            }
        }
    }
}