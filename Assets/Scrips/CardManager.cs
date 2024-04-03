using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    // Crear una variable de tipo bool para lanzar carta
    public bool enabledThrow;
    // Crear una lista de cartas buenas
    public List<Card> goodCards = new List<Card>();
    // Crear una lista de cartas malas
    public List<Card> badCards = new List<Card>();
    // Crear una lista de cartas especiales
    public List<Card> specialCards = new List<Card>();
    // Crear una variable de tipo string para la dificultad
    public string dificultad;// = "easy"; // easy, medium, hard
    // Crear una variable de tipo string para primavera-otono o para verano-invierno
    public string season = "springAutumn"; // springAutumn, summerWinter
    // Crear una variable de tipo int para el número de parcelas
    public int numCrops;
    // Crear una variable de tipo int para el número de cartas tiradas
    public int numCards = 0;
    // Crear una variable de tipo int para el número de cartas ruin tiradas
    public int ruin = 0;
    // Crear una variable de tipo int llamada límite
    public int limit = 0;
    // Referencia al letrero de dinero
    public TMPro.TextMeshProUGUI letretoDinero;
    public GameObject letreroEstacion;
    // Crear una variable de tipo double para el dinero
    public double dinero = 0;
    public double pago = 0;
    public double tasa = 0;
    public int aumentoRendimiento = 0;
    public TMPro.TextMeshProUGUI letretoCobrar;
    public TMPro.TextMeshProUGUI letretoDeuda;
    public TMPro.TextMeshProUGUI letretoTasa;
    public TMPro.TextMeshProUGUI letretoPagar;
    private void Start()
    {
        dificultad = Dificultad.dificultad;
        // Publicar un mensaje en consola
        Debug.Log("Dificultad: " + dificultad);
        enabledThrow = true;
        // Modificar el límite
        if (dificultad == "easy" && season == "springAutumn")
        {
            limit = 70;
            // Eliminar la cuarta carta de la lista badCards
            badCards.RemoveAt(4);
        } else if (dificultad == "easy" && season == "summerWinter")
        {
            limit = 60;
            // Eliminar la cuarta carta de la lista badCards
            badCards.RemoveAt(4);
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
        // Inicializar el dinero en 0
        dinero = 0.00;
        pago = 0.00;
        // Actualizar el letrero de dinero
        UpdateMoney(0.00);
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
                numCrops--;
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
            } else if (myRandNum <= 96) {
                ThrowBadCard();
            } else {
                ThrowSpecialCard();
            }
        }
    }
    // Crear una función para actualizar el letrero de dinero y que reciba un valor de tipo int
    public void UpdateMoney(double money)
    {
        dinero += money;
        // Display money in the format "0.00"
        dinero = System.Math.Round(dinero, 2);
        letretoDinero.text = dinero.ToString("F2");
    }
    // Método para determinar cuánto dinero recibirá el jugador
    public void CalcularPago() { // ELIMINAR LOS DEBUG
        int parcelas = GameObject.Find("BarManager").GetComponent<BarManager>().numParcelas;
        Debug.Log("Parcelas: " + parcelas);
        Debug.Log("Productividad: " + GameObject.Find("BarManager").GetComponent<BarManager>().currentProd);
        pago = 3500 * parcelas * GameObject.Find("BarManager").GetComponent<BarManager>().currentProd;
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
        }
    }
    public void ActualizarLetrerosEstacion() {
        CalcularPago();
        DeterminarTasa();
        double deuda = GameObject.Find("Cofre").GetComponent<Cofre>().deuda;
        double total = deuda * tasa;
        letretoCobrar.text = pago.ToString("F2");
        letretoDeuda.text = deuda.ToString("F2");
        letretoTasa.text = tasa.ToString("F2");
        letretoPagar.text = total.ToString("F2");
    }
}
