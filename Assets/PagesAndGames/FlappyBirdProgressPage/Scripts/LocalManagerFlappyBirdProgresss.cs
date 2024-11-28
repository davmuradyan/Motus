using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalManagerFlappyBirdProgress : MonoBehaviour
{
    [Header("Module2 Scene Names")]
    [SerializeField] private string gameProgressHistorySceneName;

    public void GoBackButtonClickedFromFlappyBirdHistory()
    {
        StartCoroutine(LoadSceneAsync(gameProgressHistorySceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }
}
