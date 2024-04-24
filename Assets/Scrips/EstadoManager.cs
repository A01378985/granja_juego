using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoManager : MonoBehaviour
{


    //private DataNotDestroy data;
    /*
    private void Start()
    {
        data = FindObjectOfType<DataNotDestroy>();
    }
    */

    private void Start() {
        EstadoIniciado();
    }

    // Método para cambiar el estado a "Iniciado"
    public void EstadoIniciado()
    {
        Estado.estado = "Iniciado";
        Debug.Log("El estado cambió a: " + Estado.estado);
    }
    public void EstadoFinalizadoG()
    {
        Estado.estado = "Finalizado y ganado";
        Debug.Log("El estado cambió a: " + Estado.estado);
    }
    public void EstadoFinalizadoP()
    {
        Estado.estado = "Finalizado y perdido";
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
