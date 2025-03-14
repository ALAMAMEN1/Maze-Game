using UnityEngine;
using System.Collections;

public class Open : MonoBehaviour
{
    public GameObject text;
    public GameObject door;
    public GameObject trigger;    
    public Animator doorAnimator;
    public Player player;
    
    BoxCollider2D collider;
    bool isEnter;

    void Start()
    {
        isEnter = false;
        collider = door.GetComponent<BoxCollider2D>();
        text.SetActive(false);
    }

    void Update()
    {
        if(player.key && isEnter)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(OpenDoor());
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        doorAnimator.SetBool("open", true);
        yield return new WaitForSeconds(0.6f);
        doorAnimator.SetBool("open", false);
        collider.isTrigger = true;
        trigger.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = false;
            text.SetActive(false);
        }
    }
}
