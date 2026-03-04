using UnityEngine;

public class BasePlateFlip : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Animator playerAnimator;

    public void EnterRightsideUp()
    {
        PlayerMovement movement = PlayerContainer.instance.GetPlayerMovement();
        movement.isFlipped = false;

        Rigidbody2D rb = movement.GetComponent<Rigidbody2D>();
        rb.position = new Vector2(rb.position.x, Mathf.Abs(rb.position.y));
        
        PlayerContainer.instance.groundCheck.localScale = new Vector3(1, 1, 1);

        cameraFollow.isFlipped = false;

        playerAnimator.SetTrigger("goToUp");
    }

    public void EnterUpsideDown()
    {
        PlayerMovement movement = PlayerContainer.instance.GetPlayerMovement();
        movement.isFlipped = true;

        Rigidbody2D rb = movement.GetComponent<Rigidbody2D>();
        rb.position = new Vector2(rb.position.x, -Mathf.Abs(rb.position.y));
        PlayerContainer.instance.groundCheck.localScale = new Vector3(1, -1, 1);

        cameraFollow.isFlipped = true;

        playerAnimator.SetTrigger("goToDown");
    }

    public void ObjectToRightsideUp(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.position = new Vector2(rb.position.x, Mathf.Abs(rb.position.y));
        rb.gravityScale = 1f;
    }

    public void ObjectToUpsideDown(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        rb.position = new Vector2(rb.position.x, -Mathf.Abs(rb.position.y));
        rb.gravityScale = -1f;
    }
}