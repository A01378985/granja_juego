using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinicioEstacion : MonoBehaviour
{
    public int totalEstaciones;
    public GameObject letreroGanar;
    public GameObject letreroPerderDeuda;
    public GameObject letreroPerderTiempo;
    public GameObject letreroNuevaEstacion;
    void Start()
    {
        totalEstaciones = 1;
        letreroGanar.SetActive(false);
        letreroPerderDeuda.SetActive(false);
        letreroPerderTiempo.SetActive(false);
        letreroNuevaEstacion.SetActive(false);
    }
    // Método para reiniciar todo
    public void NuevaEstacion() {
        if (RevisarCondiciones()) {
            return;
        }
        letreroNuevaEstacion.SetActive(true);
        GameObject.Find("CardManager").GetComponent<CardManager>().enabledThrow = true;
        GameObject.Find("CardManager").GetComponent<CardManager>().numCards = 0;
        GameObject.Find("ItemManager").GetComponent<ItemManager>().ResetWater();
        GameObject.Find("Cofre").GetComponent<Cofre>().chestEnabled = true;
        GameObject.Find("Cofre").GetComponent<Cofre>().closedChest.SetActive(true);
        GameObject.Find("Cofre").GetComponent<Cofre>().openChest.SetActive(false);
        GameObject.Find("BarManager").GetComponent<BarManager>().RandomHum();
        GameObject.Find("BarManager").GetComponent<BarManager>().NextSeason();
        GameObject.Find("BarManager").GetComponent<BarManager>().CountParcels();
        GameObject.Find("HumidityManager").GetComponent<HumidityManager>().ResetRuined();
        GameObject.Find("HumidityManager").GetComponent<HumidityManager>().CheckActiveAndShow();
        GameObject.Find("CardManager").GetComponent<CardManager>().aumentoRendimiento = 0;
        totalEstaciones++;
    }
    public bool RevisarCondiciones() {
        int numContratos = GameObject.Find("CardManager").GetComponent<CardManager>().numContratos;
        double dineroActual = GameObject.Find("CardManager").GetComponent<CardManager>().dinero;
        int unlockedParcels = GameObject.Find("ItemManager").GetComponent<ItemManager>().unlockedParcels;
        if (dineroActual < -1500000.00) {
            letreroPerderDeuda.SetActive(true);
            return true;
        } else if (totalEstaciones >= 24) {
            letreroPerderTiempo.SetActive(true);
            return true;
        } else if (numContratos >= 3 && unlockedParcels == 5) {
            letreroGanar.SetActive(true);
            return true;
        }
        return false;
    }
}
