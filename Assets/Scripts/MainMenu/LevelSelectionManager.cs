using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class LevelSelectionManager : MonoBehaviour
{
    public GameObject levelSlot;

    void Start()
    {
        for (int i = 0; i < 24; i++)
        {
            GameObject slot = Instantiate(levelSlot, transform);
            slot.GetComponent<MenuSlot>().Initialise(i + 1);
        }
    }
}
