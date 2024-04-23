using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultadManager : MonoBehaviour
{
    private dataReader dataReader;
    private void Start()
    {
        dataReader = FindObjectOfType<dataReader>();
    }
    public void SetEasy()
    {
        Dificultad.dificultad = "easy";
        dataReader.ActualizarUltimoJuego("verqor");
    }
    public void SetMedium()
    {
        Dificultad.dificultad = "medium";
        dataReader.ActualizarUltimoJuego("tradicional");
    }
    public void SetHard()
    {
        Dificultad.dificultad = "hard";
        dataReader.ActualizarUltimoJuego("informal");
    }
}
