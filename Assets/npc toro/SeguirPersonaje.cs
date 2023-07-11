using UnityEngine;

public class SeguirPersonaje : MonoBehaviour
{
    public Transform personaje; // Referencia al Transform del personaje que se va a seguir
    public float suavidad = 0.5f; // Valor de suavidad para suavizar el movimiento de la c�mara

    private Vector3 desplazamiento;

    private void Start()
    {
        desplazamiento = transform.position - personaje.position; // Calcular el desplazamiento inicial entre la c�mara y el personaje
    }

    private void LateUpdate()
    {
        Vector3 posicionDestino = personaje.position + desplazamiento; // Calcular la posici�n a la que la c�mara debe moverse

        transform.position = Vector3.Lerp(transform.position, posicionDestino, suavidad); // Suavizar el movimiento de la c�mara
    }
}
