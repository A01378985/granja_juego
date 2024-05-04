/// <summary>
/// Se encarga de leer los datos del jugador desde el servidor y actualizarlos.
/// </summary>
/// <author>Fidel Alexander Bonilla Montalvo</author>

using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

public class DataReader : MonoBehaviour
{
    private string serverUrl = "https://cropopoly-server-production.up.railway.app/jugadores";
    private string resultUrl = "https://cropopoly-server-production.up.railway.app/result";
    private string playerDataUrl;
    private string correo;
    Juego juego = new Juego();
    
    Juego2 juego2 = new Juego2();
    private string jsonJuego;
    private string jsonJuego2;
    string newGameID = IDGenerator.GenerateID();
    Juego ultimoJuego;
    Jugador jugadorPrincipal;
    private int trabajador = 0;
    private int herramienta = 0;
    private int semilla = 0;
    private int agua = 0;
    private int fertilizante = 0;
    private DataNotDestroy data;
    private int longitud = 0;
    

    private void Start()
    {
        data = FindObjectOfType<DataNotDestroy>();
        string fechaHoraInicio = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        
        Juego juego = new Juego
        {
        fechaHoraInicio = fechaHoraInicio,
        fechaHoraFin = "00.00.00",
        tipoFinanciamiento = "",
        noEstaciones = "1",
        noContratos = "0",
        balance = "0",
        qytTrabajador = "0",
        qytHerramienta = "0",
        qytSemilla = "0",
        qytAgua = "0",
        qytFertilizante = "0",
        Parcela = new List<Parcela>()
        };


        Juego2 juego2 = new Juego2
        {
        fechaHoraInicio = fechaHoraInicio,
        fechaHoraFin = "00.00.00",
        tipoFinanciamiento = "",
        noEstaciones = "1",
        noContratos = "0",
        balance = "0",
        qytTrabajador = "0",
        qytHerramienta = "0",
        qytSemilla = "0",
        qytAgua = "0",
        qytFertilizante = "0",
        };
    for (int i = 1; i <= 5; i++)
    {
        Parcela parcela = new Parcela
        {
            numeroParcela = i.ToString(),
            qytTrabajadorPar = "0",
            qytHerramientaPar = "0",
            qytSemillaPar = "0",
            qytAguaPar = "0",
            qytFertilizantePar = "0",
            desbloqueada = "false",
            productividad = "0"
        };

        juego.Parcela.Add(parcela);
    }

        jsonJuego = JsonConvert.SerializeObject(juego);
        jsonJuego2 = JsonConvert.SerializeObject(juego2);
        Debug.Log("Datos del juego: " + jsonJuego);
        StartCoroutine(FetchData(fechaHoraInicio));
    }

    IEnumerator FetchData(string fechaHoraInicio)
    {
        UnityWebRequest wwwResult = UnityWebRequest.Get(resultUrl);
        yield return wwwResult.SendWebRequest();

        if (wwwResult.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error fetching data from result URL: " + wwwResult.error);
        }
        else
        {
            string resultJson = wwwResult.downloadHandler.text;
            print("Received data from result URL: " + resultJson);

            List<Jugador> jugadoresResult = JsonConvert.DeserializeObject<List<Jugador>>(resultJson);

            correo = jugadoresResult[jugadoresResult.Count - 1].email;

            UnityWebRequest wwwJugadores = UnityWebRequest.Get(serverUrl);
            yield return wwwJugadores.SendWebRequest();

            if (wwwJugadores.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error fetching data from jugadores URL: " + wwwJugadores.error);
            }
            else
            {
                string jugadoresJson = wwwJugadores.downloadHandler.text;
                Debug.Log("Received data from jugadores URL: " + jugadoresJson);
                print(jugadoresJson);

                List<Jugador> jugadores = JsonConvert.DeserializeObject<List<Jugador>>(jugadoresJson);
                

                // Buscar si hay algún jugador con el mismo correo electrónico
                foreach (Jugador jugador in jugadores)
                {
                    if (jugador.email == correo)
                    {
                        print("Concidencia");
                        jugadorPrincipal = jugador;
                        print("Concidencia");
        print("https://cropopoly-server-production.up.railway.app/jugadores/" + jugadorPrincipal.id);
        playerDataUrl = "https://cropopoly-server-production.up.railway.app/jugadores/" + jugadorPrincipal.id;

        

        print(jsonJuego2);

        var request = new UnityWebRequest("https://cropopoly-server-production.up.railway.app/jugadores/" + jugadorPrincipal.id, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonJuego2);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // La solicitud POST fue exitosa
            print("Solicitud POST exitosa");

            // Realizar una solicitud GET para obtener los datos actualizados del jugador
            var getRequest = UnityWebRequest.Get("https://cropopoly-server-production.up.railway.app/jugadores/" + jugadorPrincipal.id);
            yield return getRequest.SendWebRequest();

            if (getRequest.result == UnityWebRequest.Result.Success)
            {
                // La solicitud GET fue exitosa, obtener el JSON de la respuesta
                string jsonPlayerData = getRequest.downloadHandler.text;

                print(jsonPlayerData);

                // Deserializar el JSON en un objeto Jugador
                Jugador jugadorActualizado = JsonConvert.DeserializeObject<Jugador>(jsonPlayerData);

                // Actualizar los datos del jugador principal con los datos obtenidos del GET
                jugadorPrincipal = jugadorActualizado;
        

                
                ultimoJuego = jugadorPrincipal.Juego[jugadorPrincipal.Juego.Count - 1];
                longitud = jugadorPrincipal.Juego.Count-1;
                print("Longitud" + longitud);
                ultimoJuego.tipoFinanciamiento = data.DevolverDif();
                jugadorPrincipal.Juego[jugadorPrincipal.Juego.Count - 1] = ultimoJuego;
                data.SetId(ultimoJuego.id);
                string jsonUpdatedData2 = JsonConvert.SerializeObject(jugadorPrincipal.Juego[jugadorPrincipal.Juego.Count -1] );
                
                 print(jsonUpdatedData2);
                
                
                string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
                
                 print(jsonUpdatedData);

                // Enviar los datos actualizados del jugador al servidor
                StartCoroutine(UpdatePlayerData(jsonUpdatedData));
            }
            else
            {
                // La solicitud GET no fue exitosa
                Debug.LogError("Error al obtener los datos del jugador: " + getRequest.error);
            }
        }
        else
        {
            // La solicitud POST no fue exitosa
            Debug.LogError("Error al actualizar los datos del juego: " + request.error);
        }

        break;
    }
}}}}
    public void ActualizarUltimoJuego(string nuevoValor)
    {   
        ultimoJuego.tipoFinanciamiento = nuevoValor;
        print(ultimoJuego.tipoFinanciamiento);
        jugadorPrincipal.Juego[longitud] = ultimoJuego;

        print("DifUpdate" + jugadorPrincipal.Juego[longitud]);
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
        print(jsonUpdatedData);

        
        StartCoroutine(UpdatePlayerData(jsonUpdatedData));

    }
    public void ActualizarEstaciones(string nuevoValor)
    {   
        ultimoJuego.noEstaciones = nuevoValor;
        jugadorPrincipal.Juego[longitud] = ultimoJuego;
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
        StartCoroutine(UpdatePlayerData(jsonUpdatedData));

    }
    public void ActualizarContratos(string nuevoValor)
    {   
        ultimoJuego.noContratos = nuevoValor;
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
        StartCoroutine(UpdatePlayerData(jsonUpdatedData));

    }
    public void ActualizarBalances(string nuevoValor)
    {   
        ultimoJuego.balance = nuevoValor;
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
        StartCoroutine(UpdatePlayerData(jsonUpdatedData));

    }
    public void ActualizarTrabajador(string nuevoValor)
    {   
        ultimoJuego.qytTrabajador = nuevoValor;
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
        StartCoroutine(UpdatePlayerData(jsonUpdatedData));

    }
    public void ActualizarHerramienta(string nuevoValor)
    {   
        ultimoJuego.qytHerramienta = nuevoValor;
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
        StartCoroutine(UpdatePlayerData(jsonUpdatedData));

    }
    public void ActualizarSemilla(string nuevoValor)
    {   
        ultimoJuego.qytSemilla = nuevoValor;
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
        StartCoroutine(UpdatePlayerData(jsonUpdatedData));

    }
    public void ActualizarAgua(string nuevoValor)
    {   
        ultimoJuego.qytAgua = nuevoValor;
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
        StartCoroutine(UpdatePlayerData(jsonUpdatedData));

    }
    public void ActualizarFertilizante(string nuevoValor)
    {   
        ultimoJuego.qytFertilizante = nuevoValor;
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
        StartCoroutine(UpdatePlayerData(jsonUpdatedData));

    }
    public void ActualizarParcela(string campo, string nuevoValor, int parcela){
        switch (campo)
        {
        case "Trabajador":
            ultimoJuego.Parcela[parcela].qytTrabajadorPar = nuevoValor;
            trabajador += int.Parse(nuevoValor);
            ActualizarTrabajador(trabajador.ToString());
            break;
        case "Herramienta":
            ultimoJuego.Parcela[parcela].qytHerramientaPar = nuevoValor;
            herramienta += int.Parse(nuevoValor);
            ActualizarHerramienta(herramienta.ToString());
            break;
        case "Semilla":
            ultimoJuego.Parcela[parcela].qytSemillaPar = nuevoValor;
            semilla += int.Parse(nuevoValor);
            ActualizarSemilla(semilla.ToString());
            break;
        case "Agua":
            ultimoJuego.Parcela[parcela].qytAguaPar = nuevoValor;
            agua += int.Parse(nuevoValor);
            ActualizarAgua(agua.ToString());
            break;
        case "Fertilizante":
            ultimoJuego.Parcela[parcela].qytFertilizantePar = nuevoValor;
            fertilizante += int.Parse(nuevoValor);
            ActualizarFertilizante(fertilizante.ToString());
            break;
        case "Desbloqueada":
            ultimoJuego.Parcela[parcela].desbloqueada = nuevoValor;
            break;
        case "Productividad":
            ultimoJuego.Parcela[parcela].productividad = nuevoValor;
            break;
        default:
            Debug.LogWarning("Campo no válido: " + campo);
            return;
        }
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);

        StartCoroutine(UpdatePlayerData(jsonUpdatedData));
    }


    IEnumerator UpdatePlayerData(string playerDataJson)
    {
        print(playerDataJson);
        var request = new UnityWebRequest(playerDataUrl, "PUT");
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

    // Clase para deserializar el JSON
    [System.Serializable]
    public class Jugador
    {
        public string id;
        public string nombre;
        public string apellido;
        public string fechaNacimiento;
        public string genero;
        public string estado;
        public string email;
        public List<Juego> Juego;
    }

    [System.Serializable]
    public class Juego
    {
        public string id;
        public string fechaHoraInicio;
        public string fechaHoraFin;
        public string tipoFinanciamiento;
        public string noEstaciones;
        public string noContratos;
        public string balance;
        public string qytTrabajador;
        public string qytHerramienta;
        public string qytSemilla;
        public string qytAgua;
        public string qytFertilizante;
        public List<Parcela> Parcela;
    }

    public class Juego2
    {
        public string id;
        public string fechaHoraInicio;
        public string fechaHoraFin;
        public string tipoFinanciamiento;
        public string noEstaciones;
        public string noContratos;
        public string balance;
        public string qytTrabajador;
        public string qytHerramienta;
        public string qytSemilla;
        public string qytAgua;
        public string qytFertilizante;
    }


    [System.Serializable]
    public class Parcela
    {
        public string id;
        public string numeroParcela;
        public string qytTrabajadorPar;
        public string qytHerramientaPar;
        public string qytSemillaPar;
        public string qytAguaPar;
        public string qytFertilizantePar;
        public string desbloqueada;
        public string productividad;
    }
}
