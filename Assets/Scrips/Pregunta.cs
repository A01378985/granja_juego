using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pregunta : MonoBehaviour
{
    // Variable int llamada id
    public int id;
    // Variable char llamada respuestaUsuario
    public char respuestaUsuario;
    // Variable char llamada respuestaCorrecta
    public char respuestaCorrecta;
    // Variable GameObject llamada descripcion
    public GameObject descripcion;
    // Variable GameObject llamada BotonJugar
    public GameObject BotonJugar;
    // Arreglo de GameObject llamado opciones
    public List<BtnRespuesta> opciones = new List<BtnRespuesta>();
    private void Start()
    {
        // Llenar el arreglo opciones con los GameObjects hijos 1, 2 y 3 de este GameObject
        opciones = new List<BtnRespuesta>(GetComponentsInChildren<BtnRespuesta>());
        // Asignar a respuestaUsuario el valor de ' '
        respuestaUsuario = ' ';
    }
    // Método void llamado RecibirRespuesta. Recibe un char llamado r
    public void RecibirRespuesta(char r)
    {
        // Asignar a respuestaUsuario el valor de r
        respuestaUsuario = r;
        // Activar el GameObject descripcion, si es que existe
        if (descripcion != null)
        {
            descripcion.SetActive(true);
        }
        // Activar el GameObject BotonJugar
        BotonJugar.SetActive(true);
        // Si respuestaUsuario es igual a respuestaCorrecta
        if (respuestaUsuario == respuestaCorrecta)
        {
            // Colorear con el valor hexadecimal 9CB642 el BtnRespuesta en opciones con letra==r 
            foreach (BtnRespuesta opcion in opciones)
            {
                if (opcion.GetComponent<BtnRespuesta>().letra == r)
                {
                    opcion.GetComponent<UnityEngine.UI.Image>().color = new Color(0.61f, 0.71f, 0.26f);
                }
            }
        }
        // Si respuestaUsuario no es igual a respuestaCorrecta
        else
        {
            // Colorear con el valor hexadecimal B64E43 el BtnRespuesta en opciones con letra==r
            foreach (BtnRespuesta opcion in opciones)
            {
                if (opcion.GetComponent<BtnRespuesta>().letra == r)
                {
                    opcion.GetComponent<UnityEngine.UI.Image>().color = new Color(0.71f, 0.31f, 0.26f);
                }
            }
        }
        // Deshabilitar los botones en opciones cuya letra sea diferente a r
        foreach (BtnRespuesta opcion in opciones)
        {
            if (opcion.GetComponent<BtnRespuesta>().letra != r)
            {
                opcion.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
        }
    } 
}
