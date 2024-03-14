using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float detectionRadius = 1.5f; // Radio de detección del tilemap cultivable
    public Color highlightColor = Color.white; // Color de resaltado

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private TilemapRenderer highlightedTilemapRenderer; // TilemapRenderer del tile resaltado

    private const string AXIS_H = "Horizontal";
    private const string AXIS_V = "Vertical";
    private const string WALK = "Walking";
    private const string LAST_H = "LastH";
    private const string LAST_V = "LastV";
    private const string MOV = "Movement";

    private Vector2 lastMovement = Vector2.zero;
    private bool attacking = false;
    private float attackTime = 1f;
    private float attackTimeCounter;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxisRaw(AXIS_H);
        float verticalInput = Input.GetAxisRaw(AXIS_V);

        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * speed;

        _rigidbody.velocity = movement;

        bool walking = Mathf.Abs(horizontalInput) > 0.2f || Mathf.Abs(verticalInput) > 0.2f;

        if (walking)
        {
            lastMovement = new Vector2(horizontalInput, verticalInput);
        }

        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter < 0)
            {
                attacking = false;
                _animator.SetBool(MOV, false);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _rigidbody.velocity = Vector2.zero;
                attacking = true;
                attackTimeCounter = attackTime;
                _rigidbody.velocity = Vector2.zero;
                _animator.SetBool(MOV, true);
            }
        }

        _animator.SetFloat(AXIS_H, horizontalInput);
        _animator.SetFloat(AXIS_V, verticalInput);
        _animator.SetBool(WALK, walking);
        _animator.SetFloat(LAST_H, lastMovement.x);
        _animator.SetFloat(LAST_V, lastMovement.y);

        // Detectar el tilemap del cultivo que el jugador está mirando
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastMovement, detectionRadius, LayerMask.GetMask("Default"));
        if (hit.collider != null)
        {
            TilemapRenderer hitTilemapRenderer = hit.collider.GetComponent<TilemapRenderer>();
            if (hitTilemapRenderer != null && hit.collider.CompareTag("Tile"))
            {
                print("Detectado");
                // Resaltar el tilemap detectado
                HighlightTilemap(hitTilemapRenderer);
            }
        }
        else
        {
            print("no Detectado");
            // Si no hay ningún tilemap en la dirección del jugador, eliminar cualquier resaltado existente
            ClearHighlightedTilemap();
        }
    }

    void HighlightTilemap(TilemapRenderer tilemapRenderer)
    {
        // Eliminar el resaltado del tilemap previamente resaltado (si hay alguno)
        ClearHighlightedTilemap();

        // Resaltar el nuevo tilemap
        highlightedTilemapRenderer = tilemapRenderer;
        Material[] materials = highlightedTilemapRenderer.materials;
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = highlightColor;
        }
        highlightedTilemapRenderer.materials = materials;
    }

    void ClearHighlightedTilemap()
    {
        // Si hay un tilemap resaltado, eliminar su resaltado
        if (highlightedTilemapRenderer != null)
        {
            Material[] materials = highlightedTilemapRenderer.materials;
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].color = Color.white; // Restaurar el color original del material
            }
            highlightedTilemapRenderer.materials = materials;
            highlightedTilemapRenderer = null;
        }
    }
}
