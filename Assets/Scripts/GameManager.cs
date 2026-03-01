using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.InputSystem.LowLevel;
using Unity.Multiplayer.PlayMode;
using Unity.VisualScripting;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<GameObject> levels = new List<GameObject>();

    private PlayerState currentPlayerState = PlayerState.RightsideUp;
    private GameState currentGameState = GameState.MainMenu;
    private GameObject activeLevel;
    private int activeLevelID = 1;

    [Header("References")]
    public BasePlateFlip basePlateFlip;

    public enum PlayerState
    {
        RightsideUp,
        UpsideDown,
    }

    public enum GameState
    {
        Playing,
        MainMenu,
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(transform.parent);

        ChangeGameState(GameState.MainMenu);
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

    public void ChangeGameState(GameState state)
    {
        MenuManager manager = MenuManager.instance;
        manager.DisableAll();
        

        switch (state)
        {
            case GameState.MainMenu:
                currentGameState = GameState.MainMenu;
                manager.EnterMainMenu();
                break;
            case GameState.Playing:
                currentGameState = GameState.Playing;
                manager.EnterGameplay();
                break;
        }
    }

    public void FlipObject(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody2D>().gravityScale < 0)
        {
            basePlateFlip.ObjectToRightsideUp(obj);
        }
        basePlateFlip.ObjectToUpsideDown(obj);
    }

    public PlayerState GetPlayerState()
    {
        return currentPlayerState;
    }

    public void LoadLevel(int id)
    {
        FullyRemoveLevel();
        GameObject level = Instantiate(levels[id]);
        level.SetActive(true);
        activeLevel = level;
        activeLevelID = id;
        PlayerContainer.instance.player.transform.position = new Vector2(0, 4);
        ChangeGameState(GameState.Playing);
    }

    public void KillPlayer()
    {
        if (activeLevel) Destroy(activeLevel);
        LoadLevel(activeLevelID);
    }
    
    public void DeleteLevels()
    {
        if (activeLevel) Destroy(activeLevel);
    }

    public void FullyRemoveLevel()
    {
        if (activeLevel) Destroy(activeLevel);
        activeLevelID = 1;
    }

    public void Win()
    {
        ChangeGameState(GameState.MainMenu);
        MenuManager.instance.AlertUser($"You won level {activeLevelID}");
        FullyRemoveLevel();
    }
}