using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FondoMovimiento : MonoBehaviour
{
    [SerializeField] private Vector2 velocidadMovimiento; // Declare velocity and offset as private serialized fields
    [SerializeField] private Vector2 offset;

    private Material material;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>().material; // Use 'GetComponent' to get the material of the sprite renderer component attached to the game object
    }
    private void Update()
    {
        offset = velocidadMovimiento * Time.deltaTime; // Calculate the offset based on the velocity and the time that has passed since the last frame
        material.mainTextureOffset += offset; // Update the main texture offset of the material with the calculated offset
    }
}
