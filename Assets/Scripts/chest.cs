using UnityEngine;
using System.Collections;

public class Chest : MonoBehaviour
{
    bool isOpen = false;
    public GameObject text;
    public GameObject key;

    public GameObject triggerObject;
    public AudioSource audioSource; // أزل التعليق إذا كنت تريد استخدام الصوت
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
        if (!isOpen) // التأكد من أن الصندوق لم يتم فتحه بالفعل
        {
            isOpen = true;
            animator.SetBool("isOpen", isOpen);

            // تشغيل الصوت (إذا كان audioSource معينًا)
            if (audioSource != null)
            {
                audioSource.Play();
            }

            key.SetActive(true);
            keyAnimator.SetBool("play", true);
            StartCoroutine(DeactivateKeyWithDelay());

            player.key = true; // إعطاء المفتاح للاعب
            isEnter = false;
        }
    }

    private IEnumerator DeactivateKeyWithDelay()
    {
        yield return new WaitForSeconds(2f);
        key.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
        if (other.gameObject.tag == "Player")
        {
            isEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isEnter = false;
        }
    }
}