using UnityEngine;
using System.Collections;

public class TicTacToe : MonoBehaviour
{
    [Header("References")]
    public GameObject lastPiece;
    public GameObject Key;
    public Animator animator;
    public PlayerObject player;

    private bool isEnter = false;

    void Start()
    {
        Key.SetActive(false);
        lastPiece.SetActive(false);

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerObject>();
            }
        }
    }

    void Update()
    {
        if (isEnter && player.lastPiece && InputManager.Instance != null && InputManager.Instance.ConsumeInteract(isEnter))
        {
            lastPiece.SetActive(true);
            Key.SetActive(true);
            animator.SetBool("play", true);
            player.getObj(1);
            StartCoroutine(DeactivateKeyWithDelay());
            isEnter = false;
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
