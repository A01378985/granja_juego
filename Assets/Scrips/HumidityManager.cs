using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumidityManager : MonoBehaviour
{
    [SerializeField]
    private List<Parcela> parcelas = new List<Parcela>();

    public int ruinedParcels { get; private set; }

    [SerializeField]
    private GameObject letreroArruinadasConSeguro;

    [SerializeField]
    private GameObject letreroArruinadasSinSeguro;

    [SerializeField]
    private GameObject letreroNingunaArruinada;

    public string dificultad { get; private set; }
    public double dineroSeguro { get; private set; }

    [SerializeField]
    private TMPro.TextMeshProUGUI textoDineroSeguro;

    [SerializeField]
    private TMPro.TextMeshProUGUI textoArruinadas1;

    [SerializeField]
    private TMPro.TextMeshProUGUI textoArruinadas2;

    [SerializeField]
    private AudioSource sonidoFail;
    // Starter
    void Start()
    {
        ruinedParcels = CountRuinedParcels();
        dificultad = Dificultad.dificultad;
        dineroSeguro = 0;
        sonidoFail = GameObject.Find("SonidoFail").GetComponent<AudioSource>();
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
                    parcela.LockThisParcel();
                    parcela.RuinThisParcel();
                    parcela.ResetParcelValues();
                    parcela.QuitarTodosTrabajadores();
                    parcela.EnablePlants();
                    GameObject.Find("CardManager").GetComponent<CardManager>().DecNumCrops();
                    GameObject.Find("ItemManager").GetComponent<ItemManager>().DecUnlockedParcels();
                    GameObject.Find("BarManager").GetComponent<BarManager>().CountParcels();
                    GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
                }
            }
        } else if (lluvia) {
            foreach (Parcela parcela in parcelas)
            {
                if (parcela.water >= 2 && parcela.unlocked)
                {
                    // Bloquear la parcela
                    parcela.LockThisParcel();
                    parcela.RuinThisParcel();
                    parcela.ResetParcelValues();
                    parcela.QuitarTodosTrabajadores();
                    parcela.EnablePlants();
                    GameObject.Find("CardManager").GetComponent<CardManager>().DecNumCrops();
                    GameObject.Find("ItemManager").GetComponent<ItemManager>().DecUnlockedParcels();
                    GameObject.Find("BarManager").GetComponent<BarManager>().CountParcels();
                    GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
                }
            }
        } else if (!sequia && !lluvia) {
            foreach (Parcela parcela in parcelas)
            {
                if ((parcela.water < 1 || parcela.water >= 7) && parcela.unlocked)
                {
                    // Bloquear la parcela
                    parcela.LockThisParcel();
                    parcela.RuinThisParcel();
                    parcela.ResetParcelValues();
                    parcela.QuitarTodosTrabajadores();
                    parcela.EnablePlants();
                    GameObject.Find("CardManager").GetComponent<CardManager>().DecNumCrops();
                    GameObject.Find("ItemManager").GetComponent<ItemManager>().DecUnlockedParcels();
                    GameObject.Find("BarManager").GetComponent<BarManager>().CountParcels();
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
            sonidoFail.Play();
        }
        else if (ruinedParcels > 0 && dificultad == "easy")
        {
            dineroSeguro = 50000 * ruinedParcels;
            textoDineroSeguro.text = dineroSeguro.ToString();
            textoArruinadas1.text = ruinedParcels.ToString();
            // Activar letrero de parcelas arruinadas sin seguro
            letreroArruinadasConSeguro.SetActive(true);
            sonidoFail.Play();
        }
        else if (ruinedParcels == 0)
        {
            // Activar letrero de ninguna parcela arruinada
            letreroNingunaArruinada.SetActive(true);
        }
    }
    // Método para poner ruin como false en todas las parcelas
    public void ResetRuined()
    {
        // Crear un ciclo para recorrer el arreglo de parcelas
        foreach (Parcela parcela in parcelas)
        {
            // Poner ruin como false
            parcela.NotRuinThisParcel();
        }
    }
    // Método para pagar el dinero del seguro
    public void PayInsurance()
    {
        GameObject.Find("CardManager").GetComponent<CardManager>().UpdateMoney(dineroSeguro);
    }
    // Método para ver si hay parcelas activas y mostrar sus barras
    public void CheckActiveAndShow()
    {
        // Crear un ciclo para recorrer el arreglo de parcelas
        foreach (Parcela parcela in parcelas)
        {
            // Verificar si la parcela está activa
            if (parcela.activeParcel)
            {
                parcela.MostrarBarras();
            }
        }
    }
}
