using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    void Start()
    {
        if (SceneManager.GetSceneByName("Core").isLoaded)
        {
            // SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene("Core", LoadSceneMode.Additive);
            // SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        }
    }
}
