using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
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

    private Collider2D mapBoundsCollider;
    private float playerWidth;
    private float playerHeight;

    public Joystick joystick;
    public Button dashButton;
    public Button attackButton;

    void Start()
    {
        health = MAX_HEALTH;
        key = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sowrd.SetActive(false);
        eff.SetActive(false);

        if (dashButton != null)
            dashButton.onClick.AddListener(OnDashButtonPressed);

        if (attackButton != null)
            attackButton.onClick.AddListener(OnAttackButtonPressed);

        mapBoundsCollider = GameObject.FindGameObjectWithTag("MapBounds").GetComponent<Collider2D>();
        var playerCollider = GetComponent<Collider2D>();
        playerWidth = playerCollider.bounds.extents.x;
        playerHeight = playerCollider.bounds.extents.y;
    }

    void Update()
    {
        float inputX = joystick != null ? joystick.Horizontal : 0;
        float inputY = joystick != null ? joystick.Vertical : 0;

        if (Mathf.Abs(inputX) < 0.1f)
            inputX = Input.GetAxis("Horizontal");

        if (Mathf.Abs(inputY) < 0.1f)
            inputY = Input.GetAxis("Vertical");

        speedX = inputX;
        speedY = inputY;

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

        if (!isDashing && !stop)
        {
            Vector2 newVelocity = new Vector2(speedX * speed, speedY * speed);
            rb.linearVelocity = ConstrainVelocityToMapBounds(newVelocity);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnDashButtonPressed();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnAttackButtonPressed();
        }
    }

    public void OnDashButtonPressed()
    {
        if (canDash && (speedX != 0 || speedY != 0))
        {
            StartCoroutine(Dash());
        }
    }

    public void OnAttackButtonPressed()
    {
        sowrd.SetActive(true);
        eff.SetActive(true);
        sowrdAnimator.SetBool("play", true);
        effAnimator.SetBool("play", true);
        StartCoroutine(DeactivateAttWithDelay());
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        dashDirection = new Vector2(speedX, speedY).normalized;

        float dashSpeed = dashDistance / dashDuration;
        Vector2 dashVelocity = dashDirection * dashSpeed;
        rb.linearVelocity = ConstrainVelocityToMapBounds(dashVelocity);

        animator.SetTrigger("dash");

        yield return new WaitForSeconds(dashDuration);

        isDashing = false;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private Vector2 ConstrainVelocityToMapBounds(Vector2 velocity)
    {
        if (mapBoundsCollider == null)
            return velocity;

        Vector3 newPosition = transform.position + (Vector3)velocity * Time.deltaTime;

        Bounds bounds = mapBoundsCollider.bounds;

        newPosition.x = Mathf.Clamp(newPosition.x, bounds.min.x + playerWidth, bounds.max.x - playerWidth);
        newPosition.y = Mathf.Clamp(newPosition.y, bounds.min.y + playerHeight, bounds.max.y - playerHeight);

        return (newPosition - transform.position) / Time.deltaTime;
    }

    private IEnumerator DeactivateAttWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        sowrd.SetActive(false);
        eff.SetActive(false);
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
