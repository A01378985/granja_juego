using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PausaManager : MonoBehaviour
{
    //este código pausa completamente el juego al momento de tocar el botón específico
    public GameObject pruebaPausa;

    public void PausarBoton()
    {
        pruebaPausa.SetActive(true);
    }

    public void Continar()
    {
        pruebaPausa.SetActive(false);
    }
}
