using UnityEngine;
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

    public float dashDistance = 0.5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private bool canDash = true;
    private Vector2 dashDirection;

    public GameObject sowrd;
    public GameObject eff;
    public Animator sowrdAnimator;
    public Animator effAnimator;

    // For boundary checking
    private Collider2D mapBoundsCollider;
    private float playerWidth;
    private float playerHeight;

    void Start()
    {
        health = MAX_HEALTH;
        key = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sowrd.SetActive(false);
        eff.SetActive(false);

        // Initialize boundary checking
        mapBoundsCollider = GameObject.FindGameObjectWithTag("MapBounds").GetComponent<Collider2D>();
        var playerCollider = GetComponent<Collider2D>();
        playerWidth = playerCollider.bounds.extents.x;
        playerHeight = playerCollider.bounds.extents.y;
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

        // Dash input
        if (Input.GetKeyDown(KeyCode.Space) && canDash && (speedX != 0 || speedY != 0))
        {
            StartCoroutine(Dash());
        }

        if (!isDashing && !stop)
        {
            Vector2 newVelocity = new Vector2(speedX * speed, speedY * speed);
            rb.linearVelocity = ConstrainVelocityToMapBounds(newVelocity);
        }

        if(Input.GetKeyDown(KeyCode.F)) {
            sowrd.SetActive(true);
            eff.SetActive(true);
            sowrdAnimator.SetBool("play", true);
            effAnimator.SetBool("play", true);
            StartCoroutine(DeactivateAttWithDelay());
        }
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        
        // Store the dash direction based on input
        dashDirection = new Vector2(speedX, speedY).normalized;
        
        // Apply dash force
        float dashSpeed = dashDistance / dashDuration;
        Vector2 dashVelocity = dashDirection * dashSpeed;
        rb.linearVelocity = ConstrainVelocityToMapBounds(dashVelocity);
        
        // Optional: Add visual effects for dash
        animator.SetTrigger("dash");
        
        yield return new WaitForSeconds(dashDuration);
        
        isDashing = false;
        rb.linearVelocity = Vector2.zero; // Reset velocity after dash
        
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    // New method to constrain movement within map bounds
    private Vector2 ConstrainVelocityToMapBounds(Vector2 velocity)
    {
        if (mapBoundsCollider == null)
            return velocity;

        Vector3 newPosition = transform.position + (Vector3)velocity * Time.deltaTime;
        
        // Get the bounds of the map collider
        Bounds bounds = mapBoundsCollider.bounds;
        
        // Clamp the new position within map bounds (considering player size)
        newPosition.x = Mathf.Clamp(
            newPosition.x, 
            bounds.min.x + playerWidth, 
            bounds.max.x - playerWidth
        );
        
        newPosition.y = Mathf.Clamp(
            newPosition.y, 
            bounds.min.y + playerHeight, 
            bounds.max.y - playerHeight
        );
        
        // Return the constrained velocity
        return (newPosition - transform.position) / Time.deltaTime;
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
}