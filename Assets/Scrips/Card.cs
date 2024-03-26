using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool isRuin;
    // Referencia de tipo int para valor de dinero
    public int moneyValue;
    // Función activa de nuevo CardManager.enabled
    public void EnableAgain()
    {
        // Activar la variable de tipo bool
        FindObjectOfType<CardManager>().enabledThrow = true;
    }
    // Función para restar o sumar dinero
    public void AddMoney()
    {
        // Buscar el objeto CardManager
        FindObjectOfType<CardManager>().UpdateMoney(moneyValue);
    }
}