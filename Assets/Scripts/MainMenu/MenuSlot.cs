using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MenuSlot : MonoBehaviour
{
    public string slotID;
    public TextMeshProUGUI textMesh;

    public void Initialise(string id)
    {
        slotID = id;
        textMesh.text = slotID;
    }

    public void LoadLevel()
    {
        GameManager.instance.LoadLevel(slotID);
    }
}