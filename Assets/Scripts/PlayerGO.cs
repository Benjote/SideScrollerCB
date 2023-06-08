using UnityEngine;

public class PlayerGO : MonoBehaviour
{
    public float deathLimit = -50f; // Límite del eje Y para la muerte
    public GameObject CanvasGO; // Referencia al objeto CanvasGO

    private bool isDead = false; // Bandera para evitar llamadas repetidas a la función Die

    private void Update()
    {
        if (!isDead && transform.position.y < deathLimit)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        CanvasGO.SetActive(true);
        // Aquí puedes agregar cualquier lógica adicional que desees ejecutar cuando el jugador muere
    }
}