using UnityEngine;

public class Enemy : MonoBehaviour
{

    public GameObject player;

    public float speed;


    private float distance; 

    // Start is called once before the first execution of Update after the MonoBehavior is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position , player.transform.position);
        Vector2 direction =  player.transform.position - transform.position;

        direction = direction.normalized;

        transform.position = Vector2.MoveTowards(this.transform.position , player.transform.position, speed * Time.deltaTime);


    }
}
