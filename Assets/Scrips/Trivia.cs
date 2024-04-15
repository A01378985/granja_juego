using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trivia : MonoBehaviour
{
    // Lista de objetos de tipo pregunta
    public List<Pregunta> preguntas = new List<Pregunta>();
    // Variable int llamada preguntasRealizadas
    public int preguntasRealizadas;
    // Arreglo vacío para objetos de tipo pregunta
    public List<Pregunta> preguntasRandom = new List<Pregunta>();
    // Variable GameObject letreroFinal
    public GameObject letreroFinal;
    // Start is called before the first frame update
    void Start()
    {
        // Deshabilitar todas las preguntas de preguntas
        foreach (Pregunta pregunta in preguntas)
        {
            pregunta.gameObject.SetActive(false);
        }
        preguntasRealizadas = 0;
        // Llenar preguntasRandom con 10 Pregutnas aleatorias de preguntas
        for (int i = 0; i < Math.Min(10, preguntas.Count); i++)
        {
            int random = UnityEngine.Random.Range(0, preguntas.Count);
            preguntasRandom.Add(preguntas[random]);
            preguntas.RemoveAt(random);
        }
        letreroFinal.SetActive(false);
    }
    // Método para habilitar la siguiente pregunta en preguntasRandom
    public void SiguientePregunta()
    {
        // Deshabilitar todas las preguntas de preguntas
        foreach (Pregunta pregunta in preguntasRandom)
        {
            pregunta.gameObject.SetActive(false);
        }
        // Si preguntasRealizadas es menor a 10
        if (preguntasRealizadas < 10)
        {
            // Habilitar la pregunta en preguntasRandom en la posición preguntasRealizadas
            preguntasRandom[preguntasRealizadas].gameObject.SetActive(true);
            // Aumentar en 1 preguntasRealizadas
            preguntasRealizadas++;
        }
        // Si preguntasRealizadas es igual a 10
        else
        {
            letreroFinal.SetActive(true);
        }
    }
}
