using System;
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
    // Variable int para el número de parcelas desbloqueadas
    public int unlockedParcels = 0;
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
        // Actualizar las parcelas desbloqueadas con base en la variable unlocked de cada parcela
        unlockedParcels = CountParcels();
        GameObject.Find("CardManager").GetComponent<CardManager>().numCrops = unlockedParcels;
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
        if (fertilizer > 0 && activeParcel != null && activeParcel.fertilizer < 2 && activeParcel.unlocked)
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
        if (irrigation > 0 && activeParcel != null && activeParcel.unlocked)
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
        if (tool > 0 && activeParcel != null && activeParcel.tool < 1 && activeParcel.unlocked)
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
        if (worker > 0 && activeParcel != null && activeParcel.worker < 2 && activeParcel.unlocked)
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
    // Método para encontrar una parcela desbloqueada y con productividad menor a 100, y añadirle productividad
    public void AddProductivity()
    {
        // Crear un ciclo para recorrer el arreglo de parcelas
        foreach (Parcela parcela in parcelas)
        {
            // Verificar si la parcela está desbloqueada y tiene productividad menor a 100
            if (parcela.unlocked && parcela.productivity < 100)
            {
                // Añadir productividad
                parcela.productivity += 10;
                parcela.EnablePlants();
                // Salir del ciclo
                break;
            }
        }
    }
    // Método para encontrar una parcela desbloqueada y con productividad mayor a 0, y restarle productividad
    public void SubstractProductivity()
    {
        // Crear un ciclo para recorrer el arreglo de parcelas
        foreach (Parcela parcela in parcelas)
        {
            // Verificar si la parcela está desbloqueada y tiene productividad mayor a 0
            if (parcela.unlocked && parcela.worker > 1)
            {
                // Restar productividad
                parcela.worker--;
                parcela.productivity -= 10;
                parcela.EnablePlants();
                // Salir del ciclo
                break;
            }
        }
    }
    // Función para bloquear una parcela y reestablecer sus valores
    public void RuinParcel()
    {
        // Crear un ciclo para recorrer el arreglo de parcelas
        foreach (Parcela parcela in parcelas)
        {
            // Verificar si la parcela está activa
            if (parcela.unlocked)
            {
                // Bloquear la parcela
                parcela.unlocked = false;
                parcela.productivity = 0;
                parcela.water = 0;
                parcela.fertilizer = 0;
                parcela.tool = 0;
                parcela.worker = 0;
                parcela.EnablePlants();
                unlockedParcels--;
                // Salir del ciclo
                break;
            }
        }
    }
    // Función para actualizar las parcelas desbloqueadas con base en la variable unlocked de cada parcela
    public int CountParcels()
    {
        foreach (Parcela parcela in parcelas)
        {
            if (parcela.unlocked)
            {
                unlockedParcels++;
            }
        }
        return unlockedParcels;
    }
    // Función para restar 3 items de cada tipo y desbloquear una parcela
    public void RestarTres()
    {
        tool -= 3;
        toolText.text = tool.ToString();
        seed -= 3;
        seedText.text = seed.ToString();
        worker -= 3;
        workerText.text = worker.ToString();
        irrigation -= 3;
        irrigationText.text = irrigation.ToString();
    }
    // Función para revisar si hay 3 o más ítems de cada tipo
    public bool CheckThree()
    {
        if (tool >= 3 && seed >= 3 && worker >= 3 && irrigation >= 3)
        {
            return true;
        }
        return false;
    }
}
