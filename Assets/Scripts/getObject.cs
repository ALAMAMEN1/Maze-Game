using UnityEngine;

public class getObject : MonoBehaviour
{
    public PlayerObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void getObj (int number) {
        player.getObj(number);
    }


}
