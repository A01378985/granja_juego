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
