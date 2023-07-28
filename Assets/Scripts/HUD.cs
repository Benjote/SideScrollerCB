using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class HUD : MonoBehaviour
{
    public GameManager gameManager;
    public TextMeshProUGUI puntos;
    [SerializeField] private List<GameObject> listaCorazones;
    [SerializeField] private Sprite corazonDesactivado;
    [SerializeField] private TMP_Text textoMonedas;

    private int totalMonedas;

    private const string MonedasPlayerPrefsKey = "TotalMonedas";

    void Start()
    {
        Coin.sumaMoneda += SumarMonedas;
        // Cargar el valor guardado de las monedas al iniciar
        totalMonedas = PlayerPrefs.GetInt(MonedasPlayerPrefsKey, 0);
        ActualizarTextoMonedas();
    }

    void OnDestroy()
    {
        // Guardar el valor actual de las monedas al salir
        PlayerPrefs.SetInt(MonedasPlayerPrefsKey, totalMonedas);
        PlayerPrefs.Save();
    }

    void Update()
    {

    }

    private void SumarMonedas(int moneda)
    {
        totalMonedas += moneda;
        ActualizarTextoMonedas();
    }

    private void ActualizarTextoMonedas()
    {
        textoMonedas.text = totalMonedas.ToString();
    }

    public void RestaCorazones(float indice)
    {
        Image imagenCorazon = listaCorazones[Convert.ToInt32(Math.Round(indice))].GetComponent<Image>();
        imagenCorazon.sprite = corazonDesactivado;
    }
}