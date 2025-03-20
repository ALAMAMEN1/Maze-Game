using UnityEngine;

public class Knife : MonoBehaviour
{
    public GameObject player;
    public GameObject goblin;
    public float speed;
    public bool startAtt;

    bool isEnter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isEnter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(startAtt) {
            transform.position = Vector3.MoveTowards(this.transform.position , player.transform.position, speed * Time.deltaTime);
        }
        if(isEnter) {
            gameObject.SetActive(false);
            transform.position = goblin.transform.position;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") {
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") {
            isEnter = false;
            gameObject.SetActive(true);
        }
    }
}
