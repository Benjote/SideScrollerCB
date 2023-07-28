using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidadMovimiento = 2.0f;
    public Transform puntoA;
    public Transform puntoB;
    public BoxCollider2D rangoVision;

    public BoxCollider2D deteccionCollider; // Collider para la detecci�n del jugador

    private Transform jugador;
    private bool persiguiendo = false;
    private bool mirandoHaciaDerecha = true;
    private Transform destinoActual;

    private float tolerancia = 0.1f;

    private Animator animator; // Referencia al componente Animator del enemigo

    public int damage = 1; // Cantidad de da�o que el enemigo inflige al jugador
    public float attackCooldown = 2f; // Tiempo entre ataques del enemigo
    public float attackDuration = 1f; // Duraci�n de la animaci�n de ataque

    private float attackTimer = 0f; // Temporizador para el ataque

    private bool isPlayerInvulnerable = false; // Variable para rastrear si el jugador est� en estado de invulnerabilidad
    public float invulnerabilityDuration = 1.5f; // Duraci�n de la invulnerabilidad del jugador despu�s de recibir un ataque
    private float invulnerabilityTimer = 0f; // Temporizador para el estado de invulnerabilidad
    private PlayerController PlayerController; // Referencia al componente PlayerController del jugador

    private void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        destinoActual = puntoB;

        // Obtener la referencia al componente Animator
        animator = GetComponent<Animator>();

        // Obtener la referencia al componente PlayerController
        PlayerController = jugador.GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Verificar si el jugador est� dentro del rango de detecci�n
        bool jugadorEnRango = deteccionCollider.bounds.Contains(jugador.position);
        persiguiendo = jugadorEnRango && rangoVision.bounds.Contains(jugador.position); // Usar rangoVision.bounds en lugar de rangoVision.GetComponent<Collider>().bounds

        if (persiguiendo)
        {
            // Perseguir al jugador si est� dentro del rango de detecci�n
            Vector2 direccion = (jugador.position - transform.position).normalized;
            transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
            MirarHacia(direccion.x);

            // Establecer el par�metro "Speed" en el Animator para activar la animaci�n "Walk"
            animator.SetFloat("Speed", direccion.x);

            // Atacar al jugador si ha pasado suficiente tiempo desde el �ltimo ataque
            if (attackTimer <= 0f)
            {
                Atacar();
                attackTimer = attackCooldown;
            }
            else
            {
                attackTimer -= Time.deltaTime;
            }
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

                // Animaci�n de caminar si ha llegado al punto A
                animator.SetFloat("Speed", 1f);
            }
            else if (Mathf.Abs(transform.position.x - puntoB.position.x) < tolerancia)
            {
                destinoActual = puntoA;
                MirarHacia(-1f); // Girar hacia la izquierda

                // Animaci�n de caminar si ha llegado al punto B
                animator.SetFloat("Speed", 1f);
            }
            else
            {
                // Animaci�n de reposo si est� entre los puntos A y B
                animator.SetFloat("Speed", 0f);
            }
        }

        // Verificar si el jugador est� en estado de invulnerabilidad
        if (PlayerController.isInvulnerable)
        {
            // Decrementar el temporizador de invulnerabilidad
            PlayerController.invulnerabilityTimer -= Time.deltaTime;

            // Si el temporizador de invulnerabilidad llega a cero, el jugador ya no es invulnerable
            if (PlayerController.invulnerabilityTimer <= 0f)
            {
                PlayerController.isInvulnerable = false;
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

    // M�todo para realizar el ataque al jugador
    private void Atacar()
    {
        // Aqu� puedes implementar la l�gica del ataque del enemigo al jugador
        // Aseg�rate de ajustar los valores de da�o y animaci�n en funci�n del dise�o del juego

        // Si el jugador no est� en estado de invulnerabilidad, causar da�o al jugador
        if (!PlayerController.isInvulnerable)
        {
            // Hacer que el enemigo le quite 1 coraz�n al jugador
            PlayerController.CausarHerida(damage);

            // Marcar al jugador como invulnerable temporalmente
            PlayerController.isInvulnerable = true;
            PlayerController.invulnerabilityTimer = PlayerController.invulnerabilityDuration;

            // Ejecutar animaci�n de ataque en el enemigo (si tienes una animaci�n de ataque)
            animator.SetTrigger("Attack");
        }
    }
}


// M�todo para recibir da�o del jugador
//public void RecibirDa�o(int cantidadDa�o)
//{
//    if (vida > 0)
//    {
//        vida -= cantidadDa�o;
//        enemyDeath.CausarHerida(); // Llama al m�todo CausarHerida de EnemyDeath

//        if (vida <= 0)
//        {
//            enemyDeath.Morir(); // Llama al m�todo Morir de EnemyDeath
//        }
//    }
//}
