using UnityEngine;

public class PuddleDelay : MonoBehaviour
{
    private float puddleCooldown;

    private void Update()
    {
        puddleCooldown = puddleCooldown < 0 ? puddleCooldown : puddleCooldown - Time.deltaTime;
    }

    public bool CanPuddle()
    {
        return puddleCooldown < 0;
    }

    public void Puddle()
    {
        puddleCooldown = 0.5f;
    }
}
