using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // Transform del personaje a seguir
    public float smoothSpeed = 0.125f; // Suavidad del movimiento de la cámara

    private void LateUpdate()
    {
        // Movimiento de la cámara
        Vector3 desiredPosition = target.position + new Vector3(0, 0, -10);
        desiredPosition.y = transform.position.y; // Mantener la altura actual de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}

