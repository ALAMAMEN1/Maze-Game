using UnityEngine;

public class Knife : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject goblin;
    
    [Header("Settings")]
    public float speed = 10f;
    public bool startAtt = false;

    [Header("Collision Settings")]
    public LayerMask collisionLayers;

    private Vector3 movementDirection;
    private bool isMoving = false;
    private bool hasCollided = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void Update()
    {
        HandleMovement();
        RotateKnife();
    }

    void HandleMovement()
    {
        if (startAtt && !isMoving && !hasCollided)
        {
            InitializeAttack();
        }

        if (isMoving && !hasCollided)
        {
            MoveKnife();
        }
        else if (hasCollided)
        {
            ResetKnifePosition();
        }
    }

    void InitializeAttack()
    {
        movementDirection = (player.transform.position - transform.position).normalized;
        isMoving = true;
        startAtt = false;
    }

    void MoveKnife()
    {
        rb.linearVelocity = movementDirection * speed;
    }

    void RotateKnife()
    {
        if (isMoving)
        {
            float angle = Mathf.Atan2(movementDirection.y, movementDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void ResetKnifePosition()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = goblin.transform.position;
        transform.rotation = Quaternion.identity;
        isMoving = false;
        hasCollided = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("wall"))
        {
            HandleCollision();
        }
    }

    

    void HandleCollision()
    {
        hasCollided = true;
        
    }
}