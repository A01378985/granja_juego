using System.Collections.Generic;
using UnityEngine;

public class ObjetoAnimado : MonoBehaviour
{
    private List<Animator> animadores = new List<Animator>();

    void Start()
    {
        RecopilarAnimadores(gameObject);
    }

    private void RecopilarAnimadores(GameObject objetoPadre)
    {
        // Recopila los animadores de todos los hijos recursivamente
        Animator[] hijosAnimadores = objetoPadre.GetComponentsInChildren<Animator>();
        foreach (Animator animador in hijosAnimadores)
        {
            animadores.Add(animador);
        }
    }

    public void ActivarAnimacion(bool activar)
    {
        foreach (Animator animador in animadores)
        {
            animador.SetBool("aproximacion", activar);
        }
    }
}