/// <summary>
/// Muestra el nivel correspondiente en las barras de producción y estaciones.
/// </summary>
/// <author>Arturo Barrios Mendoza y Fidel Alexander Bonilla Montalvo</author>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    private DataReader dataReader;
    // Crear un array de GameObjects para la barra de productividad
    [SerializeField] private GameObject[] prodBars;
    // Crear un array de GameObjects para la barra de estaciones
    [SerializeField] private GameObject[] seasonBars;
    // Crear un array de Parcelas
    [SerializeField] private List<Parcela> parcelas = new List<Parcela>();
    // Variable para humedad
    public int hum { get; private set; }
    // Variable para número de parcelas
    public int numParcelas { get; private set; }
    // Variable para estación
    public int season { get; private set; }
    // Variable para productividad actual
    public double currentProd { get; private set; }
    // Variable bool para sequía
    public bool sequia { get; private set; }
    // Variable bool para lluvia
    public bool lluvia { get; private set; }
    void Start()
    {
        dataReader = FindObjectOfType<DataReader>();
        sequia = false;
        lluvia = false;
        RandomHum();
        // Inicializar la estación en 1
        season = 1;
        // Inicializar la productividad en 0
        currentProd = 0;
        // Inicializar el número de parcelas en 0
        CountParcels();
    }
    public void SetHumBar()
    {
        sequia = false;
        lluvia = false;
        if (hum <= 1) {
            sequia = true;
        } else if (hum >= 8) {
            lluvia = true;
        } 
    }
    // Método para inicializar la humedad aleatoriamente
    public void RandomHum() {
        int rand = Random.Range(0, 100);
        if (rand < 10) {
            // 10% de posibilidad de que hum sea 0 o 1
            hum = Random.Range(0, 2);
        } else if (rand < 20) {
            // 10% de posibilidad de que hum sea 8 o 9
            hum = Random.Range(8, 10);
        } else {
            // 80% de posibilidad de que hum sea entre 2 y 7
            hum = Random.Range(2, 8);
        }
        SetHumBar();
    }
    // Método para contar la productividad total de las parcelas
    public void CountProd() {
        // Inicializar la productividad en 0
        currentProd = 0;
        // Recorrer el array de parcelas
        foreach (Parcela parcela in parcelas) {
            // Sumar la productividad de cada parcela a la productividad total
            currentProd += parcela.productivity;
        }
        double percentage = currentProd / (numParcelas * 100);
        Debug.Log("Porcentaje actual: " + percentage);
        SetProdBar(percentage);
    }
    // Método para habilitar el objeto correcto de la barra de productividad con base en ProdPercentage
    public void SetProdBar(double p) {
        double percentage = p;
        // Deshabilitar todos los objetos de la barra de productividad
        foreach (GameObject prodBar in prodBars) {
            prodBar.SetActive(false);
        }
        // Recorrer el array de barras de productividad
        if (percentage <= 0.1 && percentage > 0) {
            prodBars[0].SetActive(true);
        } else if (percentage <= 0.2) {
            prodBars[1].SetActive(true);
        } else if (percentage <= 0.3) {
            prodBars[2].SetActive(true);
        } else if (percentage <= 0.4) {
            prodBars[3].SetActive(true);
        } else if (percentage <= 0.5) {
            prodBars[4].SetActive(true);
        } else if (percentage <= 0.6) {
            prodBars[5].SetActive(true);
        } else if (percentage <= 0.7) {
            prodBars[6].SetActive(true);
        } else if (percentage <= 0.8) {
            prodBars[7].SetActive(true);
        } else if (percentage <= 0.9) {
            prodBars[8].SetActive(true);
        } else if (percentage <= 1) {
            prodBars[9].SetActive(true);
        }
    }
    // Método para contar parcelas unlocked
    public void CountParcels() {
        // Inicializar el número de parcelas en 0
        numParcelas = 0;
        // Recorrer el array de parcelas
        foreach (Parcela parcela in parcelas) {
            // Verificar si la parcela está desbloqueada
            if (parcela.unlocked) {
                // Sumar 1 al número de parcelas
                numParcelas++;
            }
        }
    }
    public void NextSeason() {
        season++;
        if (season > 4) {
            season = 1;
        }
        SetSeasonBar();
        //dataReader.ActualizarEstaciones(season.ToString());
    }
    public void SetSeasonBar() {
        if (season == 1) {
            seasonBars[0].SetActive(true);
            seasonBars[1].SetActive(false);
            seasonBars[2].SetActive(false);
            seasonBars[3].SetActive(false);
        } else if (season == 2) {
            seasonBars[1].SetActive(true);
            seasonBars[0].SetActive(false);
            seasonBars[2].SetActive(false);
            seasonBars[3].SetActive(false);
        } else if (season == 3) {
            seasonBars[2].SetActive(true);
            seasonBars[0].SetActive(false);
            seasonBars[1].SetActive(false);
            seasonBars[3].SetActive(false);
        } else if (season == 4) {
            seasonBars[3].SetActive(true);
            seasonBars[0].SetActive(false);
            seasonBars[1].SetActive(false);
            seasonBars[2].SetActive(false);
        }
    }
    public void IncNumParcelas() {
        numParcelas++;
    }
}
