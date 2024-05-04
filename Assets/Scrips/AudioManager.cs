/// <summary>
/// Revisa las preferencias de usuario para saber si el audio esta activado o desactivado
/// y cambia el icono de acuerdo a la preferencia del usuario.
/// Tambien se encarga de activar o desactivar el audio y colocar el ícono correspondiente.
/// </summary>
/// <author>Arturo Barrios Mendoza</author>

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Image MusicOnIcon;
    [SerializeField] Image MusicOffIcon;
    private bool muted = false;
    [SerializeField] private AudioSource musicSource;

    public AudioClip background;

    private void Start() {
        if (!PlayerPrefs.HasKey("muted")) {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        } else {
            Load();
        }
        UpdateButtonIcon();
    }
    public void OnButtonPress() {
        if (muted == false) {
            muted = true;
            //AudioListener.pause = true;
            AudioListener.volume = 0;
        } else {
            muted = false;
            //AudioListener.pause = false;
            AudioListener.volume = 1;
        }
        UpdateButtonIcon();
        Save();
    }
    private void UpdateButtonIcon() {
        if (muted == true) {
            MusicOnIcon.gameObject.SetActive(false);
            MusicOffIcon.gameObject.SetActive(true);
        } else {
            MusicOnIcon.gameObject.SetActive(true);
            MusicOffIcon.gameObject.SetActive(false);
        }
    }
    private void Load() {
        muted = PlayerPrefs.GetInt("muted") == 1;
        AudioListener.volume = muted ? 0 : 1;
    }
    private void Save() {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
