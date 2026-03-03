using UnityEngine;

public class DoorButton : ButtonReceiver
{
    public Door door;

    public override void SetState(bool state)
    {
        door.isOpen = state;
        Debug.Log("Door is open");
    }
}