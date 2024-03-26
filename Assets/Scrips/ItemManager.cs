using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Crear un arreglo de objetos de tipo Parcela
    public List<Parcela> parcelas = new List<Parcela>();
    // Crear una variable de tipo int para item fertilizante
    public int fertilizer = 3;
    // Crear una variable de tipo int para item riego
    public int irrigation = 3;
    // Crear una variable de tipo int para item herramienta
    public int tool = 3;
    // Crear una variable de tipo int para item semilla
    public int seed = 3;
    // Crear una variable de tipo int para item trabajador
    public int worker = 3;
    // Referencias a los objetos text mesh pro ugui
    public TMPro.TextMeshProUGUI fertilizerText;
    public TMPro.TextMeshProUGUI irrigationText;
    public TMPro.TextMeshProUGUI toolText;
    public TMPro.TextMeshProUGUI seedText;
    public TMPro.TextMeshProUGUI workerText;
    private void Start()
    {
        // Actualizar los textos de la interfaz
        UpdateTexts();
    }
    // Método para encontrar la parcela activa
    public Parcela FindActiveParcel()
    {
        // Crear un ciclo para recorrer el arreglo de parcelas
        foreach (Parcela parcela in parcelas)
        {
            // Verificar si la parcela está activa
            if (parcela.activeParcel)
            {
                // Retornar la parcela activa
                return parcela;
            }
        }
        // Retornar nulo
        return null;
    }
    // Método para restar un item de fertilizante y actualizar el texto
    public void UseFertilizer()
    {
        Parcela activeParcel = FindActiveParcel();
        if (fertilizer > 0 && activeParcel != null && activeParcel.fertilizer < 2)
        {
            fertilizer--;
            fertilizerText.text = fertilizer.ToString();
            activeParcel.fertilizer++;
            activeParcel.productivity += 10;
            activeParcel.EnablePlants();
        }
    }
    // Método para restar un item de riego y actualizar el texto
    public void UseIrrigation()
    {
        Parcela activeParcel = FindActiveParcel();
        if (irrigation > 0 && activeParcel != null)
        {
            irrigation--;
            irrigationText.text = irrigation.ToString();
            activeParcel.water++;
        }
    }
    // Método para restar un item de herramienta y actualizar el texto
    public void UseTool()
    {
        Parcela activeParcel = FindActiveParcel();
        if (tool > 0 && activeParcel != null && activeParcel.tool < 1)
        {
            tool--;
            toolText.text = tool.ToString();
            activeParcel.tool++;
            activeParcel.productivity += 10;
            activeParcel.EnablePlants();
        }
    }
    // Método para restar un item de semilla y actualizar el texto
    public void UseSeed()
    {
        if (seed > 0)
        {
            seed--;
            seedText.text = seed.ToString();
        }
    }
    // Método para restar un item de trabajador y actualizar el texto
    public void UseWorker()
    {
        Parcela activeParcel = FindActiveParcel();
        if (worker > 0 && activeParcel != null && activeParcel.worker < 2)
        {
            worker--;
            workerText.text = worker.ToString();
            activeParcel.worker++;
            activeParcel.productivity += 10;
            activeParcel.EnablePlants();
        }
    }
    // Método para actualizar los textos de la interfaz para todos los items
    public void UpdateTexts()
    {
        // Actualizar el texto de fertilizante
        fertilizerText.text = fertilizer.ToString();
        // Actualizar el texto de riego
        irrigationText.text = irrigation.ToString();
        // Actualizar el texto de herramienta
        toolText.text = tool.ToString();
        // Actualizar el texto de semilla
        seedText.text = seed.ToString();
        // Actualizar el texto de trabajador
        workerText.text = worker.ToString();
    }
}
