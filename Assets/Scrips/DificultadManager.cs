using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DificultadManager : MonoBehaviour
{
    public void SetEasy()
    {
        Dificultad.dificultad = "easy";
    }
    public void SetMedium()
    {
        Dificultad.dificultad = "medium";
    }
    public void SetHard()
    {
        Dificultad.dificultad = "hard";
    }
}
