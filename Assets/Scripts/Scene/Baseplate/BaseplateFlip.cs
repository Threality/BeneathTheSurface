using UnityEngine;

public class BasePlateFlip : MonoBehaviour
{
    public CameraFollow cameraFollow;

    public void EnterRightsideUp()
    {
        PlayerMovement movement = PlayerContainer.instance.GetPlayerMovement();
        movement.isFlipped = false;

        Rigidbody2D rb = movement.GetComponent<Rigidbody2D>();
        rb.position = new Vector2(rb.position.x, Mathf.Abs(rb.position.y));
        
        PlayerContainer.instance.groundCheck.localScale = new Vector3(1, 1, 1);

        cameraFollow.isFlipped = false;
    }

    public void EnterUpsideDown()
    {
        PlayerMovement movement = PlayerContainer.instance.GetPlayerMovement();
        movement.isFlipped = true;

        Rigidbody2D rb = movement.GetComponent<Rigidbody2D>();
        rb.position = new Vector2(rb.position.x, -Mathf.Abs(rb.position.y));
        PlayerContainer.instance.groundCheck.localScale = new Vector3(1, -1, 1);

        cameraFollow.isFlipped = true;
    }
}