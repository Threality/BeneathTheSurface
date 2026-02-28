using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class MenuSlot : MonoBehaviour
{
    public int slotID;
    public TextMeshProUGUI textMesh;

    public void Initialise(int id)
    {
        slotID = id;
        textMesh.text = slotID.ToShortString();
    }

    public void LoadLevel()
    {
        GameManager.instance.LoadLevel(slotID);
    }
}