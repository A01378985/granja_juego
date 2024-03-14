using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorDeProximidad : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject objetoAnimado; // Asigna el objeto animado en el inspector de Unity
    public float distanciaDeActivacion = 5f; // La distancia a la que se activará la animación

    private ObjetoAnimado controladorDeAnimacion;

    void Start()
    {
        if (objetoAnimado != null)
        {
            controladorDeAnimacion = objetoAnimado.GetComponent<ObjetoAnimado>();
        }
    }

    void Update()
    {
        if (controladorDeAnimacion != null)
        {
            float distancia = Vector3.Distance(transform.position, objetoAnimado.transform.position);
            if (distancia <= distanciaDeActivacion)
            {
                controladorDeAnimacion.ActivarAnimacion(true);
            }
            else
            {
                controladorDeAnimacion.ActivarAnimacion(false);
            }
        }
    }
}
