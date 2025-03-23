using System.Threading.Tasks;
using System;
using System.Globalization;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enter : MonoBehaviour
{
    public GameObject isTrigger;

    //public AudioSource audioSource; 
    public bool isEnter;

    // Start is called once before the first execution of Update after the MonoBehavior is created
    void Start()
    {
        isTrigger.SetActive(true);
        //audioSource = GetComponent<AudioSource>();
        isEnter = false; 
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnter)
        {
           
                 
                isTrigger.SetActive(false);
                //audioSource.Play();
                isEnter = false;

                int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
                if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(nextSceneIndex);
                }
                else
                {
                    Debug.Log("No more scenes to load.");
                }
            
        }
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