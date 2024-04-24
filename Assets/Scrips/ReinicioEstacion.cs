using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinicioEstacion : MonoBehaviour
{
    public int totalEstaciones { get; private set; }
    [SerializeField]
    private GameObject letreroGanar;
    [SerializeField]
    private GameObject letreroPerderDeuda;
    [SerializeField]
    private GameObject letreroPerderTiempo;
    [SerializeField]
    private GameObject letreroNuevaEstacion;
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
        GameObject.Find("CardManager").GetComponent<CardManager>().EnableCardThrow();
        GameObject.Find("CardManager").GetComponent<CardManager>().ResetNumCards();
        GameObject.Find("ItemManager").GetComponent<ItemManager>().ResetWater();
        GameObject.Find("Cofre").GetComponent<Cofre>().EnableChest();
        GameObject.Find("Cofre").GetComponent<Cofre>().ClosedChestSprites();
        GameObject.Find("BarManager").GetComponent<BarManager>().RandomHum();
        GameObject.Find("BarManager").GetComponent<BarManager>().NextSeason();
        GameObject.Find("BarManager").GetComponent<BarManager>().CountParcels();
        GameObject.Find("HumidityManager").GetComponent<HumidityManager>().ResetRuined();
        GameObject.Find("HumidityManager").GetComponent<HumidityManager>().CheckActiveAndShow();
        GameObject.Find("CardManager").GetComponent<CardManager>().ResetAumentoRendimiento();
        totalEstaciones++;
    }
    public bool RevisarCondiciones() {
        int numContratos = GameObject.Find("CardManager").GetComponent<CardManager>().numContratos;
        double dineroActual = GameObject.Find("CardManager").GetComponent<CardManager>().dinero;
        int unlockedParcels = GameObject.Find("ItemManager").GetComponent<ItemManager>().unlockedParcels;
        if (dineroActual < -150.00) {
            letreroPerderDeuda.SetActive(true);
            GameObject.Find("EstadoManager").GetComponent<EstadoManager>().PerdidoSinTrivia();
            return true;
        } else if (totalEstaciones >= 24) {
            letreroPerderTiempo.SetActive(true);
            GameObject.Find("EstadoManager").GetComponent<EstadoManager>().PerdidoSinTrivia();
            return true;
        } else if (numContratos >= 3 && unlockedParcels == 5) {
            letreroGanar.SetActive(true);
            GameObject.Find("EstadoManager").GetComponent<EstadoManager>().GanadoSinTrivia();
            return true;
        }
        return false;
    }
}
