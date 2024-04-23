using UnityEngine;
using UnityEngine.Networking;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

public class dataReader : MonoBehaviour
{
    private string serverUrl = "http://localhost:8000/jugadores";
    private string resultUrl = "http://localhost:8000/result";
    private string playerDataUrl;
    private string correo;
    Juego juego = new Juego();
    private string jsonJuego;
    string newGameID = IDGenerator.GenerateID();
    Juego ultimoJuego;
    Jugador jugadorPrincipal;
    private int trabajador = 0;
    private int herramienta = 0;
    private int semilla = 0;
    private int agua = 0;
    private int fertilizante = 0;
    

    private void Start()
    {
        string fechaHoraInicio = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        StartCoroutine(FetchData(fechaHoraInicio));
        Juego juego = new Juego
        {
        id=newGameID,
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

    for (int i = 1; i <= 5; i++)
    {
        Parcela parcela = new Parcela
        {
            id = i.ToString(),
            numeroParcela = "1",
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
    Debug.Log("Datos del juego: " + jsonJuego);
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
            Debug.Log("Received data from result URL: " + resultJson);

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

                List<Jugador> jugadores = JsonConvert.DeserializeObject<List<Jugador>>(jugadoresJson);
                

                // Buscar si hay algún jugador con el mismo correo electrónico
                foreach (Jugador jugador in jugadores)
                {
                    if (jugador.email == correo)
                    {
                        jugadorPrincipal = jugador;
                        print(juego);
                        Juego objetoJuego = JsonConvert.DeserializeObject<Juego>(jsonJuego);
                        jugadorPrincipal.Juego.Add(objetoJuego);
                        ultimoJuego = jugadorPrincipal.Juego[jugador.Juego.Count - 1];
                        print("http://localhost:8000/jugadores/"+jugadorPrincipal.id);
                        playerDataUrl= "http://localhost:8000/jugadores/"+jugadorPrincipal.id;

                        // Serializar los datos actualizados
                        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);


                        // Enviar los datos actualizados al servidor
                        StartCoroutine(UpdatePlayerData(jsonUpdatedData));
                        
                        break;
                    }
                }
            }
        }
    }
    public void ActualizarUltimoJuego(string nuevoValor)
    {   
        ultimoJuego.tipoFinanciamiento = nuevoValor;
        string jsonUpdatedData = JsonConvert.SerializeObject(jugadorPrincipal);
        StartCoroutine(UpdatePlayerData(jsonUpdatedData));

    }
    public void ActualizarEstaciones(string nuevoValor)
    {   
        ultimoJuego.noEstaciones = nuevoValor;
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
