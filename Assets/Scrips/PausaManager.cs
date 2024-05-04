/// <summary>
/// Clase que se encarga de pausar y reanudar el juego.
/// </summary>
/// <author>Arturo Barrios Mendoza</author>

using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PausaManager : MonoBehaviour
{
    // Referencia al GameObject del panel pausa
    public GameObject panelPausa;
    // estado del juego, pausado o no
    public bool estaPausado = false;
    // función para pausar el juego
    public void Pausar()
    {
        // cambiar el estado del juego
        estaPausado = true;
        // activar o desactivar el panel de pausa
        panelPausa.SetActive(true);
        Time.timeScale = (estaPausado) ? 0 : 1;
    }
    public void Reanudar()
    {
        // cambiar el estado del juego
        estaPausado = false;
        // activar o desactivar el panel de pausa
        panelPausa.SetActive(false);
        Time.timeScale = (estaPausado) ? 0 : 1;
    }
}

