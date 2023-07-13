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
            // Aquí se puede llamar al diálogo antes de cambiar de escena
            // ...
            SceneManager.LoadScene(2);
        }
    }
}