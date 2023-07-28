using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rabbit : MonoBehaviour
{
    private Animator animator;

    public float distanciaDeteccionAtaque = 1.5f;
    public int cantidadDaño = 1; // Cambiado de int a float

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
        // Verificar si el jugador está dentro del rango de detección para atacar
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);
        if (distanciaAlJugador <= distanciaDeteccionAtaque)
        {
            // Atacar al jugador si está dentro del rango de detección
            animator.SetTrigger("Attack");
            jugador.GetComponent<PlayerController>().CausarHerida(cantidadDaño);
        }
        else
        {
            // Si el jugador no está en rango de ataque, el conejo debería caminar o estar en reposo (Idle)
            // Puedes implementar la lógica para caminar o reposo aquí según tus necesidades.
        }
    }
    // Método para activar la animación de caminar
    public void PlayWalkAnimation()
    {
        animator.SetBool("IsWalking", true);
    }

    // Método para desactivar la animación de caminar
    public void StopWalkAnimation()
    {
        animator.SetBool("IsWalking", false);
    }

    // Método para activar la animación de ataque
    public void PlayAttackAnimation()
    {
        animator.SetTrigger("Attack");
    }

    // Método para activar la animación de reposo (Idle)
    public void PlayIdleAnimation()
    {
        // En este caso, no necesitamos un método adicional ya que la animación de caminar se establece automáticamente a false en Update().
        // Si deseas añadir alguna lógica adicional para la animación de reposo, puedes hacerlo aquí.
    }

    // Método para activar la animación de herir (Hurt)
    public void PlayHurtAnimation()
    {
        // En este caso, no necesitamos un método adicional ya que la animación de herir se activa automáticamente cuando el Animator recibe un evento de herida.
        // Si tienes eventos de herida configurados en el Animator, la animación se reproducirá automáticamente cuando reciba un evento de herida.
    }

    // Método para activar la animación de muerte (Death)
    public void PlayDeathAnimation()
    {
        animator.SetTrigger("IsDeath");
    }
}