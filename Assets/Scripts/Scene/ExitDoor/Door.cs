using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isOpen)
        {
            GameManager.instance.Win();
        }
    }
}
