using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Parcela : MonoBehaviour
{
    // Crear una variable de tipo bool para parcela activa
    public bool activeParcel;
    // Crear una variable de tipo int para productividad
    public int productivity;
    // Crear una variable de tipo int para agua
    public int water;
    // Crear una variable de tipo int para fertilizante
    public int fertilizer;
    // Crear una variable de tipo int para herramienta
    public int tool;
    // Crear una variable de tipo int para trabajador
    public int worker;
    public SpriteRenderer spriteRendererNormal;
    public SpriteRenderer spriteRendererActive;
    public GameObject plantas_1;
    public GameObject plantas_2;
    public GameObject plantas_3;
    public GameObject plantas_4;
    // Variable para parcela desbloqueada
    public bool unlocked;
    public GameObject letreroDesbloquear;
    public GameObject noSuficientes;
    // Detectar la colisión con el jugador
    private void Start()
    {
        activeParcel = false;
        productivity = 0;
        water = 0;
        fertilizer = 0;
        tool = 0;
        worker = 0;
        spriteRendererNormal.enabled = true;
        spriteRendererActive.enabled = false;
        plantas_1.SetActive(false);
        plantas_2.SetActive(false);
        plantas_3.SetActive(false);
        plantas_4.SetActive(false);
    }
    private void Update() {
    if (activeParcel && !unlocked && Input.GetKeyDown(KeyCode.E)) {
        int fichasTrabajador = GameObject.Find("ItemManager").GetComponent<ItemManager>().worker;
        int fichasAgua = GameObject.Find("ItemManager").GetComponent<ItemManager>().irrigation;
        int fichasHerramienta = GameObject.Find("ItemManager").GetComponent<ItemManager>().tool;
        int fichasSemilla = GameObject.Find("ItemManager").GetComponent<ItemManager>().seed;
        if (GameObject.Find("ItemManager").GetComponent<ItemManager>().CheckThree()) {
            GameObject.Find("ItemManager").GetComponent<ItemManager>().RestarTres();
            unlocked = true;
            GameObject.Find("BarManager").GetComponent<BarManager>().numParcelas++;
            letreroDesbloquear.SetActive(false);
            GameObject.Find("CardManager").GetComponent<CardManager>().numCrops++;
            GameObject.Find("ItemManager").GetComponent<ItemManager>().unlockedParcels++;
            water = 2;
            tool = 1;
            worker = 2;
            productivity = 30;
            EnablePlants();
            GameObject.Find("BarManager").GetComponent<BarManager>().numParcelas++;
            GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
        } else {
            letreroDesbloquear.SetActive(false);
            noSuficientes.SetActive(true);
        }
    }
}
    // Detectar la colisión con el jugador
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            activeParcel = true;
            spriteRendererNormal.enabled = false;
            spriteRendererActive.enabled = true;
            if (!unlocked) {
                letreroDesbloquear.SetActive(true);
            }
        }
    }
    // Detectar la salida de la colisión con el jugador
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            activeParcel = false;
            spriteRendererNormal.enabled = true;
            spriteRendererActive.enabled = false;
            letreroDesbloquear.SetActive(false);
            noSuficientes.SetActive(false);
        }
    }
    // Habilitar GameObjects de acuerdo con la productividad de la parcela
    public void EnablePlants() {
        if (productivity < 20) {
            plantas_1.SetActive(false);
            plantas_2.SetActive(false);
            plantas_3.SetActive(false);
            plantas_4.SetActive(false);
        } else if (productivity >= 20 && productivity < 40) {
            plantas_1.SetActive(true);
            plantas_2.SetActive(false);
            plantas_3.SetActive(false);
            plantas_4.SetActive(false);
        } else if (productivity >= 40 && productivity < 60) {
            plantas_1.SetActive(false);
            plantas_2.SetActive(true);
            plantas_3.SetActive(false);
            plantas_4.SetActive(false);
        } else if (productivity >= 60 && productivity < 80) {
            plantas_1.SetActive(false);
            plantas_2.SetActive(false);
            plantas_3.SetActive(true);
            plantas_4.SetActive(false);
        } else if (productivity >= 80) {
            plantas_1.SetActive(false);
            plantas_2.SetActive(false);
            plantas_3.SetActive(false);
            plantas_4.SetActive(true);
        }
    }
}
