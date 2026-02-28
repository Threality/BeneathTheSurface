using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public int groundCount = 0;
    public int baseplateCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")|| collision.CompareTag("Baseplate") || collision.CompareTag("Object"))
        {
            groundCount++;
        }
        if (collision.CompareTag("Baseplate"))
        {
            baseplateCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground") || collision.CompareTag("Baseplate") || collision.CompareTag("Object"))
        {
            groundCount--;
        }
        if (collision.CompareTag("Baseplate"))
        {
            baseplateCount--;
        }
    }

    public bool IsGrounded()
    {
        return groundCount > 0;
    }

    public bool IsOnBaseplate()
    {
        return baseplateCount > 0;
    }
}
