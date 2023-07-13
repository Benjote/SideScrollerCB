using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tp0 : MonoBehaviour
{
    [SerializeField] private GameObject jugador; // Referencia al objeto del jugador

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == jugador)
        {
            // Aqu� se puede llamar al di�logo antes de cambiar de escena
            // ...
            SceneManager.LoadScene(2);
        }
    }
}