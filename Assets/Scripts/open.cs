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

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip openSound;

    private BoxCollider2D doorCollider;
    private bool isEnter = false;
    private bool isOpening = false;

    void Start()
    {
        doorCollider = door.GetComponent<BoxCollider2D>();

        if (text != null) text.SetActive(false);
        if (trigger != null) trigger.SetActive(true);
        if (doorCollider != null) doorCollider.isTrigger = false;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerObject>();
            }
        }

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isOpening && isEnter && player.key)
        {
            if (InputManager.Instance != null && InputManager.Instance.ConsumeInteract(isEnter))
            {
                StartCoroutine(OpenDoor());
            }
        }
    }

    private IEnumerator OpenDoor()
    {
        isOpening = true;

        if (audioSource != null && openSound != null)
        {
            audioSource.PlayOneShot(openSound, 0.7f);
        }

        if (doorAnimator != null)
        {
            doorAnimator.SetBool("open", true);

            
            yield return null; 
            AnimatorStateInfo stateInfo = doorAnimator.GetCurrentAnimatorStateInfo(0);
            float animationLength = stateInfo.length;
            yield return new WaitForSeconds(animationLength);

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
