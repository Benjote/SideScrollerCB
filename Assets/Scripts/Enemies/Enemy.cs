using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidadMovimiento = 2.0f;
    public Transform puntoA;
    public Transform puntoB;
    public float distanciaDeteccion = 5.0f;
    public Transform rangoVision;

    private Transform jugador;
    private bool persiguiendo = false;
    private bool mirandoHaciaDerecha = true;
    private Transform destinoActual;

    private float tolerancia = 0.1f;

    private EnemyAnimationController animationController; // Referencia al controlador de animaciones

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        destinoActual = puntoB;

        // Obtener la referencia al componente EnemyAnimationController
        animationController = GetComponent<EnemyAnimationController>();
    }

    private void Update()
    {
        // Verificar si el jugador está dentro del rango de detección
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);
        persiguiendo = distanciaAlJugador <= distanciaDeteccion;

        if (persiguiendo)
        {
            // Perseguir al jugador si está dentro del rango de detección
            Vector2 direccion = (jugador.position - transform.position).normalized;
            transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
            MirarHacia(direccion.x);

            // Animación de ataque si está persiguiendo al jugador
            animationController.PlayAttackAnimation();
        }
        else
        {
            // Si no persigue al jugador, moverse entre punto A y punto B
            float destinoX = destinoActual.position.x;
            float paso = velocidadMovimiento * Time.deltaTime;
            transform.position = new Vector2(Mathf.MoveTowards(transform.position.x, destinoX, paso), transform.position.y);

            // Cambiar el destino al llegar a los puntos
            if (Mathf.Abs(transform.position.x - puntoA.position.x) < tolerancia)
            {
                destinoActual = puntoB;
                MirarHacia(1f); // Girar hacia la derecha

                // Animación de caminar si ha llegado al punto A
                animationController.PlayWalkAnimation();
            }
            else if (Mathf.Abs(transform.position.x - puntoB.position.x) < tolerancia)
            {
                destinoActual = puntoA;
                MirarHacia(-1f); // Girar hacia la izquierda

                // Animación de caminar si ha llegado al punto B
                animationController.PlayWalkAnimation();
            }
            else
            {
                // Animación de reposo si está entre los puntos A y B
                animationController.PlayIdleAnimation();
            }
        }
    }

    private void MirarHacia(float dirX)
    {
        if (dirX > 0 && !mirandoHaciaDerecha || dirX < 0 && mirandoHaciaDerecha)
        {
            mirandoHaciaDerecha = !mirandoHaciaDerecha;
            Vector3 escala = transform.localScale;
            escala.x *= -1;
            transform.localScale = escala;
        }
    }
}