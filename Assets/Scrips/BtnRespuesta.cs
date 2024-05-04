/// <summary>
/// Permite responder una pregunta de la trivia recibir la respuesta elegida por el jugador.
/// </summary>
/// <author>Arturo Barrios Mendoza</author>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnRespuesta : MonoBehaviour
{
    // Variable char llamada letra
    public char letra;
    public Pregunta pregunta;
    public void Responder()
    {
        pregunta.RecibirRespuesta(letra);
    }
}
