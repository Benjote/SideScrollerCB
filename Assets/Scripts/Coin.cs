using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public delegate void SumaMoneda(int moneda);
    public static event SumaMoneda sumaMoneda;

    [SerializeField] private int cantidadMonedas;

    // Referencia al GameObject del CollisionNormal
    public GameObject CollisionNormal;    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisionó es el collider deseado adjunto al CollisionNormal
        if (collision.gameObject == CollisionNormal)
        {
            if (sumaMoneda != null)
            {
                SumarMoneda();
                Destroy(gameObject);
            }
        }
    }

    private void SumarMoneda()
    {
        sumaMoneda?.Invoke(cantidadMonedas);
    }
}
