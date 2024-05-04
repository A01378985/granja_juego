/// <summary>
/// Clase que se encarga de manejar las preguntas y respuestas de los juegos.
/// Determina si una pregunta se contestó correctamente o no.
/// </summary>
/// <author>Arturo Barrios Mendoza y Fidel Alexander Bonilla Monalvo</author>

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;
using System.Text;

using UnityEngine.Networking;
public class Pregunta : MonoBehaviour
{
    public int id;
    public char respuestaUsuario { get; private set; }
    public char respuestaCorrecta;

    [SerializeField]
    private GameObject descripcion;

    [SerializeField]
    private GameObject BotonJugar;

    [SerializeField]
    private List<BtnRespuesta> opciones = new List<BtnRespuesta>();
    private AudioSource sonidoRight;
    private AudioSource sonidoWrong;
    private string correcta;
    private string j_id;
    
    private DataNotDestroy data;
    private void Start()
    {
        data = FindObjectOfType<DataNotDestroy>();
        j_id = data.GetId();
        // Llenar el arreglo opciones con los GameObjects hijos 1, 2 y 3 de este GameObject
        opciones = new List<BtnRespuesta>(GetComponentsInChildren<BtnRespuesta>());
        // Asignar a respuestaUsuario el valor de ' '
        respuestaUsuario = ' ';
        sonidoRight = GameObject.Find("Correcto").GetComponent<AudioSource>();
        sonidoWrong = GameObject.Find("Incorrecto").GetComponent<AudioSource>();
    }
    // Método void llamado RecibirRespuesta. Recibe un char llamado r
    public void RecibirRespuesta(char r)
    {

        // Asignar a respuestaUsuario el valor de r
        respuestaUsuario = r;
        // Activar el GameObject descripcion, si es que existe
        if (descripcion != null)
        {
            
            descripcion.SetActive(true);
        }
        // Activar el GameObject BotonJugar
        BotonJugar.SetActive(true);
        // Si respuestaUsuario es igual a respuestaCorrecta
        if (respuestaUsuario == respuestaCorrecta)
        {
            sonidoRight.Play();
            correcta = "true";
            // Colorear con el valor hexadecimal 9CB642 el BtnRespuesta en opciones con letra==r 
            foreach (BtnRespuesta opcion in opciones)
            {
                if (opcion.GetComponent<BtnRespuesta>().letra == r)
                {
                    opcion.GetComponent<UnityEngine.UI.Image>().color = new Color(0.61f, 0.71f, 0.26f);
                }
            }
        }
        // Si respuestaUsuario no es igual a respuestaCorrecta
        else
        {
            correcta = "false";
            sonidoWrong.Play();
            // Colorear con el valor hexadecimal B64E43 el BtnRespuesta en opciones con letra==r
            foreach (BtnRespuesta opcion in opciones)
            {
                if (opcion.GetComponent<BtnRespuesta>().letra == r)
                {
                    opcion.GetComponent<UnityEngine.UI.Image>().color = new Color(0.71f, 0.31f, 0.26f);
                }
            }
        }
        GameObject texto = GameObject.Find("Pregunta");
        TextMeshProUGUI textMesh2 = texto.GetComponentInChildren<TextMeshProUGUI>();
        print(textMesh2.text);

        GameObject button = GameObject.Find(respuestaUsuario.ToString());
        TextMeshProUGUI textMesh = button.GetComponentInChildren<TextMeshProUGUI>();
        print(textMesh.text);

        print(correcta);
        // Deshabilitar los botones en opciones cuya letra sea diferente a r
        foreach (BtnRespuesta opcion in opciones)
        {
            if (opcion.GetComponent<BtnRespuesta>().letra != r)
            {
                opcion.GetComponent<UnityEngine.UI.Button>().interactable = false;
            }
        }

        Cuestionario data = new Cuestionario
        {
            pregunta = textMesh2.text,
            respuesta = textMesh.text,
            correcta = correcta,
            juego_id = j_id
        };

        string jsonData = JsonUtility.ToJson(data);
        print(jsonData);
        StartCoroutine(UpdatePlayerData(jsonData));
    } 


 IEnumerator UpdatePlayerData(string playerDataJson)
    {
        print(playerDataJson);
        var request = new UnityWebRequest("http://localhost:8000/pregunta", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(playerDataJson);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Player data updated successfully");
        }
        else
        {
            Debug.LogError("Error updating player data: " + request.error);
        }
    }


    [System.Serializable]

    public class Cuestionario
    {
        public string juego_id;
        public string pregunta;
        public string respuesta;
        public string correcta;
    }
}
