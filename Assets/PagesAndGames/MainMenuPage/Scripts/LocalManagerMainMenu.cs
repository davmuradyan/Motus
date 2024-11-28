using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalManagerMainMenu : MonoBehaviour
{
    [Header("Module1 Scene Names")]
    [SerializeField] private string loginSceneName;

    [Header("Module2 Scene Names")]

    [SerializeField] private string gamesSceneName;
    [SerializeField] private string progressSceneName;

    public void ProgressButtonClickedFromMainMenu()
    {
        StartCoroutine(LoadSceneAsync(progressSceneName));

    }

    public void GamesButtonClickedFromMainMenu()
    {
        StartCoroutine(LoadSceneAsync(gamesSceneName));

    }

    public void LogOutButtonClickedFromMainMenu()
    {
        StartCoroutine(LoadSceneAsync(loginSceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }
}
