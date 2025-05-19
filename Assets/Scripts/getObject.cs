using UnityEngine;

public class getObject : MonoBehaviour
{
    public PlayerObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                {
                    player = playerObj.GetComponent<PlayerObject>();
                }
        }
    }


    public void getObj(int number)
    {
        player.getObj(number);
    }


}
