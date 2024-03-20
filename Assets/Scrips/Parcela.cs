using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Parcela : MonoBehaviour
{
    // Crear una variable de tipo bool para parcela activa
    public bool activeParcel;
    // Crear una variable de tipo int para productividad
    public int productivity;
    // Crear una variable de tipo int para agua
    public int water;
    // Crear una variable de tipo int para fertilizante
    public int fertilizer;
    // Crear una variable de tipo int para herramienta
    public int tool;
    // Crear una variable de tipo int para trabajador
    public int worker;
    // Detectar la colisión con el jugador
    private void Start()
    {
        activeParcel = false;
        productivity = 0;
        water = 0;
        fertilizer = 0;
        tool = 0;
        worker = 0;
    }
    // Detectar la colisión con el jugador
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            activeParcel = true;
        }
    }
    // Detectar la salida de la colisión con el jugador
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            activeParcel = false;
        }
    }
}
