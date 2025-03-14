using UnityEngine;

public class Kill : MonoBehaviour
{


    public GameObject enemy;

    public GameObject trigger;

    bool isEnter;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isEnter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnter) {
            enemy.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy"){
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Enemy"){
            isEnter = false;
        }
    }
}
