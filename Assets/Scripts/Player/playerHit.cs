using UnityEngine;
using System.Collections;
public class playerHit : MonoBehaviour
{
    private bool isEnter;
    [Header("References")]
    public SpriteRenderer spriteRender;
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isEnter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnter) {
            spriteRender.color = new Color(1f, 0.325f, 0.325f);
            isEnter = false;
            StartCoroutine(StopTime(1f));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) {
            isEnter = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) {
            isEnter = false;
        }
    }


    public IEnumerator StopTime(float time)
    {
            player.health -= 5;
            yield return new WaitForSeconds(time);
            spriteRender.color = new Color(1f, 1f, 1f);
    }
}
