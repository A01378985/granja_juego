/// <summary>
/// Permite cambiar el estado del juego.
/// </summary>
/// <author>Arturo Barrios Mendoza</author>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoManager : MonoBehaviour
{
    private void Start() {
        EstadoIniciado();
    }

    // Método para cambiar el estado a "Iniciado"
    public void EstadoIniciado()
    {
        Estado.estado = "Iniciado";
        Debug.Log("El estado cambió a: " + Estado.estado);
    }
    public void EstadoFinalizar()
    {
        if (Estado.estado == "Ganado sin trivia") {
            Estado.estado = "Finalizado y ganado";
        } else if (Estado.estado == "Perdido sin trivia") {
            Estado.estado = "Finalizado y perdido";
        } else {
            Estado.estado = "Finalizado";
        }
        Debug.Log("El estado cambió a: " + Estado.estado);
    }
    public void GanadoSinTrivia()
    {
        Estado.estado = "Ganado sin trivia";
        Debug.Log("El estado cambió a: " + Estado.estado);
    }
    public void PerdidoSinTrivia()
    {
        Estado.estado = "Perdido sin trivia";
        Debug.Log("El estado cambió a: " + Estado.estado);
    }
    // Método para obtener el estado actual
    public string ObtenerEstado()
    {
        return Estado.estado;
    }
}
