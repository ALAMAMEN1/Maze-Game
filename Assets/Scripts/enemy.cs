using UnityEngine;
using System.Collections;
public class Enemy : MonoBehaviour
{

    public GameObject player;

    public float speed;
    public float stopDistance;
    public float minDistance;
    private float distance;
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehavior is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position , player.transform.position);
        Vector2 direction =  player.transform.position - transform.position;

        direction = direction.normalized;

        if(distance > stopDistance && distance < minDistance) {
            transform.position = Vector2.MoveTowards(this.transform.position , player.transform.position, speed * Time.deltaTime);
            animator.SetBool("play" , false);
        }
        if(distance< stopDistance) {
            Delay();
        }
    }
    private IEnumerator Delay(){
        animator.SetBool("play",true);
        yield return new WaitForSeconds(0.15f);
        animator.SetBool("play",false);
    }

}
