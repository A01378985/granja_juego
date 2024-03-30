using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cofre : MonoBehaviour
{
    // Variable de tipo bool enable para el cofre
    public bool chestEnabled;
    // Variable GameObject para el letrero de abrir
    public GameObject letreroCofre;
    public int extraItems;
    public int lessItems;
    public string dificultad;
    public bool insideChest;
    // Variable GameObject para el cofre cerrado
    public GameObject closedChest;
    // Variable GameObject para el cofre abierto
    public GameObject openChest;
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
            FindObjectOfType<CardManager>().UpdateMoney(-250000);
            closedChest.SetActive(false);
            openChest.SetActive(true);
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
}
