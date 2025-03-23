using UnityEngine;

public class getObject : MonoBehaviour
{
    public PlayerObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void getObj (int number) {
        if(number == 1) {
            player.key = true;
        } else if(number == 2) {
            player.lastPiece  = true;
        }
    }


}
