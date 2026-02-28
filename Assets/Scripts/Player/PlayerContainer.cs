using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    public static PlayerContainer instance;

    public GameObject player;
    public Transform groundCheck;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public PlayerMovement GetPlayerMovement()
    {
        return player.GetComponent<PlayerMovement>();
    }
}