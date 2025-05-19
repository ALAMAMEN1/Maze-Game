using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Chest : MonoBehaviour
{
    [Header("References")]
    public GameObject text;
    public GameObject key;
    public GameObject triggerObject;
    public getObject getObject;
    public Animator animator;
    public Animator keyAnimator;
    public AudioSource audioSource;

    [Header("Audio")]
    public AudioClip openChestSound;

    [Header("Settings")]
    public int numberOfItem;

    private bool isOpen = false;
    private bool isEnter = false;

    void Start()
    {
        text.SetActive(false);
        key.SetActive(false);
        animator.SetBool("isOpen", isOpen);
    }

    void Update()
    {
        if (!isOpen && isEnter)
        {
            if (InputManager.Instance != null && InputManager.Instance.ConsumeInteract(isEnter))
            {
                OpenChest();
            }
        }
    }

    private void OpenChest()
    {
        Debug.Log("OpenChest CALLED");
        if (isOpen) return;

        isOpen = true;
        animator.SetBool("isOpen", isOpen);

        if (audioSource != null && openChestSound != null)
        {
            audioSource.PlayOneShot(openChestSound, 1.0f); // 1.0f تعني الحجم الكامل
        }

        key.SetActive(true);
        if (keyAnimator != null)
        {
            keyAnimator.SetBool("play", true);
        }

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

    private IEnumerator DeactivateKeyWithDelay()
    {
        yield return new WaitForSeconds(2f);

        if (keyAnimator != null)
        {
            keyAnimator.SetBool("play", false);
        }

        key.SetActive(false);
        triggerObject.SetActive(false);
        isOpen = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = true;
            if (text != null)
                text.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = false;
            if (text != null)
                text.SetActive(false);
        }
    }
}
