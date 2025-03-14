using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float MAX_HEALTH;
    public float health;
    public bool key = false;
    public float speed;
    float speedX, speedY;
    Rigidbody2D rb;
    Animator animator;
    public bool stop = false;
    bool facingRight = true;

    void Start()
    {
        health = MAX_HEALTH;
        key = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        speedX = Input.GetAxis("Horizontal");
        speedY = Input.GetAxis("Vertical");

        // التحكم في animation الجري
        if (speedX != 0 || speedY != 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        // قلب اتجاه اللاعب
        if (speedX < 0 && facingRight)
        {
            Flip();
        }
        else if (speedX > 0 && !facingRight)
        {
            Flip();
        }

        // إعادة تحميل المشهد إذا انتهت حياة اللاعب
        if (health <= 0) // استخدام <= بدلاً من ==
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // تحريك اللاعب إذا لم يكن متوقفًا
        if (!stop)
        {
            rb.linearVelocity = new Vector2(speedX * speed, speedY * speed);
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