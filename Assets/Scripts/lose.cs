using System.Numerics;
using System.Threading.Tasks;
using System;
using System.Globalization;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class Lose : MonoBehaviour
{
    public BoxCollider2D boxCollider2D;
    public GameObject isTrigger;
    //public AudioSource audioSource; 
    public bool isEnter;
    public Rigidbody2D rigdbody2D;

    public SpriteRenderer spriteRender;
    public Player player;

    // Start is called once before the first execution of Update after the MonoBehavior is created
    void Start()
{
    isEnter = false;
    isTrigger.SetActive(true);

    if (player == null)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<Player>();
        }
    }

    StartCoroutine(noLose());
}


    // Update is called once per frame
    void Update(){
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trigger");
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
            isTrigger.SetActive(false);
        }
    }

    private void loseHandel(){
        if (isEnter)
        {
            rigdbody2D.linearVelocity = new UnityEngine.Vector2(-0.2f, 0);
            rigdbody2D.linearVelocity = new UnityEngine.Vector2(-0.2f, 0);
            player.health -= 20;
            isEnter = false;
            StartCoroutine(StopMove(1f));
        }
    }



    private IEnumerator StopMove(float time)
    {
        
            player.stop = true;
            Debug.Log("Player Stopped!");            
            yield return new WaitForSeconds(time);
            spriteRender.color = new Color(1f, 1f, 1f);
            player.stop = false;        
    }

    private IEnumerator noLose(){
        boxCollider2D.enabled = false;
        yield return new WaitForSeconds(0.2f);
        boxCollider2D.enabled = true;
        loseHandel();
        StartCoroutine(noLose());
    }



}
