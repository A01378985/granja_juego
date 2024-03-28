using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    // Crear un array de GameObjects para la barra de humedad
    public GameObject[] humBars;
    // Crear un array de GameObjects para la barra de productividad
    public GameObject[] prodBars;
    // Crear un array de GameObjects para la barra de estaciones
    public GameObject[] seasonBars;
    // Crear un array de Parcelas
    public List<Parcela> parcelas = new List<Parcela>();
    // Variable para humedad
    public int hum;
    // Variable para número de parcelas
    public int numParcelas;
    // Variable para costo de parcela
    public int costoParcela = 350000;
    // Variable para estación
    public int season;
    // Variable para productividad actual
    public int currentProd;
    void Start()
    {
        RandomHum();
        // Inicializar la estación en 1
        season = 1;
        // Inicializar la productividad en 0
        currentProd = 0;
        // Inicializar el número de parcelas en 0
        numParcelas = GameObject.Find("ItemManager").GetComponent<ItemManager>().CountParcels();
        SetHumBar();
    }
    // Método para habilitar el objeto correcto de la barra de humedad
    public void SetHumBar()
    {
        // Deshabilitar todos los objetos de la barra de humedad
        foreach (GameObject humBar in humBars)
        {
            humBar.SetActive(false);
        }
        // Recorrer el array de barras de humedad
        if (hum == 0) {
            humBars[0].SetActive(true);
        } else if (hum == 1) {
            humBars[1].SetActive(true);
        } else if (hum == 2) {
            humBars[2].SetActive(true);
        } else if (hum == 3) {
            humBars[3].SetActive(true);
        } else if (hum == 4) {
            humBars[4].SetActive(true);
        } else if (hum == 5) {
            humBars[5].SetActive(true);
        } else if (hum == 6) {
            humBars[6].SetActive(true);
        } else if (hum == 7) {
            humBars[7].SetActive(true);
        } else if (hum == 8) {
            humBars[8].SetActive(true);
        } else if (hum == 9) {
            humBars[9].SetActive(true);
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
    }
}
