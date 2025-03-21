using UnityEngine;
using System.Collections;

public class TicTacToe : MonoBehaviour
{
    [Header("References")]
    public GameObject lastPiece;
    public GameObject Key;
    public Animator animator;
    public PlayerObject player;

    bool isEnter = false;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Key.SetActive(false);
        lastPiece.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnter && player.lastPiece)
        {

            lastPiece.SetActive(true);
            isEnter = false;
            Key.SetActive(true);
            animator.SetBool("play", true);
            StartCoroutine(DeactivateKeyWithDelay());

        }
    }

    private IEnumerator DeactivateKeyWithDelay()
    {
        yield return new WaitForSeconds(2f);
        Key.SetActive(false);
        animator.SetBool("play", false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isEnter = false;
        }
    }
}
