using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BotonCargarNivel : MonoBehaviour
{
    public string nombreNivel = "Nivel1";

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(CargarNivel);
    }

    private void CargarNivel()
    {
        FindObjectOfType<GameManager>().LoadLevel(nombreNivel);
    }
}
