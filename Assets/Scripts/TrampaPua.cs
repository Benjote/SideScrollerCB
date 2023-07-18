using UnityEngine;

public class TrampaPua : MonoBehaviour
{
    public int damageAmount = 1; // La cantidad de daño que hará la trampa

    private PlayerController playerController; // Referencia al componente PlayerController

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Buscar el componente PlayerController en escena
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController.CausarHerida(damageAmount); // Pasar el valor de damageAmount al método CausarHerida en PlayerController
        }
    }
}