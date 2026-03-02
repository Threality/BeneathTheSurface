using UnityEngine;

public class LevelSelectionManager : MonoBehaviour
{
    public GameObject levelSlot;

    void Start()
    {
        for (int i = 0; i < GameManager.instance.levels.Count ; i++)
        {
            GameObject slot = Instantiate(levelSlot, transform);
            slot.GetComponent<MenuSlot>().Initialise(GameManager.instance.levels[i]);
        }
    }
}
