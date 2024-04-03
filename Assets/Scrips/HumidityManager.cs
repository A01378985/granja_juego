using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumidityManager : MonoBehaviour
{
    // Crear un arreglo de objetos de tipo Parcela
    public List<Parcela> parcelas = new List<Parcela>();
    // Variable int para parcelas arruinadas
    public int ruinedParcels;
    // Variable GameObject para letrero arruinadas con seguro
    public GameObject letreroArruinadasConSeguro;
    // Variable GameObject para letrero arruinadas sin seguro
    public GameObject letreroArruinadasSinSeguro;
    // Variable GameObject para letrero ninguna arruinada
    public GameObject letreroNingunaArruinada;
    public string dificultad;
    public double dineroSeguro;
    public TMPro.TextMeshProUGUI textoDineroSeguro;
    public TMPro.TextMeshProUGUI textoArruinadas1;
    public TMPro.TextMeshProUGUI textoArruinadas2;
    // Starter
    void Start()
    {
        ruinedParcels = CountRuinedParcels();
        dificultad = Dificultad.dificultad;
        dineroSeguro = 0;
    }
    public void CheckIrrigation()
    {
        bool sequia = GameObject.Find("BarManager").GetComponent<BarManager>().sequia;
        bool lluvia = GameObject.Find("BarManager").GetComponent<BarManager>().lluvia;
        if (sequia) {
            foreach (Parcela parcela in parcelas)
            {
                if (parcela.water < 2 && parcela.unlocked)
                {
                    // Bloquear la parcela
                    parcela.unlocked = false;
                    parcela.ruined = true;
                    parcela.productivity = 0;
                    parcela.water = 0;
                    parcela.fertilizer = 0;
                    parcela.tool = 0;
                    parcela.worker = 0;
                    parcela.EnablePlants();
                    GameObject.Find("ItemManager").GetComponent<ItemManager>().unlockedParcels--;
                    GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
                }
            }
        } else if (lluvia) {
            foreach (Parcela parcela in parcelas)
            {
                if (parcela.water > 2 && parcela.unlocked)
                {
                    // Bloquear la parcela
                    parcela.unlocked = false;
                    parcela.ruined = true;
                    parcela.productivity = 0;
                    parcela.water = 0;
                    parcela.fertilizer = 0;
                    parcela.tool = 0;
                    parcela.worker = 0;
                    parcela.EnablePlants();
                    GameObject.Find("ItemManager").GetComponent<ItemManager>().unlockedParcels--;
                    GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
                }
                {
                    // Bloquear la parcela
                    parcela.unlocked = false;
                    parcela.ruined = true;
                    parcela.productivity = 0;
                    parcela.water = 0;
                    parcela.fertilizer = 0;
                    parcela.tool = 0;
                    parcela.worker = 0;
                    parcela.EnablePlants();
                    GameObject.Find("ItemManager").GetComponent<ItemManager>().unlockedParcels--;
                    GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
                }
            }
        } else if (!sequia && !lluvia) {
            foreach (Parcela parcela in parcelas)
            {
                if ((parcela.water < 1 || parcela.water > 7) && parcela.unlocked)
                {
                    // Bloquear la parcela
                    parcela.unlocked = false;
                    parcela.ruined = true;
                    parcela.productivity = 0;
                    parcela.water = 0;
                    parcela.fertilizer = 0;
                    parcela.tool = 0;
                    parcela.worker = 0;
                    parcela.EnablePlants();
                    GameObject.Find("ItemManager").GetComponent<ItemManager>().unlockedParcels--;
                    GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
                }
            }
        }
    }
    // Método para contar parcelas arruinadas regresa el número de parcelas arruinadas
    public int CountRuinedParcels()
    {
        // Crear una variable para contar parcelas arruinadas
        int ruinedParcels = 0;
        // Crear un ciclo para recorrer el arreglo de parcelas
        foreach (Parcela parcela in parcelas)
        {
            // Verificar si la parcela está arruinada
            if (parcela.ruined)
            {
                // Aumentar el contador de parcelas arruinadas
                ruinedParcels++;
            }
        }
        // Regresar el número de parcelas arruinadas
        return ruinedParcels;
    }
    // Método que revisa si hay parcelas arruinadas
    public void RuinMessage()
    {
        ruinedParcels = CountRuinedParcels();
        // Verificar si hay parcelas arruinadas
        if (ruinedParcels > 0 && (dificultad == "hard" || dificultad == "medium"))
        {
            textoArruinadas2.text = ruinedParcels.ToString();
            // Activar letrero de parcelas arruinadas con seguro
            letreroArruinadasSinSeguro.SetActive(true);
        }
        else if (ruinedParcels > 0 && dificultad == "easy")
        {
            dineroSeguro = 50000 * ruinedParcels;
            textoDineroSeguro.text = dineroSeguro.ToString();
            textoArruinadas1.text = ruinedParcels.ToString();
            // Activar letrero de parcelas arruinadas sin seguro
            letreroArruinadasConSeguro.SetActive(true);
        }
        else if (ruinedParcels == 0)
        {
            // Activar letrero de ninguna parcela arruinada
            letreroNingunaArruinada.SetActive(true);
        }
    }
}
