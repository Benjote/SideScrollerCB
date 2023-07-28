using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    private Animator animator;

    public float distanciaDeteccionAtaque = 1.5f;
    public int cantidadDa�o = 1; // Cambiado de int a float

    private Transform jugador;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Verificar si el jugador est� dentro del rango de detecci�n para atacar
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);
        if (distanciaAlJugador <= distanciaDeteccionAtaque)
        {
            // Atacar al jugador si est� dentro del rango de detecci�n
            animator.SetTrigger("Attack");
            jugador.GetComponent<PlayerController>().CausarHerida(cantidadDa�o);
        }
        else
        {
            // Si el jugador no est� en rango de ataque, el conejo deber�a caminar o estar en reposo (Idle)
            // Puedes implementar la l�gica para caminar o reposo aqu� seg�n tus necesidades.
        }
    }
    // M�todo para activar la animaci�n de caminar
    public void PlayWalkAnimation()
    {
        animator.SetBool("IsWalking", true);
    }

    // M�todo para desactivar la animaci�n de caminar
    public void StopWalkAnimation()
    {
        animator.SetBool("IsWalking", false);
    }

    // M�todo para activar la animaci�n de ataque
    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    // M�todo para activar la animaci�n de reposo (Idle)
    public void PlayIdleAnimation()
    {
        // En este caso, no necesitamos un m�todo adicional ya que la animaci�n de caminar se establece autom�ticamente a false en Update().
        // Si deseas a�adir alguna l�gica adicional para la animaci�n de reposo, puedes hacerlo aqu�.
    }

    // M�todo para activar la animaci�n de herir (Hurt)
    public void PlayHurtAnimation()
    {
        // En este caso, no necesitamos un m�todo adicional ya que la animaci�n de herir se activa autom�ticamente cuando el Animator recibe un evento de herida.
        // Si tienes eventos de herida configurados en el Animator, la animaci�n se reproducir� autom�ticamente cuando reciba un evento de herida.
    }

    // M�todo para activar la animaci�n de muerte (Death)
    public void PlayDeathAnimation()
    {
        animator.SetTrigger("IsDeath");
    }
}