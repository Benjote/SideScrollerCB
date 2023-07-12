using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /*public int PuntosTotales { get  { return puntosTotales;  } }
    private int puntosTotales;
    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales = puntosTotales + puntosASumar;
        Debug.Log(puntosTotales);
    } */
    public static GameManager instance; // Instancia única del GameManager

    public int maxHearts = 3; // Número máximo de corazones
    public int currentHearts; // Corazones actuales
   

    public string gameOverSceneName = "GameOver"; // Nombre de la escena de Game Over

    private bool isPaused = false;
    public GameObject pauseMenu;
    public GameObject pausePanel;

    public bool canPause = true; // Indica si el juego se puede pausar o no

    private void Start()
    {
        //currentHearts = maxHearts; // Inicializar los corazones al máximo

        if (!canPause)
        {
            pauseMenu.SetActive(false); // Desactivar el canvas de pausa al inicio
            pausePanel.SetActive(false); // Desactivar el panel de pausa al inicio
        }
    }

    private void Update()
    {
        if (canPause && Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void LoadLevel(string levelName)
    {
        Debug.Log("Cargando nivel: " + levelName);
        SceneManager.LoadScene(levelName);
    }

    public void RestartLevel()
    {
        // Desactivar la pausa y ocultar el panel de pausa
        isPaused = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1;

        // Reiniciar el nivel cargando la escena nuevamente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    public void PauseGame()
    {
        if (canPause)
        {
            Debug.Log("Pausando el juego");
            Time.timeScale = 0;
            isPaused = true;
            pausePanel.SetActive(true); // Activar el panel de pausa
        }
    }

    public void ResumeGame()
    {
        Debug.Log("Reanudando el juego");
        Time.timeScale = 1;
        isPaused = false;
        pausePanel.SetActive(false); // Desactivar el panel de pausa
    }

    public void TakeDamage()
    {
        currentHearts--; // Reducir un corazón

        if (currentHearts <= 0)
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}