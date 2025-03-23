using UnityEngine;
using UnityEngine.UI; 
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour
{
    public float MAX_HEALTH;
    public float health;
    public Text healthText; 
    public bool key = false;
    public float speed;
    float speedX, speedY;
    Rigidbody2D rb;
    Animator animator;
    public bool stop = false;
    bool facingRight = true;

    public GameObject sowrd;
    public GameObject eff;
    public Animator sowrdAnimator;
    public Animator effAnimator;

    void Start()
    {
        health = MAX_HEALTH;
        key = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sowrd.SetActive(false);
        eff.SetActive(false);
        UpdateHealthDisplay(); 
    }

    
    public void TakeDamage(float damage)
    {
        health -= damage;
        health = Mathf.Clamp(health, 0, MAX_HEALTH);
        UpdateHealthDisplay();

        if (health <= 0)
        {
            Die();
        }
    }

    void UpdateHealthDisplay()
    {
        healthText.text = "Health: " + Mathf.Round(health).ToString();
    }

    void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        speedX = Input.GetAxis("Horizontal");
        speedY = Input.GetAxis("Vertical");

        animator.SetBool("run", (speedX != 0 || speedY != 0));

        if (speedX < 0 && facingRight)
        {
            Flip();
        }
        else if (speedX > 0 && !facingRight)
        {
            Flip();
        }

        
        if (!stop)
        {
            rb.linearVelocity = new Vector2(speedX * speed, speedY * speed);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            sowrd.SetActive(true);
            eff.SetActive(true);
            sowrdAnimator.SetBool("play", true);
            effAnimator.SetBool("play", true);
            StartCoroutine(DeactivateAttWithDelay());
        }
    }

    IEnumerator DeactivateAttWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        sowrd.SetActive(false);
        eff.SetActive(false);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}

//code origine in case (khrebha aboud) 
/*using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


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


    public GameObject sowrd;

    public GameObject eff;
    public Animator sowrdAnimator;


    public Animator effAnimator;

    void Start()
    {
        health = MAX_HEALTH;
        key = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sowrd.SetActive(false);
        eff.SetActive(false);
    }

    void Update()
    {
        speedX = Input.GetAxis("Horizontal");
        speedY = Input.GetAxis("Vertical");

        if (speedX != 0 || speedY != 0)
        {
            animator.SetBool("run", true);
        }
        else
        {
            animator.SetBool("run", false);
        }

        if (speedX < 0 && facingRight)
        {
            Flip();
        }
        else if (speedX > 0 && !facingRight)
        {
            Flip();
        }

        if (health <= 0) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (!stop)
        {
            rb.linearVelocity = new Vector2(speedX * speed, speedY * speed);
        }


        if(Input.GetKeyDown(KeyCode.F)) {
            sowrd.SetActive(true);
            eff.SetActive(true);
            sowrdAnimator.SetBool("play" , true);
            effAnimator.SetBool("play" , true);
            StartCoroutine(DeactivateAttWithDelay());
        }

              
    }


    private IEnumerator DeactivateAttWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        sowrd.SetActive(false);
        eff.SetActive(false);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    
}*/