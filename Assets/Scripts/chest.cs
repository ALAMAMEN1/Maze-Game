using UnityEngine;

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

    // Start is called once before the first execution of Update after the MonoBehavior is created
   void Start()
    {
        isEnter = true; 
        text.SetActive(false);
        key.SetActive(false);
        Debug.Log("Key should be hidden: " + key.activeSelf); // تحقق من حالة المفتاح
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
                keyAnimator.SetBool("play",true);
            }
        }
        
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
