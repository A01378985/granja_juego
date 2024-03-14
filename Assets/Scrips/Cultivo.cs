using UnityEngine;

public class Cultivo : MonoBehaviour
{
    public Sprite[] stagesOfGrowth; // Array de sprites que representan las diferentes etapas de crecimiento
    private SpriteRenderer spriteRenderer;
    private int currentStage = 0; // Índice del sprite actual de crecimiento
    private float timeSinceLastGrowth = 0f;
    private float growthInterval = 2f; // Intervalo de crecimiento en segundos
    private bool fullyGrown = false;
    private bool shouldGrow = false; // Indica si el cultivo debe crecer o no

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // Desactiva el SpriteRenderer al inicio
        if (stagesOfGrowth.Length > 0)
        {
            spriteRenderer.sprite = stagesOfGrowth[currentStage];
        }
    }

    void Update()
    {
        if (shouldGrow && !fullyGrown)
        {
            timeSinceLastGrowth += Time.deltaTime;
            if (timeSinceLastGrowth >= growthInterval)
            {
                Grow();
                timeSinceLastGrowth = 0f;
            }
        }
    }

    public void StartGrowth()
    {
        shouldGrow = true;
        spriteRenderer.enabled = true; // Activa el SpriteRenderer cuando el cultivo comienza a crecer
    }

    void Grow()
    {
        currentStage++;
        if (currentStage < stagesOfGrowth.Length)
        {
            spriteRenderer.sprite = stagesOfGrowth[currentStage];
        }
        else
        {
            // Ya no hay más etapas de crecimiento
            fullyGrown = true;
            spriteRenderer.sprite = null; // Desaparece el sprite
            Debug.Log("El cultivo ha alcanzado su etapa de crecimiento final.");
        }
    }
}
