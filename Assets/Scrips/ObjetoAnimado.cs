using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoAnimado : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ActivarAnimacion(bool activar)
    {
        anim.SetBool("aproximacion", activar);
    }
}
