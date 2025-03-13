using UnityEngine;

public class Player : MonoBehaviour
{
    public bool key = false;
    public float speed;
    float speedX, speedY;
    Rigidbody2D rb;
    Animator animator;
    bool facingRight = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        key = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxis("Horizontal");
        speedY = Input.GetAxis("Vertical");
        if(speedX != 0 || speedY != 0){
            animator.SetBool("run", true);
        } else {
            animator.SetBool("run", false);
        }

        if(speedX < 0 && facingRight){
            Flip();
        } else if(speedX > 0 && !facingRight){
            Flip();
        }
        rb.linearVelocity = new Vector2(speedX * speed, speedY * speed);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}