using UnityEngine;
using System.Collections;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

    [Header("References")]
    // public GameObject pauseUI;
    public GameObject alertUI;
    public TextMeshProUGUI alertText;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void DisableAll()
    {
        // pauseUI.SetActive(false);
    }

    public void EnterPauseUI()
    {
        // pauseUI.SetActive(true);
    }

    public void AlertUser(string text)
    {
        alertText.text = text;
        StopAllCoroutines();
        StartCoroutine(ShowAlert());
    }

    private IEnumerator ShowAlert()
    {
        alertUI.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        alertUI.SetActive(false);
    }
}