using NUnit.Framework.Constraints;
using UnityEngine;

public abstract class ButtonReceiver : MonoBehaviour
{
    
    public abstract void SetState(bool state);
}