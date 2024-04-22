using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Parcela : MonoBehaviour
{
    public bool activeParcel { get; private set; }
    public int productivity { get; private set; }
    public int water { get; private set; }
    public int fertilizer { get; private set; }
    public int tool { get; private set; }
    public int worker { get; private set; }
    public int extraProductivity { get; private set; }

    [SerializeField]
    private SpriteRenderer spriteRendererNormal;

    [SerializeField]
    private SpriteRenderer spriteRendererActive;

    [SerializeField]
    private GameObject plantas_1;

    [SerializeField]
    private GameObject plantas_2;

    [SerializeField]
    private GameObject plantas_3;

    [SerializeField]
    private GameObject plantas_4;

    public bool unlocked;
    public bool ruined { get; private set; }

    [SerializeField]
    private GameObject letreroDesbloquear;

    [SerializeField]
    private GameObject noSuficientes;

    [SerializeField]
    private GameObject[] humBars;

    [SerializeField]
    private GameObject[] trabajadores;

    [SerializeField]
    private AudioSource sonidoDesbloquear;
    private void Start()
    {
        activeParcel = false;
        ruined = false;
        productivity = 0;
        water = 0;
        fertilizer = 0;
        tool = 0;
        worker = 0;
        extraProductivity = 0;
        ruined = false;
        spriteRendererNormal.enabled = true;
        spriteRendererActive.enabled = false;
        plantas_1.SetActive(false);
        plantas_2.SetActive(false);
        plantas_3.SetActive(false);
        plantas_4.SetActive(false);
        sonidoDesbloquear = GameObject.Find("SonidoDesbloquear").GetComponent<AudioSource>();
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
                ruined = false;
                GameObject.Find("BarManager").GetComponent<BarManager>().IncNumParcelas();
                letreroDesbloquear.SetActive(false);
                GameObject.Find("CardManager").GetComponent<CardManager>().IncNumCrops();
                GameObject.Find("ItemManager").GetComponent<ItemManager>().IncUnlockedParcels();
                tool = 1;
                worker = 2;
                productivity = 30;
                MostrarTodosTrabajadores();
                EnablePlants();
                sonidoDesbloquear.Play();
                GameObject.Find("BarManager").GetComponent<BarManager>().CountProd();
                if (GameObject.Find("BarManager").GetComponent<BarManager>().sequia) {
                    water = 3;
                } else if (GameObject.Find("BarManager").GetComponent<BarManager>().lluvia) {
                    water = 0;
                } else {
                    water = 2;
                }
                MostrarBarras();
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
            } else {
                MostrarBarras();
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
            foreach (GameObject humBar in humBars) {
                humBar.SetActive(false);
            }
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
    public void MostrarBarras() {
        bool sequia = GameObject.Find("BarManager").GetComponent<BarManager>().sequia;
        bool lluvia = GameObject.Find("BarManager").GetComponent<BarManager>().lluvia;
        foreach (GameObject humBar in humBars) {
            humBar.SetActive(false);
        }
        if (sequia) {
            if (water == 0) {
                humBars[0].SetActive(true);
            } else if (water == 1) {
                humBars[1].SetActive(true);
            } else if (water == 2) {
                humBars[2].SetActive(true);
            } else if (water == 3) {
                humBars[3].SetActive(true);
            } else if (water == 4) {
                humBars[4].SetActive(true);
            } else if (water == 5) {
                humBars[5].SetActive(true);
            } else if (water == 6) {
                humBars[6].SetActive(true);
            } else if (water == 7) {
                humBars[7].SetActive(true);
            } else if (water == 8) {
                humBars[11].SetActive(true);
            } else if (water >= 9) {
                humBars[12].SetActive(true);
            }
        } else if (lluvia) {
            if (water == 0) {
                humBars[6].SetActive(true);
            } else if (water == 1) {
                humBars[10].SetActive(true);
            } else if (water ==2) {
                humBars[8].SetActive(true);
            }  else if (water >=3) {
                humBars[9].SetActive(true);
            }
        } else if (!sequia && !lluvia) {
            if (water == 0) {
                humBars[1].SetActive(true);
            } else if (water == 1) {
                humBars[2].SetActive(true);
            } else if (water == 2) {
                humBars[3].SetActive(true);
            } else if (water == 3) {
                humBars[4].SetActive(true);
            } else if (water == 4) {
                humBars[5].SetActive(true);
            } else if (water == 5) {
                humBars[6].SetActive(true);
            } else if (water == 6) {
                humBars[10].SetActive(true);
            } else if (water == 7) {
                humBars[8].SetActive(true);
            } else if (water >= 8) {
                humBars[9].SetActive(true);
            }
        }
    }
    public void MostrarTrabajador() {
        if (!trabajadores[0].activeSelf) {
            trabajadores[0].SetActive(true);
        } else if (!trabajadores[1].activeSelf) {
            trabajadores[1].SetActive(true);
        }
    }
    public void QuitarUnTrabajador() {
        if (trabajadores[0].activeSelf) {
            trabajadores[0].SetActive(false);
        } else if (trabajadores[1].activeSelf) {
            trabajadores[1].SetActive(false);
        }
    }
    public void QuitarTodosTrabajadores() {
        trabajadores[0].SetActive(false);
        trabajadores[1].SetActive(false);
    }
    public void MostrarTodosTrabajadores() {
        trabajadores[0].SetActive(true);
        trabajadores[1].SetActive(true);
    }
    public void LockThisParcel () {
        unlocked = false;
    }
    public void RuinThisParcel() {
        ruined = true;
    }
    public void ResetParcelValues() {
        productivity = 0;
        water = 0;
        fertilizer = 0;
        tool = 0;
        worker = 0;
        extraProductivity = 0;
    }
    public void IncParcelFertilizer() {
        fertilizer++;
    }
    public void IncParcelProductivityByTen() {
        productivity += 10;
    }
    public void DecParcelProductivityByTen() {
        productivity -= 10;
    }
    public void IncParcelWater() {
        water++;
    }
    public void IncParcelTool() {
        tool++;
    }
    public void IncParcelWorker() {
        worker++;
    }
    public void NotRuinThisParcel() {
        ruined = false;
    }
    public void IncExtraProductivity() {
        extraProductivity++;
    }
    public void DecParcelWorker() {
        worker--;
    }
    public void ResetParcelWater() {
        water = 0;
    }
}
