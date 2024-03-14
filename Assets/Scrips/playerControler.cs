using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float detectionRadius = 1.5f; // Radio de detección del tilemap cultivable
    public Color highlightColor = Color.white; // Color de resaltado

    private Rigidbody2D _rigidbody;
    private Animator _animator;

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
    }
}
