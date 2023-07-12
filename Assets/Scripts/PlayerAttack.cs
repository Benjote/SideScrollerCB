using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private EnemyDeath enemyDeath;
    private BoxCollider2D colAttack;

    private void Awake()
    {
        colAttack = GetComponent<BoxCollider2D>();      
    }
    private void Start()
    {  

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            enemyDeath.TakeDamage();
        }
    }
}
