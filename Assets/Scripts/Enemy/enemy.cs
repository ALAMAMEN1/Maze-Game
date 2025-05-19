using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject KnifeObject;
    public float speed;
    public float stopDistance;
    public float minDistance;
    public Knife Knife;

    public AudioSource backgroundAudioSource;

    private float distance;
    private Animator animator;
    private bool facingRight = true;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (KnifeObject != null)
            KnifeObject.SetActive(false);

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null)
            return;

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = (player.transform.position - transform.position).normalized;

        if (distance > stopDistance && distance < minDistance)
        {
            if (KnifeObject != null)
                KnifeObject.SetActive(true);

            Vector3 targetPosition = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            targetPosition.z = transform.position.z;
            transform.position = targetPosition;

            animator.SetBool("play", true);
            if (Knife != null)
                Knife.startAtt = true;
        }
        else
        {
            if (Knife != null)
                Knife.startAtt = false;

            animator.SetBool("play", false);
        }

        if (direction.x < 0 && facingRight)
        {
            Flip();
        }
        else if (direction.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    void OnBecameVisible()
    {
        if (backgroundAudioSource != null && !backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource.loop = true;
            backgroundAudioSource.Play();
        }
    }

    void OnBecameInvisible()
    {
        if (backgroundAudioSource != null && backgroundAudioSource.isPlaying)
        {
            backgroundAudioSource.Stop();
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
