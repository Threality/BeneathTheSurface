using System.Collections.Generic;
using UnityEngine;

public class InGameButton : MonoBehaviour
{
    public Sprite notPushedSprite;
    public Sprite pushedSprite;
    public SpriteRenderer spriteRenderer;
    public List<ButtonReceiver> buttonReceivers = new List<ButtonReceiver>();

    private int pressedCount = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object") || collision.CompareTag("Player"))
        {
            if (pressedCount == 0)
            {
                SetButtonState(true);
            }
            pressedCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object") || collision.CompareTag("Player"))
        {
            if (pressedCount == 1)
            {
                SetButtonState(false);
            }
            pressedCount--;
        }
    }

    private void SetButtonState(bool state)
    {
        foreach (ButtonReceiver button in buttonReceivers)
        {
            button.SetState(state);
        }

        if (state == true)
        {
            spriteRenderer.sprite = pushedSprite;
        }
        else
        {
            spriteRenderer.sprite = notPushedSprite;
        }
    }
}
