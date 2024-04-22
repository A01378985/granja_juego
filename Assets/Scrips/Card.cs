using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private double moneyValue;
    // Función activa de nuevo CardManager.enabled
    public void EnableAgain()
    {
        // Activar la variable de tipo bool
        FindObjectOfType<CardManager>().EnableCardThrow();
    }
    // Función para restar o sumar dinero
    public void AddMoney()
    {
        // Buscar el objeto CardManager
        FindObjectOfType<CardManager>().UpdateMoney(moneyValue);
    }
    public void AddExtraItem() {
        GameObject.Find("Cofre").GetComponent<Cofre>().IncExtraItems();
    }
    public void AddLessItem() {
        GameObject.Find("Cofre").GetComponent<Cofre>().IncLessItems();
    }
    public void AddAumentoRendimiendo() {
        GameObject.Find("CardManager").GetComponent<CardManager>().IncAumentoRendimiento();
    }
}