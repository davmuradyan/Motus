using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalManagerGames : MonoBehaviour
{
    [Header("Module2 Scene Names")]
    [SerializeField] private string mainMenuSceneName;

    [Header("Games")]
    [SerializeField] private string flappyBirdGameScene;

    public void FlappyBirdButtonClickedFromGames()
    {
        StartCoroutine(LoadSceneAsync(flappyBirdGameScene));
    }
    public void GoBackButtonClickedFromGames()
    {
        StartCoroutine(LoadSceneAsync(mainMenuSceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }
}
