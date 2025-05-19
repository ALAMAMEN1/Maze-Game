using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class enter : MonoBehaviour
{
    public GameObject isTrigger;
    public Animator transitionAnimator;
    public float transitionTime = 0.3f;
    public CanvasGroup transitionCanvasGroup;

    private bool isEnter;

    void Start()
    {
        isTrigger.SetActive(true);
        isEnter = false;

        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.blocksRaycasts = false;
        }
    }

    void Update()
    {
        if (isEnter)
        {
            isEnter = false;
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        if (transitionCanvasGroup != null)
        {
            transitionCanvasGroup.blocksRaycasts = true;
        }

        if (transitionAnimator != null)
        {
            transitionAnimator.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
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
