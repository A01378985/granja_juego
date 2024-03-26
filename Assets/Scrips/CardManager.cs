using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    public string dificultad = "easy"; // easy, medium, hard
    // Crear una variable de tipo string para primavera-otono o para verano-invierno
    public string season = "springAutumn"; // springAutumn, summerWinter
    // Crear una variable de tipo int para el número de parcelas
    public int numCrops = 1;
    // Crear una variable de tipo int para el número de cartas tiradas
    public int numCards = 0;
    // Crear una variable de tipo int para el número de cartas ruin tiradas
    public int ruin = 0;
    // Crear una variable de tipo int llamada límite
    public int limit = 0;
    // Referencia al letrero de dinero
    public TMPro.TextMeshProUGUI letretoDinero;
    // Crear una variable de tipo int para el dinero
    public int dinero = 0;
    private void Start()
    {
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
        dinero = 0;
        // Actualizar el letrero de dinero
        UpdateMoney(0);     
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
        if (ruin < (numCrops -1)) {
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
    public void UpdateMoney(int money)
    {
        dinero += money;
        letretoDinero.text = dinero.ToString();
    }
}
