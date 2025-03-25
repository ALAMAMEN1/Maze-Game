using UnityEngine;

public class key : MonoBehaviour
{
    public PlayerObject player;
    public GameObject keyImage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        keyImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.key)
        {
            keyImage.SetActive(true);
        }
        
    }
}
