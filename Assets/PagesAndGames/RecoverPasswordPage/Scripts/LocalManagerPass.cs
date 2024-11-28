using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LocalManagerPass : MonoBehaviour
{
    [Header("Module1 Scene Names")]
    [SerializeField] private string loginSceneName;

    public void GoBackButtonClickedFromRecoverPassword()
    {
        StartCoroutine(LoadSceneAsync(loginSceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }
}
