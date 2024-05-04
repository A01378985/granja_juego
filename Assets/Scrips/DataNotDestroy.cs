/// <summary>
/// Guarda la dificultad y el id del juego para que no se pierdan al cambiar de escena.
/// </summary>
/// <author>Fidel Alexander Bonilla Montalvo</author>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataNotDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private string dif;
    private string id_juego;

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

    public void SetId(string nuevoValor)
    {   
        id_juego = nuevoValor;
        print("Id actualizado a: " + id_juego);

    }
    public string GetId()
    {   

        return id_juego;

    }
}
