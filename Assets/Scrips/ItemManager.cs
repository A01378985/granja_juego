/// <summary>
/// Clase que administra los items del juego.
/// Permite usar items, añadir o restar productividad a las parcelas, 
/// bloquear parcelas y contar parcelas desbloqueadas.
/// Al final de cada estación reestablece el agua en todas las parcelas desbloqueadas.
/// </summary>
/// <author>Arturo Barrios Mendoza y Fidel Alexander Bonilla Montalvo</author>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private DataReader dataReader;
    [SerializeField]
    private List<Parcela> parcelas = new List<Parcela>();

    public int fertilizer { get; private set; }
    public int irrigation { get; private set; }
    public int tool { get; private set; }
    public int seed { get; private set; }
    public int worker { get; private set; }
    public int unlockedParcels { get; private set; }

    [SerializeField]
    private TMPro.TextMeshProUGUI fertilizerText;

    [SerializeField]
    private TMPro.TextMeshProUGUI irrigationText;

    [SerializeField]
    private TMPro.TextMeshProUGUI toolText;

    [SerializeField]
    private TMPro.TextMeshProUGUI seedText;

    [SerializeField]
    private TMPro.TextMeshProUGUI workerText;

    [SerializeField]
    private AudioSource sonidoAgua;

    [SerializeField]
    private AudioSource sonidoTrabajador;

    [SerializeField]
    private AudioSource sonidoHerramienta;

    [SerializeField]
    private AudioSource sonidoFertilizante;

    [SerializeField]
    private AudioSource sonidoPuntos;
    private string lastChar;
    private int number;
    private void Start()
    {
        dataReader = FindObjectOfType<DataReader>();
        
        // Actualizar los textos de la interfaz
        UpdateTexts();
        // Actualizar las parcelas desbloqueadas con base en la variable unlocked de cada parcela
        unlockedParcels = CountParcels();
        GameObject.Find("CardManager").GetComponent<CardManager>().NumCropsSetter(unlockedParcels);
        sonidoAgua = GameObject.Find("SonidoAgua").GetComponent<AudioSource>();
        sonidoTrabajador = GameObject.Find("SonidoTrabajador").GetComponent<AudioSource>();
        sonidoHerramienta = GameObject.Find("SonidoHerramienta").GetComponent<AudioSource>();
        sonidoPuntos = GameObject.Find("SonidoPuntos").GetComponent<AudioSource>();
        sonidoFertilizante = GameObject.Find("SonidoFertilizante").GetComponent<AudioSource>();
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
                lastChar = parcela.name.Substring(parcela.name.Length - 1);
                number = int.Parse(lastChar); // Convierte el carácter a un número
                number--; // Resta uno al número
                print(number); // Imprime el número
                dataReader.ActualizarParcela("Desbloqueada","true",number);
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
            activeParcel.IncParcelFertilizer();
            activeParcel.IncParcelProductivityByTen();
            activeParcel.EnablePlants();
            GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
            sonidoFertilizante.Play();
            dataReader.ActualizarFertilizante(fertilizer.ToString());   
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
            activeParcel.IncParcelWater();
            activeParcel.MostrarBarras();
            sonidoAgua.Play();
            dataReader.ActualizarAgua(irrigation.ToString());
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
            activeParcel.IncParcelTool();
            activeParcel.IncParcelProductivityByTen();
            activeParcel.EnablePlants();
            GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
            sonidoHerramienta.Play();
            dataReader.ActualizarHerramienta(tool.ToString());
        }
    }
    public void UseWorker()
    {
        Parcela activeParcel = FindActiveParcel();
        if (worker > 0 && activeParcel != null && activeParcel.worker < 2 && activeParcel.unlocked)
        {
            worker--;
            workerText.text = worker.ToString();
            activeParcel.IncParcelWorker();
            activeParcel.IncParcelProductivityByTen();
            activeParcel.EnablePlants();
            GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
            activeParcel.MostrarTrabajador();
            sonidoTrabajador.Play();
            dataReader.ActualizarTrabajador(worker.ToString());
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
            // Verificar si la parcela está desbloqueada y tiene productividad extra menor a 6
            if (parcela.unlocked && parcela.extraProductivity < 5)
            {
                // Añadir productividad
                parcela.IncExtraProductivity();
                parcela.IncParcelProductivityByTen();
                parcela.EnablePlants();
                GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
                sonidoPuntos.Play();
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
            if (parcela.unlocked && parcela.worker >= 1)
            {
                // Restar productividad
                parcela.QuitarUnTrabajador();
                parcela.DecParcelWorker();
                parcela.DecParcelProductivityByTen();
                parcela.EnablePlants();
                GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
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
                parcela.LockThisParcel();
                parcela.RuinThisParcel();
                parcela.ResetParcelValues();
                parcela.EnablePlants();
                unlockedParcels--;
                parcela.QuitarTodosTrabajadores();
                GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
                GameObject.Find("CardManager").GetComponent<CardManager>().DecNumCrops();
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
        dataReader.ActualizarHerramienta(tool.ToString());
        dataReader.ActualizarSemilla(seed.ToString());
        dataReader.ActualizarTrabajador(worker.ToString());
        dataReader.ActualizarAgua(irrigation.ToString());
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
    // Función que recibe un valor de tipo int e itera sobre todos los items para agregar uno, hasra que iguala el valor recibido
    public void ReceiveItems(int value)
    {
        int itemsPerType = value / 5;
        int remainder = value % 5;

        fertilizer += itemsPerType;
        irrigation += itemsPerType;
        tool += itemsPerType;
        seed += itemsPerType;
        worker += itemsPerType;

        for (int i = 0; i < remainder; i++)
        {
            switch (i)
            {
                case 0: fertilizer++; break;
                case 1: irrigation++; break;
                case 2: tool++; break;
                case 3: seed++; break;
                case 4: worker++; break;
            }
        }

        UpdateTexts();

        dataReader.ActualizarFertilizante(fertilizer.ToString());
        dataReader.ActualizarHerramienta(tool.ToString());
        dataReader.ActualizarSemilla(seed.ToString());
        dataReader.ActualizarTrabajador(worker.ToString());
        dataReader.ActualizarAgua(irrigation.ToString());
    }
    // Función para reestablecer el agua en todas las parcelas desbloqueadas
    public void ResetWater()
    {
        foreach (Parcela parcela in parcelas)
        {
            if (parcela.unlocked)
            {
                parcela.ResetParcelWater();
            }
        }
    }
    public void DecUnlockedParcels()
    {
        unlockedParcels--;
    }
    public void IncUnlockedParcels()
    {
        unlockedParcels++;
    }
}
