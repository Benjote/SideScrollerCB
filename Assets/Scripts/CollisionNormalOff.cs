using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionNormalOff : MonoBehaviour
{
    [SerializeField] private GameObject collisionNormal;

    private void Start()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            // Suscribirse al evento de muerte del jugador
            playerController.OnPlayerDeath += DisableCollisionNormal;
        }
        else
        {
            Debug.LogWarning("PlayerController component not found on the same GameObject as CollisionNormalOff.");
        }
    }

    private void DisableCollisionNormal()
    {
        // Desactivar el CapsuleCollider2D del objeto CollisionNormal cuando el jugador muere
        collisionNormal.GetComponent<CapsuleCollider2D>().enabled = false;
    }
}