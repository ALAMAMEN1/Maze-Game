using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour
{
    bool isOpen = false;
    public GameObject text;
    public GameObject key;
    public GameObject triggerObject;
    public getObject getObject;
    public AudioSource audioSource;       
    public bool isEnter = false;
    public Animator animator;
    public Animator keyAnimator;
    public int numberOfItem;

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
        Debug.Log("OpenChest called");
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

            if (text != null)
            {
                text.SetActive(false);
            }
            

            if (getObject != null)
            {
                getObject.getObj(numberOfItem);
            }
            

            StartCoroutine(DeactivateKeyWithDelay());

            isEnter = false;
        }
        else
        {
        }
    }

    private IEnumerator DeactivateKeyWithDelay()
    {
        yield return new WaitForSeconds(2f);

        if (keyAnimator == null)
        {
            yield break; // أوقف التنفيذ
        }

        keyAnimator.SetBool("play", false);
        key.SetActive(false);
        triggerObject.SetActive(false);
        isOpen = true;

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