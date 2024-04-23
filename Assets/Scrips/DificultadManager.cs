using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultadManager : MonoBehaviour
{
    private string tipo;
    private dataReader dataReader;
    private DataNotDestroy data;
    private void Start()
    {
        data = FindObjectOfType<DataNotDestroy>();
    }
    public void SetEasy()
    {
        Dificultad.dificultad = "easy";
        
        data.ActualizarDif("verqor");
    }
    public void SetMedium()
    {
        Dificultad.dificultad = "medium";    
        data.ActualizarDif("tradicional");
    }
    public void SetHard()
    {
        Dificultad.dificultad = "hard";
        data.ActualizarDif("informal");
    }
}
