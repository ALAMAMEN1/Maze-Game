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
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnter)
        {
            rigdbody2D.linearVelocity = new UnityEngine.Vector2(-0.2f, 0);
            spriteRender.color = new Color(1f, 0.325f, 0.325f);
            player.health -= 2;
            isEnter = false;
            StartCoroutine(StopMove(1f));
        }
       
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
            isTrigger.SetActive(false);
        }
    }




    public IEnumerator StopMove(float time)
    {
        
            player.stop = true;
            Debug.Log("Player Stopped!");            
            yield return new WaitForSeconds(time);
            spriteRender.color = new Color(1f, 1f, 1f);
            player.stop = false;        
    }


}
