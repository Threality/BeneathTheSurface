using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public List<string> levels = new List<string>();

    public PlayerState currentPlayerState = PlayerState.RightsideUp;
    public GameState currentGameState = GameState.MainMenu;
    public string activeLevelName = "";

    [Header("References")]
    public BasePlateFlip basePlateFlip;
    public CameraFollow cameraFollow;

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
        LoadLevel("MainMenu");
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
        // MenuManager manager = MenuManager.instance;
        // manager.DisableAll();

        switch (state)
        {
            case GameState.MainMenu:
                currentGameState = GameState.MainMenu;
                LoadLevel("MainMenu");
                break;
            case GameState.Playing:
                currentGameState = GameState.Playing;
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

    public void LoadLevel(string sceneName)
    {
        StartCoroutine(LoadLevelRoutine(sceneName));
    }

    private IEnumerator LoadLevelRoutine(string sceneName)
    {
        if (!string.IsNullOrEmpty(activeLevelName))
        {
            Scene scene = SceneManager.GetSceneByName(activeLevelName);

            if (scene.isLoaded)
                yield return SceneManager.UnloadSceneAsync(scene);
        }

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        activeLevelName = sceneName;

        GameObject spawn = GameObject.FindGameObjectWithTag("Spawnpoint");

        if(sceneName != "MainMenu")
        {
            PlayerContainer.instance.player.transform.position = spawn.transform.position;
            PlayerContainer.instance.player.SetActive(true);
            PlayerContainer.instance.player.GetComponent<Rigidbody2D>().linearVelocity = new Vector3(0, 0, 0);

            ChangeGameState(GameState.Playing);
        }
        else
        {
            PlayerContainer.instance.player.SetActive(false);
        }
        cameraFollow.UpdateBounds();
    }

    public void KillPlayer()
    {
        LoadLevel(activeLevelName);
        MenuManager.instance.AlertUser($"You died");
    }

    public void Win()
    {
        ChangeGameState(GameState.MainMenu);
        MenuManager.instance.AlertUser($"You won level {activeLevelName}");
    }
}