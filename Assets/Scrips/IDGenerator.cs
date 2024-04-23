using System;
using System.Collections.Generic;
using UnityEngine;

public class IDGenerator : MonoBehaviour
{
    // Caracteres válidos para generar el ID
    private static string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    // Generador de números aleatorios
    private static System.Random random = new System.Random();

    // Generar un ID único de 5 caracteres alfanuméricos
    public static string GenerateID()
    {
        // StringBuilder para construir el ID
        var builder = new System.Text.StringBuilder();

        // Generar 5 caracteres aleatorios
        for (int i = 0; i < 5; i++)
        {
            // Obtener un carácter aleatorio de los caracteres válidos
            char randomChar = validChars[random.Next(validChars.Length)];

            // Agregar el carácter al ID
            builder.Append(randomChar);
        }

        // Devolver el ID generado
        return builder.ToString();
    }
}
