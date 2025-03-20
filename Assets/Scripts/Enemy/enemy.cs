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
	bool facingRight = true;

    // Start is called once before the first execution of Update after the MonoBehavior is created
    void Start()
    {
        KnifeObject.SetActive(false);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position , player.transform.position);
        Vector2 direction =  player.transform.position - transform.position;

        direction = direction.normalized;
        if(distance > stopDistance && distance < minDistance) {
            KnifeObject.SetActive(true);
            transform.position = Vector2.MoveTowards(this.transform.position , player.transform.position, speed * Time.deltaTime);
            animator.SetBool("play" , true);
            Knife.startAtt = true;
        }else if(distance < stopDistance) {
            Knife.startAtt = false;
			animator.SetBool("play" , false);
        }



		if(direction.x < 0 && facingRight) {
			Flip();
		} else{
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
