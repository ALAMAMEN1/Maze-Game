using UnityEngine;

public class slime : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float stopDistance;
    public float minDistance;
    private float distance;
    private Animator animator;
    bool facingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            Debug.LogError("Player is not assigned in the Enemy script.");
            return;
        }

        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction = direction.normalized;

        if (distance > stopDistance && distance < minDistance) 
        {
            // حفظ قيمة z الأصلية
            float originalZ = transform.position.z;
            
            // تحريك الكائن على المحورين X و Y فقط
            transform.position = Vector2.MoveTowards(
                new Vector2(transform.position.x, transform.position.y),
                new Vector2(player.transform.position.x, player.transform.position.y),
                speed * Time.deltaTime
            );
            
            transform.position = new Vector3(transform.position.x, transform.position.y, originalZ);
            
            animator.SetBool("play", true);
        }
        else if (distance < stopDistance) 
        {
            animator.SetBool("play", false);
        }
        else
        {
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

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}