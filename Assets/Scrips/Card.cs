using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    public bool isRuin;
    // Función activa de nuevo CardManager.enabled
    public void EnableAgain()
    {
        // Activar la variable de tipo bool
        FindObjectOfType<CardManager>().enabledThrow = true;
    }
}