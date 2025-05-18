using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject KnifeObject;
    public float speed;
    public float stopDistance;
    public float minDistance;
    public Knife Knife;

    private float distance;
    private Animator animator;
    private bool facingRight = true;

    void Start()
    {
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

        // حركة العدو نحو اللاعب مع الحفاظ على قيمة Z الأصلية
        if (distance > stopDistance && distance < minDistance)
        {
            if (KnifeObject != null)
                KnifeObject.SetActive(true);

            Vector3 targetPosition = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            targetPosition.z = transform.position.z; // تثبيت الـ Z
            transform.position = targetPosition;

            animator.SetBool("play", true);
            if (Knife != null)
                Knife.startAtt = true;
        }
        else if (distance <= stopDistance)
        {
            if (Knife != null)
                Knife.startAtt = false;

            animator.SetBool("play", false);
        }
        else
        {
            animator.SetBool("play", false);
        }

        // التقليب حسب اتجاه الحركة
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
