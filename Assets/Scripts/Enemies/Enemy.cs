using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float velocidadMovimiento = 2.0f;
    public Transform puntoA;
    public Transform puntoB;
    public BoxCollider2D rangoVision;

    public BoxCollider2D deteccionCollider; // Collider para la detección del jugador

    private Transform jugador;
    private bool persiguiendo = false;
    private bool mirandoHaciaDerecha = true;
    private Transform destinoActual;

    private float tolerancia = 0.1f;

    private Animator animator; // Referencia al componente Animator del enemigo

    public int damage = 1; // Cantidad de daño que el enemigo inflige al jugador
    public float attackCooldown = 2f; // Tiempo entre ataques del enemigo
    public float attackDuration = 1f; // Duración de la animación de ataque

    private float attackTimer = 0f; // Temporizador para el ataque

    private bool isPlayerInvulnerable = false; // Variable para rastrear si el jugador está en estado de invulnerabilidad
    public float invulnerabilityDuration = 1.5f; // Duración de la invulnerabilidad del jugador después de recibir un ataque
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
        // Verificar si el jugador está dentro del rango de detección
        bool jugadorEnRango = deteccionCollider.bounds.Contains(jugador.position);
        persiguiendo = jugadorEnRango && rangoVision.bounds.Contains(jugador.position); // Usar rangoVision.bounds en lugar de rangoVision.GetComponent<Collider>().bounds

        if (persiguiendo)
        {
            // Perseguir al jugador si está dentro del rango de detección
            Vector2 direccion = (jugador.position - transform.position).normalized;
            transform.Translate(direccion * velocidadMovimiento * Time.deltaTime);
            MirarHacia(direccion.x);

            // Establecer el parámetro "Speed" en el Animator para activar la animación "Walk"
            animator.SetFloat("Speed", direccion.x);

            // Atacar al jugador si ha pasado suficiente tiempo desde el último ataque
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

                // Animación de caminar si ha llegado al punto A
                animator.SetFloat("Speed", 1f);
            }
            else if (Mathf.Abs(transform.position.x - puntoB.position.x) < tolerancia)
            {
                destinoActual = puntoA;
                MirarHacia(-1f); // Girar hacia la izquierda

                // Animación de caminar si ha llegado al punto B
                animator.SetFloat("Speed", 1f);
            }
            else
            {
                // Animación de reposo si está entre los puntos A y B
                animator.SetFloat("Speed", 0f);
            }
        }

        // Verificar si el jugador está en estado de invulnerabilidad
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

    // Método para realizar el ataque al jugador
    private void Atacar()
    {
        // Aquí puedes implementar la lógica del ataque del enemigo al jugador
        // Asegúrate de ajustar los valores de daño y animación en función del diseño del juego

        // Si el jugador no está en estado de invulnerabilidad, causar daño al jugador
        if (!PlayerController.isInvulnerable)
        {
            // Hacer que el enemigo le quite 1 corazón al jugador
            PlayerController.CausarHerida(damage);

            // Marcar al jugador como invulnerable temporalmente
            PlayerController.isInvulnerable = true;
            PlayerController.invulnerabilityTimer = PlayerController.invulnerabilityDuration;

            // Ejecutar animación de ataque en el enemigo (si tienes una animación de ataque)
            animator.SetTrigger("Attack");
        }
    }
}


// Método para recibir daño del jugador
//public void RecibirDaño(int cantidadDaño)
//{
//    if (vida > 0)
//    {
//        vida -= cantidadDaño;
//        enemyDeath.CausarHerida(); // Llama al método CausarHerida de EnemyDeath

//        if (vida <= 0)
//        {
//            enemyDeath.Morir(); // Llama al método Morir de EnemyDeath
//        }
//    }
//}
