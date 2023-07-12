using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public delegate void SumaMoneda(int moneda);
    public static event SumaMoneda sumaMoneda;

    [SerializeField] private int cantidadMonedas;

    // Referencia al GameObject del jugador
    public GameObject jugador;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisionó es el collider deseado adjunto al jugador
        if(collision.gameObject == jugador)
        {
            if (sumaMoneda != null)
            {
                SumarMoneda();
                Destroy(this.gameObject);
            }  
        }
    }

    private void SumarMoneda()
    {
        sumaMoneda(cantidadMonedas);
    }
}