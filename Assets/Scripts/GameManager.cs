using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.InputSystem.LowLevel;
using Unity.Multiplayer.PlayMode;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private PlayerState currentPlayerState = PlayerState.RightsideUp;

    [Header("References")]
    public BasePlateFlip basePlateFlip;

    public enum PlayerState
    {
        RightsideUp,
        UpsideDown,
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(transform.parent);
    }

    public void ChangePlayerState(PlayerState state)
    {
        switch (state)
        {
            case PlayerState.UpsideDown:
                currentPlayerState = PlayerState.RightsideUp;
                basePlateFlip.EnterRightsideUp();
                break;
            case PlayerState.RightsideUp:
                currentPlayerState = PlayerState.UpsideDown;
                basePlateFlip.EnterUpsideDown();
                break;
        }
    }

    public PlayerState GetPlayerState()
    {
        return currentPlayerState;
    }

    public void LoadLevel(int id)
    {
        // Load a level
    }
}