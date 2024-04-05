using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinicioEstacion : MonoBehaviour
{
    // Método para reiniciar todo
    public void NuevaEstacion() {
        GameObject.Find("CardManager").GetComponent<CardManager>().enabledThrow = true;
        GameObject.Find("CardManager").GetComponent<CardManager>().numCards = 0;
        GameObject.Find("ItemManager").GetComponent<ItemManager>().ResetWater();
        GameObject.Find("Cofre").GetComponent<Cofre>().chestEnabled = true;
        GameObject.Find("Cofre").GetComponent<Cofre>().closedChest.SetActive(true);
        GameObject.Find("Cofre").GetComponent<Cofre>().openChest.SetActive(false);
        GameObject.Find("BarManager").GetComponent<BarManager>().RandomHum();
        GameObject.Find("BarManager").GetComponent<BarManager>().NextSeason();
    }
}
