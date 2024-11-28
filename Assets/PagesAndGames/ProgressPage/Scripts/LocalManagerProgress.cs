using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalManagerProgress : MonoBehaviour
{
    [Header("Module2 Scene Names")]
    [SerializeField] private string gameProgressHistorySceneName;
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string handProgressHistorySceneName;

    public void GameProgressButtonClickedFromProgress()
    {
        StartCoroutine(LoadSceneAsync(gameProgressHistorySceneName));

    }

    public void HandProgressButtonClickedFromProgress()
    {
        StartCoroutine(LoadSceneAsync(handProgressHistorySceneName));

    }
    public void GoBackButtonClickedFromProgress()
    {
        StartCoroutine(LoadSceneAsync(mainMenuSceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }

}
