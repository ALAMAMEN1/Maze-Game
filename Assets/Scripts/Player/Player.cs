using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour
{
    [Header("Health")]
    public float MAX_HEALTH;
    public float health;

    [Header("Movement")]
    public float speed;
    float speedX, speedY;
    bool facingRight = true;
    public bool stop = false;

    [Header("Dash Settings")]
    public float dashDistance = 0.5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool isDashing = false;
    private bool canDash = true;
    private Vector2 dashDirection;

    [Header("Attack")]
    public GameObject sowrd;
    public GameObject eff;
    public Animator sowrdAnimator;
    public Animator effAnimator;

    [Header("References")]
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D mapBoundsCollider;
    private float playerWidth;
    private float playerHeight;

    [Header("UI")]
    public Joystick joystick;
    public Button dashButton;
    public Button attackButton;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioSource walkAudioSource; 
    public AudioClip walkClip;
    public AudioClip dashClip;
    public AudioClip attackClip;
    public AudioClip backgroundMusic;

    void Start()
    {
        health = MAX_HEALTH;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sowrd.SetActive(false);
        eff.SetActive(false);

        if (dashButton != null)
            dashButton.onClick.AddListener(OnDashButtonPressed);

        if (attackButton != null)
            attackButton.onClick.AddListener(OnAttackButtonPressed);

        mapBoundsCollider = GameObject.FindGameObjectWithTag("MapBounds")?.GetComponent<Collider2D>();
        var playerCollider = GetComponent<Collider2D>();
        playerWidth = playerCollider.bounds.extents.x;
        playerHeight = playerCollider.bounds.extents.y;

        if (audioSource && backgroundMusic)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.volume = 0.1f; 
            audioSource.Play();
        }

        if (walkAudioSource != null)
        {
            walkAudioSource.clip = walkClip;
            walkAudioSource.loop = true;
            walkAudioSource.volume = 0.1f;
        }
    }

    void Update()
    {
        float inputX = Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(joystick.Horizontal)
            ? Input.GetAxis("Horizontal")
            : joystick.Horizontal;

        float inputY = Mathf.Abs(Input.GetAxis("Vertical")) > Mathf.Abs(joystick.Vertical)
            ? Input.GetAxis("Vertical")
            : joystick.Vertical;

        speedX = inputX;
        speedY = inputY;

        bool isMoving = speedX != 0 || speedY != 0;

        animator.SetBool("run", isMoving);

        if (isMoving)
        {
            if (!walkAudioSource.isPlaying)
                walkAudioSource.Play();
        }
        else
        {
            if (walkAudioSource.isPlaying)
                walkAudioSource.Stop();
        }

        if (speedX < 0 && facingRight) Flip();
        else if (speedX > 0 && !facingRight) Flip();

        if (health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        if (!isDashing && !stop)
        {
            Vector2 newVelocity = new Vector2(speedX * speed, speedY * speed);
            rb.linearVelocity = ConstrainVelocityToMapBounds(newVelocity);
        }

        if (Input.GetKeyDown(KeyCode.Space)) OnDashButtonPressed();
        if (Input.GetKeyDown(KeyCode.F)) OnAttackButtonPressed();
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
        if (attackClip && audioSource)
            audioSource.PlayOneShot(attackClip, 1f);

        sowrd.SetActive(true);
        eff.SetActive(true);
        sowrdAnimator.SetBool("play", true);
        effAnimator.SetBool("play", true);
        StartCoroutine(DeactivateAttackAfterDelay());
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;

        if (dashClip && audioSource)
            audioSource.PlayOneShot(dashClip, 0.6f);

        dashDirection = new Vector2(speedX, speedY).normalized;
        Vector2 dashVelocity = dashDirection * (dashDistance / dashDuration);
        rb.linearVelocity = ConstrainVelocityToMapBounds(dashVelocity);

        animator.SetTrigger("dash");

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private IEnumerator DeactivateAttackAfterDelay()
    {
        yield return new WaitForSeconds(0.2f);
        sowrd.SetActive(false);
        eff.SetActive(false);
    }

    private Vector2 ConstrainVelocityToMapBounds(Vector2 velocity)
    {
        if (mapBoundsCollider == null)
            return velocity;

        Vector3 newPos = transform.position + (Vector3)velocity * Time.deltaTime;
        Bounds bounds = mapBoundsCollider.bounds;
        newPos.x = Mathf.Clamp(newPos.x, bounds.min.x + playerWidth, bounds.max.x - playerWidth);
        newPos.y = Mathf.Clamp(newPos.y, bounds.min.y + playerHeight, bounds.max.y - playerHeight);
        return (newPos - transform.position) / Time.deltaTime;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
