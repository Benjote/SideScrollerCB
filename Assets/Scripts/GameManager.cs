using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Instancia única del GameManager

    public int maxHearts = 3; // Número máximo de corazones
    public int currentHearts; // Corazones actuales

    public string gameOverSceneName = "GameOver"; // Nombre de la escena de Game Over

    private bool isPaused = false;
    public GameObject pauseMenu;

    private void Awake()
    {
        // Verificar si ya existe una instancia del GameManager y destruir este objeto si es el caso
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentHearts = maxHearts; // Inicializar los corazones al máximo
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    public void QuitGame()
    {
        Debug.Log("Saliendo del juego");
        Application.Quit();
    }

    public void PauseGame()
    {
        Debug.Log("Pausando el juego");
        Time.timeScale = 0;
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Debug.Log("Reanudando el juego");
        Time.timeScale = 1;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void TakeDamage()
    {
        currentHearts--; // Reducir un corazón

        if (currentHearts <= 0)
        {
            // Si no quedan corazones, cargar la escena de Game Over
            SceneManager.LoadScene(gameOverSceneName);
        }
    }

    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}