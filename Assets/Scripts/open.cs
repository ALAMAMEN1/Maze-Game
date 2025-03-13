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

    void Start()
    {
        collider = door.GetComponent<BoxCollider2D>();
        text.SetActive(false);
    }

    void Update()
    {
        if(player.key)
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
        yield return new WaitForSeconds(1f);
        doorAnimator.SetBool("open", false);
        collider.isTrigger = true;
        trigger.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(false);
        }
    }
}
