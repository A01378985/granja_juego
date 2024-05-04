/// <summary>
/// Con base en la dificultad y la estación, calcula la probabilidad para lanzar una carta buena, mala o especial.
/// Además, lleva el control de las cartas lanzadas y el dinero del jugador.
/// También maneja los letreros que aparecen al final de cada estación.
/// </summary>
/// <author>Arturo Barrios Mendoza</author>


using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private dataReader dataReader;
    [SerializeField]
    private bool enabledThrow;

    [SerializeField]
    private List<Card> goodCards = new List<Card>();

    [SerializeField]
    private List<Card> badCards = new List<Card>();

    [SerializeField]
    private List<Card> specialCards = new List<Card>();

    public string dificultad;
    public string season { get; private set; }
    public int numCrops { get; private set; }
    public int numCards { get; private set; }
    public int ruin { get; private set; }
    public int limit { get; private set; }

    [SerializeField]
    private TMPro.TextMeshProUGUI letretoDinero;

    [SerializeField]
    private GameObject letreroEstacion;

    public double dinero { get; private set; }
    public double pago { get; private set; }
    public double tasa { get; private set; }
    public int aumentoRendimiento { get; private set; }

    [SerializeField]
    private TMPro.TextMeshProUGUI letretoCobrar;

    [SerializeField]
    private TMPro.TextMeshProUGUI letretoDeuda;

    [SerializeField]
    private TMPro.TextMeshProUGUI letretoTasa;

    [SerializeField]
    private TMPro.TextMeshProUGUI letretoPagar;

    public double total { get; private set; }

    [SerializeField]
    private AudioSource sonidoCarta;

    [SerializeField]
    private AudioSource sonidoSuccess;

    [SerializeField]
    private AudioSource sonidoFail;

    [SerializeField]
    private AudioSource sonidoSmallPoints;

    [SerializeField]
    private AudioSource sonidoSmallFail;

    public int numContratos { get; private set; }
    private void Start()
    {
        dataReader = FindObjectOfType<dataReader>();
        dificultad = Dificultad.dificultad;
        season = "springAutumn";
        if (dificultad == "easy") {
            badCards.RemoveAt(4);
        }
        CheckLimit();
        // Inicializar el dinero en 0
        dinero = 0.00;
        pago = 0.00;
        // Actualizar el letrero de dinero
        UpdateMoney(0.00);
        sonidoCarta = GameObject.Find("SonidoCarta").GetComponent<AudioSource>();
        sonidoSuccess = GameObject.Find("SonidoSuccess").GetComponent<AudioSource>();
        sonidoFail = GameObject.Find("SonidoFail").GetComponent<AudioSource>();
        sonidoSmallPoints = GameObject.Find("SonidoSmallPoints").GetComponent<AudioSource>();
        sonidoSmallFail = GameObject.Find("SonidoSmallFail").GetComponent<AudioSource>();
        numContratos = 0;
    }
    // Crear una función para lanzar una carta buena
    public void ThrowGoodCard()
    {
        // Publicar un mensaje en consola
        Debug.Log("Good Card");
        Card randomCard = goodCards[Random.Range(0, goodCards.Count)];
        randomCard.gameObject.SetActive(true);
        enabledThrow = false;
    }
    // Crear una función para lanzar una carta mala
    public void ThrowBadCard()
    {
        // Publicar un mensaje en consola
        Debug.Log("Bad Card");
        if (ruin < (numCrops -2)) {
            Card randomCard = badCards[Random.Range(0, badCards.Count)];
            if ((randomCard == badCards[0]) || (randomCard == badCards[1])) {
                ruin++;
                sonidoFail.Play();
            }
            randomCard.gameObject.SetActive(true);
        } else {
            Card randomCard = badCards[Random.Range(2, badCards.Count)];
            randomCard.gameObject.SetActive(true);
        }
        enabledThrow = false;
    }
    // Crear una función para lanzar una carta especial
    public void ThrowSpecialCard()
    {
        // Publicar un mensaje en consola
        Debug.Log("Special Card");
        Card randomCard = specialCards[0];
        randomCard.gameObject.SetActive(true);
        enabledThrow = false;
    }
    // Crear una función para lanzar una carta
    public void ThrowCard()
    {
        if (enabledThrow && (numCards < 10)) {
            numCards++;
            int myRandNum = Random.Range(0, 100);
            if (myRandNum <= limit)
            {
                ThrowGoodCard();
            } else if (myRandNum <= 94) {
                ThrowBadCard();
            } else {
                ThrowSpecialCard();
            }
            sonidoCarta.Play();
        }
    }
    // Crear una función para actualizar el letrero de dinero y que reciba un valor de tipo int
    public void UpdateMoney(double money)
    {
        dinero += money;
        // Display money in the format "0.00"
        dinero = System.Math.Round(dinero, 2);
        letretoDinero.text = dinero.ToString("F2");
        dataReader.ActualizarBalances(dinero.ToString("F2"));   
    }
    // Método para determinar cuánto dinero recibirá el jugador
    public void CalcularPago() { // ELIMINAR LOS DEBUG
        int parcelas = GameObject.Find("BarManager").GetComponent<BarManager>().numParcelas;
        Debug.Log("Parcelas: " + parcelas);
        Debug.Log("Productividad: " + GameObject.Find("BarManager").GetComponent<BarManager>().currentProd);
        pago = 0.35 * GameObject.Find("BarManager").GetComponent<BarManager>().currentProd;
        Debug.Log("Pago: " + pago);
        for (int i = 0; i < aumentoRendimiento; i++) {
            pago *= 1.2;
        }
        Debug.Log("Cartas rendimiento: " + aumentoRendimiento);
        Debug.Log("Pago: " + pago);
    }
    public void DeterminarTasa() {
        if (dificultad == "easy") {
            tasa = 1.15;
        } else if (dificultad == "medium") {
            tasa = 1.4;
        } else if (dificultad == "hard") {
            float parteFlotante = Random.Range(0.0f, 0.5f);
            tasa = 1.5 + (double)parteFlotante;
        }
    }
    public void LastOne() {
        if (numCards == 10)
        {
            GameObject.Find("HumidityManager").GetComponent<HumidityManager>().CheckIrrigation();
            letreroEstacion.SetActive(true);
            ActualizarLetrerosEstacion();
            SwitchSeason();
            CheckLimit();
        }
    }
    public void ActualizarLetrerosEstacion() {
        sonidoSuccess.Play();
        CalcularPago();
        DeterminarTasa();
        double deuda = GameObject.Find("Cofre").GetComponent<Cofre>().deuda;
        total = deuda * tasa;
        letretoCobrar.text = pago.ToString("F2");
        letretoDeuda.text = deuda.ToString("F2");
        letretoTasa.text = tasa.ToString("F2");
        letretoPagar.text = total.ToString("F2");
    }
    public void EfectuarPagoEstacion() {
        UpdateMoney(pago);
        UpdateMoney(-total);
    }
    public void PlaySmallPoints() {
        sonidoSmallPoints.Play();
    }
    public void PlaySmallFail() {
        sonidoSmallFail.Play();
    }
    public void AumentarContratos() {
        numContratos++;
        dataReader.ActualizarContratos(numContratos.ToString());   
    }
    public void NumCropsSetter (int num) {
        numCrops = num;
    }
    public void IncNumCrops () {
        numCrops++;
    }
    public void DecNumCrops () {
        numCrops--;
    }
    public void EnableCardThrow() {
        enabledThrow = true;
    }
    public void IncAumentoRendimiento() {
        aumentoRendimiento++;
    }
    public void ResetNumCards() {
        numCards = 0;
    }
    public void ResetAumentoRendimiento() {
        aumentoRendimiento = 0;
    }
    public void SwitchSeason() {
        if (season == "springAutumn") {
            season = "summerWinter";
        } else {
            season = "springAutumn";
        }
    }
    public void CheckLimit() {
        // Publicar un mensaje en consola
        Debug.Log("Dificultad: " + dificultad);
        Debug.Log("Estación: " + season);
        enabledThrow = true;
        // Modificar el límite
        if (dificultad == "easy" && season == "springAutumn")
        {
            limit = 70;
        } else if (dificultad == "easy" && season == "summerWinter")
        {
            limit = 60;
        } else if (dificultad == "medium" && season == "springAutumn")
        {
            limit = 55;
        } else if (dificultad == "medium" && season == "summerWinter")
        {
            limit = 50;
        } else if (dificultad == "hard" && season == "springAutumn")
        {
            limit = 45;
        } else if (dificultad == "hard" && season == "summerWinter")
        {
            limit = 40;
        }
        Debug.Log("Limit: " + limit);
    }
}
