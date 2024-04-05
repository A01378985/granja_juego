using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour
{
    // Variable de tipo bool enable para el cofre
    public bool chestEnabled;
    // Variable GameObject para el letrero de abrir
    public GameObject letreroCofre;
    public GameObject letreroCofreBloqueado;
    public int extraItems;
    public int lessItems;
    public string dificultad;
    public bool insideChest;
    // Variable GameObject para el cofre cerrado
    public GameObject closedChest;
    // Variable GameObject para el cofre abierto
    public GameObject openChest;
    // Variable double para la deuda
    public double deuda;
    // Void Start
    private void Start()
    {
        // Asignar el valor de la variable chestEnabled a true
        chestEnabled = true;
        insideChest = false;
        extraItems = 0;
        lessItems = 0;
        dificultad = Dificultad.dificultad;
        closedChest.SetActive(true);
        openChest.SetActive(false);
    }
    private void Update() {
        if (chestEnabled && insideChest && Input.GetKeyDown(KeyCode.E)) {
            letreroCofre.SetActive(false);
            chestEnabled = false;
            int items = GiveItems();
            GameObject.Find("ItemManager").GetComponent<ItemManager>().ReceiveItems(items);
            closedChest.SetActive(false);
            openChest.SetActive(true);
            CalculateDebt();
            extraItems = 0;
            lessItems = 0;
        }
    }
    // Método para detectar entrada de colisión con el personaje
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si la variable chestEnabled es verdadera
        if (chestEnabled && collision.CompareTag("Player"))
        {
            // Activar el letrero de abrir
            letreroCofre.SetActive(true);
            insideChest = true;
        } else if (!chestEnabled && collision.CompareTag("Player")) {
            letreroCofreBloqueado.SetActive(true);
        }
    }
    // Método para detectar salida de colisión con el personaje
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Si la variable chestEnabled es verdadera
        if (chestEnabled && collision.CompareTag("Player"))
        {
            // Desactivar el letrero de abrir
            letreroCofre.SetActive(false);
            insideChest = false;
        } else if (!chestEnabled && collision.CompareTag("Player")) {
            letreroCofreBloqueado.SetActive(false);
        }
    }
    // Método para determinar cuántos items dar
    public int GiveItems()
    {
        if (dificultad == "easy")
        {
            return 15 + extraItems - lessItems;
        } else if (dificultad == "medium")
        {
            return 10 + extraItems - lessItems;
        } else if (dificultad == "hard")
        {
            return 5 + extraItems - lessItems;
        } else
        {
            return 0;
        }
    }
    // Método para calcular la deuda
    public void CalculateDebt()
    {
        double dineroActual = GameObject.Find("CardManager").GetComponent<CardManager>().dinero;
        if (dineroActual > 0) {
            deuda = 250000.00 - dineroActual;
            GameObject.Find("CardManager").GetComponent<CardManager>().UpdateMoney(-dineroActual);
        } else {
            deuda = 250000.00;
        }
    }
}