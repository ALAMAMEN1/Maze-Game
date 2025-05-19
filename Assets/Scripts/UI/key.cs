using UnityEngine;

public class key : MonoBehaviour
{
    public PlayerObject player;
    public GameObject keyImage;
    public GameObject lastPieceImage;

    void Start()
    {
        if (keyImage != null)
            keyImage.SetActive(false);

        if (lastPieceImage != null)
            lastPieceImage.SetActive(false);

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.GetComponent<PlayerObject>();
            }
        }
    }

    void Update()
    {
        if (player == null)
            return;

        if (keyImage != null)
            keyImage.SetActive(player.key);

        if (lastPieceImage != null)
            lastPieceImage.SetActive(player.lastPiece);
    }
}
