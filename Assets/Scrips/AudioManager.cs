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
        UpdateButtonIcon();
    }
    public void OnButtonPress() {
        if (muted == false) {
            muted = true;
            AudioListener.pause = true;
        } else {
            muted = false;
            AudioListener.pause = false;
        }
        UpdateButtonIcon();
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
}
