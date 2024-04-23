using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataNotDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private string dif;

    public void ActualizarDif(string nuevoValor)
    {   
        dif = nuevoValor;
        print("Dificultad actualizada a: " + dif);

    }
    public string DevolverDif()
    {   
        print("Dificultad final a: " + dif);

        return dif;

    }
}
