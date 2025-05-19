using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Player player;
    private Slider slider;

    void Start()
{
    slider = GetComponent<Slider>();

    if (player == null)
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.GetComponent<Player>();
        }
    }

    if (player != null)
    {
        slider.maxValue = player.MAX_HEALTH;
        slider.value = player.MAX_HEALTH;
    }
}

    
    void Update()
    {
        slider.value = player.health;
    }
}