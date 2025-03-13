using UnityEngine;
using System.Collections; // Add this line to include the IEnumerator type

public class Chest : MonoBehaviour
{
    bool isOpen = false;
    public GameObject text;
    public GameObject key;

    public GameObject triggerObject;
    //public AudioSource audioSource; 
    public bool isEnter;
    public Animator animator;
    public Animator keyAnimator;
    public Player player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isEnter = true; 
        text.SetActive(false);
        key.SetActive(false);
        animator.SetBool("isOpen", isOpen);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnter)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //audioSource.Play();
                isEnter = false;
                isOpen = true;
                animator.SetBool("isOpen", isOpen);
                key.SetActive(true);
                keyAnimator.SetBool("play", true);
                StartCoroutine(DeactivateKeyWithDelay());
                player.key = true;
            }
        }
    }

    private IEnumerator DeactivateKeyWithDelay()
    {
        yield return new WaitForSeconds(2f);
        key.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(true);
            isEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            text.SetActive(false);
            isEnter = false;
        }
    }
}