using UnityEngine;

public class win : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collided with {collision.tag}");
        if (collision.CompareTag("Player")) GameManager.instance.Win();
    }
}
