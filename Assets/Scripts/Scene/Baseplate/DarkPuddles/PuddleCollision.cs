using UnityEngine;

public class PuddleCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collided with {collision.tag}");
        if (collision.CompareTag("Object"))
        {
            GameManager.instance.FlipObject(collision.gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            GameManager.instance.ChangePlayerState(GameManager.instance.GetPlayerState());
        }
    }
}
