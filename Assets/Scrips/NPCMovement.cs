/// <summary>
/// Clase que se encarga de mover a los NPC de forma aleatoria.
/// </summary>
/// <author>Fidel Alexander Bonilla Montalvo</author>

using UnityEngine;
using UnityEngine.Tilemaps;

public class NPCMovement : MonoBehaviour
{
    public float speed = 2f;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool isAnimating = false;
    private bool isWalking = false;
    private float animationTimer = 0f;
    private float walkTimer = 0f;
    public float animationTime = 3f;
    public float walkTime = 1.5f;
    public float waitTime = 4.0f;
    private int currentDirection;
    private Vector2[] walkingDirections = {
        Vector2.up, Vector2.down, Vector2.left, Vector2.right
    };

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        // Start the initial waiting period
        ResetTimers();
    }

    void Update()
    {
        // If not walking or animating, decrease wait time
        if (!isWalking && !isAnimating)
        {
            waitTime -= Time.deltaTime;
            if (waitTime <= 0)
            {
                // If wait time is over, choose randomly between animation and walking
                float random = Random.value;
                if (random < 0.5f)
                {
                    StartWalking();
                }
                else
                {
                    _rigidbody.velocity = Vector2.zero;
                    StartAnimating();
                }
            }
        }

        // If walking, decrease walk timer
        if (isWalking)
        { 
            walkTimer -= Time.deltaTime;
            if (walkTimer <= 0)
            {

                StopWalking();
            }
            else
            {
                // Move in the current direction
                _rigidbody.velocity = walkingDirections[currentDirection] * speed;
            }
        }

        // If animating, decrease animation timer
        if (isAnimating)
        {
            animationTimer -= Time.deltaTime;
            if (animationTimer <= 0)
            {
                _rigidbody.velocity = Vector2.zero;
                StopAnimating();
            }
        }
    }
    private void LateUpdate()
    {
        _animator.SetBool("Walking", isWalking);
        _animator.SetFloat("Horizontal", walkingDirections[currentDirection].x);
        _animator.SetFloat("Vertical", walkingDirections[currentDirection].y);
        _animator.SetBool("Animacion", isAnimating);
    }
    // Start walking
    void StartWalking()
    {
        currentDirection = Random.Range(0, walkingDirections.Length);
        isWalking = true;
        walkTimer = walkTime;
        isAnimating = false;
        _animator.SetBool("Walking", true);
        _animator.SetBool("Animacion", false);
        ResetTimers();
    }

    // Stop walking
    void StopWalking()
    {
        isWalking = false;
        _animator.SetBool("Walking", false);
        _rigidbody.velocity = Vector2.zero;
        ResetTimers();
    }

    // Start animating
    void StartAnimating()
    {
        currentDirection = Random.Range(0, walkingDirections.Length);
        isAnimating = true;
        animationTimer = animationTime;
        isWalking = false;
        _animator.SetBool("Animacion", true);
        _animator.SetBool("Walking", false);
        ResetTimers();
    }

    // Stop animating
    void StopAnimating()
    {
        isAnimating = false;
        _animator.SetBool("Animacion", false);
        ResetTimers();
    }

    // Reset timers
    void ResetTimers()
    {
        waitTime = 4.0f;
        animationTime = Random.Range(2f, 3f);
        walkTime = Random.Range(1f, 3f);
    }
    
}
