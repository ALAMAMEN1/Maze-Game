using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour
{
    bool isOpen = false;
    public GameObject text;
    public GameObject key;

    public GameObject triggerObject;
    public AudioSource audioSource;       
    public bool isEnter = false;
    public Animator animator;
    public Animator keyAnimator;
    public Player player;

    void Start()
    {
        text.SetActive(false);
        key.SetActive(false);
        animator.SetBool("isOpen", isOpen);
    }

    void Update()
    {
        if (isEnter && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        if (!isOpen)
        {
            isOpen = true;
            animator.SetBool("isOpen", isOpen);

            if (audioSource != null)
            {
                audioSource.Play();
            }

            key.SetActive(true);
            keyAnimator.SetBool("play", true);
            StartCoroutine(DeactivateKeyWithDelay());

            player.key = true; 
            isEnter = false;
        }
    }

    private IEnumerator DeactivateKeyWithDelay()
    {
        yield return new WaitForSeconds(2f);
        keyAnimator.SetBool("play", false);
        key.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.CompareTag("Player"))
        {
            isEnter = true;
            text.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = false;
            text.SetActive(false);
        }
    }
}