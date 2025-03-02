using UnityEngine;

public class player : MonoBehaviour
{
    public float speed;
    float speedX, speedY;
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedX = Input.GetAxis("Horizontal");
        speedY = Input.GetAxis("Vertical");
        rb.linearVelocity = new Vector2(speedX * speed, speedY * speed);
    }
}
