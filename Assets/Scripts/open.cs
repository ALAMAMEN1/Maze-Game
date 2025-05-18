using UnityEngine;
using System.Collections;

public class Open : MonoBehaviour
{
    [Header("References")]
    public GameObject text;
    public GameObject door;
    public GameObject trigger;
    public Animator doorAnimator;
    public PlayerObject player;

    private BoxCollider2D doorCollider;
    private bool isEnter = false;
    private bool isOpening = false;

    void Start()
    {
        doorCollider = door.GetComponent<BoxCollider2D>();

        if (text != null) text.SetActive(false);
        if (trigger != null) trigger.SetActive(true);
        if (doorCollider != null) doorCollider.isTrigger = false;
    }

    void Update()
    {
        if (!isOpening && isEnter && player.key)
        {
            if (InputManager.Instance != null && InputManager.Instance.ConsumeInteract())
            {
                StartCoroutine(OpenDoor());
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        isOpening = true;

        if (doorAnimator != null)
        {
            doorAnimator.SetBool("open", true);
        }

        yield return new WaitForSeconds(0.6f);

        if (doorAnimator != null)
        {
            doorAnimator.SetBool("open", false);
        }

        if (doorCollider != null)
        {
            doorCollider.isTrigger = true;
        }

        if (trigger != null)
        {
            trigger.SetActive(false);
        }

        if (text != null)
        {
            text.SetActive(false);
        }

        isEnter = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isEnter = true;
        if (text != null) text.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        isEnter = false;
        if (text != null) text.SetActive(false);
    }
}
