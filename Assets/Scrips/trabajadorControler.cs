using UnityEngine;

public class TrabajadorController : MonoBehaviour
{
    // Public variables
    public float speed = 1.5f;
    public bool isTalking;
    public float walkTime = 1.5f;
    public float waitTime = 4.0f;
    private bool isWalking = false;
    private const string LAST_H = "Last_H";
    private const string LAST_V = "Last_V";
    public BoxCollider2D villagerZone;
    private Vector2 lastMovement = Vector2.zero;
    // Private variables
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private float walkCounter;
    private float waitCounter;
    private int currentDirection;
    private int previousDirection;

    private Vector2[] walkingDirections = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        waitCounter = waitTime;
        walkCounter = walkTime;
        isTalking = false;
    }

    void FixedUpdate()
    {
        if (isWalking)
        {
            if (transform.position.x < villagerZone.bounds.min.x ||
                transform.position.x > villagerZone.bounds.max.x ||
                transform.position.y < villagerZone.bounds.min.y ||
                transform.position.y > villagerZone.bounds.max.y)
            {
                StopWalking();
            }
            _rigidbody.velocity = walkingDirections[currentDirection] * speed;
            walkCounter -= Time.fixedDeltaTime;
            if (walkCounter < 0)
            {
                StopWalking();
            }
        }
        else
        {
            _rigidbody.velocity = Vector2.zero;
            waitCounter -= Time.fixedDeltaTime;
            if (waitCounter < 0)
            {
                StartWalking();
            }
        }
    }

    private void LateUpdate()
    {
        _animator.SetBool("Walking", isWalking);
        _animator.SetFloat("Horizontal", walkingDirections[currentDirection].x);
        _animator.SetFloat("Vertical", walkingDirections[currentDirection].y);
        _animator.SetFloat(LAST_H, lastMovement.x);
        _animator.SetFloat(LAST_V, lastMovement.y);
    }

    public void StartWalking()
    {
        int newDirection;
        do
        {
            newDirection = Random.Range(0, walkingDirections.Length);
        } while (newDirection == previousDirection);

        lastMovement = walkingDirections[newDirection];
        currentDirection = newDirection;
        previousDirection = currentDirection;

        isWalking = true;
        walkCounter = walkTime;
    }

    public void StopWalking()
    {
        isWalking = false;
        waitCounter = waitTime;
        _rigidbody.velocity = Vector2.zero;
    }
}
