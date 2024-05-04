/// <summary>
/// Clase que se encarga de cargar escenas y salir del juego.
/// </summary>
/// <author>Arturo Barrios Mendoza</author>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
