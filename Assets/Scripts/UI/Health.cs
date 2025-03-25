 // وش فادت هذا السكربت راني مداير الهلث في player.cs

using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Player player;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = player.MAX_HEALTH;
        slider.value = player.MAX_HEALTH;
    }
    
    void Update()
    {
        slider.value = player.health;
    }
}