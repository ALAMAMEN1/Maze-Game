using UnityEngine;

public class PlayerObject : MonoBehaviour
{

    public bool lastPiece;
    public bool key;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        key = false;
        lastPiece = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void getObj(int number)
    {
        if (number == 1)
        {
            key = true;
        }
        else if (number == 2)
        {
            lastPiece = true;
        }
    }
}
