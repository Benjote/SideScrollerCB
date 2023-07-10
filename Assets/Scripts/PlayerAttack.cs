using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject enemyObject;
    private EnemyDeath enemyDeath;

    private void Start()
    {
        // Obtén la referencia al script EnemyDeath del enemigo
        enemyDeath = enemyObject.GetComponent<EnemyDeath>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto colisionado es el enemigo
        if (other.CompareTag("Enemy"))
        {
            // Llama al método TakeDamage() del script EnemyDeath
            enemyDeath.TakeDamage();
        }
    }
}
