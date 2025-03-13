using System.Threading.Tasks;
using System;
using System.Globalization;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    public GameObject isTrigger;
 
    //public AudioSource audioSource; 
    public bool isEnter;

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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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


}