using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalManagerGameProgress : MonoBehaviour
{
    [Header("Module2 Scene Names")]
    [SerializeField] private string progressSceneName;
    [SerializeField] private string flappyBirdProgressSceneName;

    public void FlappyBirdButtonClickedFromGamePointHistory()
    {
        StartCoroutine(LoadSceneAsync(flappyBirdProgressSceneName));

    }

    public void GoBackButtonClickedFromGamePointHistory()
    {
        StartCoroutine(LoadSceneAsync(progressSceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }

}
