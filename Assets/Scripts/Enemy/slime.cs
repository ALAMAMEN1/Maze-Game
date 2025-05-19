using UnityEngine;

public class slime : MonoBehaviour
{
    [Header("References")]
    public GameObject player;

    [Header("Movement Settings")]
    public float speed;
    public float stopDistance;
    public float minDistance;

    [Header("Audio")]
    public AudioClip walkSound;
    [Range(0f, 1f)] public float walkVolume = 0.3f;

    private float distance;
    private Animator animator;
    private AudioSource audioSource;

    private bool facingRight = true;
    private bool isMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null)
            {
                Debug.LogError("Player object with tag 'Player' not found in the scene.");
            }
        }

        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.clip = walkSound;
            audioSource.volume = walkVolume;
        }
    }

    void Update()
    {
        if (player == null) return;

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        if (distance > stopDistance && distance < minDistance)
        {
            MoveTowardsPlayer();
            HandleAnimation(true);
            HandleWalkSound(true);
        }
        else
        {
            HandleAnimation(false);
            HandleWalkSound(false);
        }

        if ((direction.x < 0 && facingRight) || (direction.x > 0 && !facingRight))
        {
            Flip();
        }
    }

    void MoveTowardsPlayer()
    {
        float originalZ = transform.position.z;

        transform.position = Vector2.MoveTowards(
            new Vector2(transform.position.x, transform.position.y),
            new Vector2(player.transform.position.x, player.transform.position.y),
            speed * Time.deltaTime
        );

        transform.position = new Vector3(transform.position.x, transform.position.y, originalZ);
    }

    void HandleAnimation(bool moving)
    {
        if (animator != null)
        {
            animator.SetBool("play", moving);
        }
    }

    void HandleWalkSound(bool moving)
    {
        if (audioSource == null || walkSound == null) return;

        if (moving && !isMoving)
        {
            isMoving = true;
            audioSource.Play();
        }
        else if (!moving && isMoving)
        {
            isMoving = false;
            audioSource.Stop();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
