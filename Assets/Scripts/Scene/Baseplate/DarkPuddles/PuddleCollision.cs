using UnityEngine;

public class PuddleCollision : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Collided with {collision.tag}");
        PuddleDelay delay = collision.GetComponent<PuddleDelay>();
        if (!delay) return;

        if (collision.CompareTag("Object") && delay.CanPuddle())
        {
            GameManager.instance.FlipObject(collision.gameObject);
            delay.Puddle();
        }
        else if (collision.CompareTag("Player") && delay.CanPuddle())
        {
            GameManager.instance.ChangePlayerState(GameManager.instance.GetPlayerState());
            delay.Puddle();
        }
    }
}
