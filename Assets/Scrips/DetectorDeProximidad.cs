using UnityEngine;

public class DetectorDeProximidad : MonoBehaviour
{
    public GameObject objetoAnimado; // Asigna el objeto animado en el inspector de Unity
    public GameObject objetoCultivo; // Asigna el objeto de cultivo en el inspector de Unity
    public float distanciaDeActivacion = 5f; // La distancia a la que se activará la animación y el cultivo
    private ObjetoAnimado controladorDeAnimacion;
    private Cultivo cultivo;

    void Start()
    {
        if (objetoAnimado != null)
        {
            controladorDeAnimacion = objetoAnimado.GetComponent<ObjetoAnimado>();
        }
        if (objetoCultivo != null)
        {
            cultivo = objetoCultivo.GetComponent<Cultivo>();
        }
    }

    void Update()
    {
        if (controladorDeAnimacion != null && cultivo != null)
        {
            float distancia = Vector3.Distance(transform.position, objetoAnimado.transform.position);
            if (distancia <= distanciaDeActivacion)
            {
                controladorDeAnimacion.ActivarAnimacion(true);
                if(Input.GetKeyDown(KeyCode.E)){
                    cultivo.StartGrowth(); // Inicia el crecimiento del cultivo
                }
            }
            else
            {
                controladorDeAnimacion.ActivarAnimacion(false);
            }
        }
    }
}
