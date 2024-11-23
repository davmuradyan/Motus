using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalManagerTerms : MonoBehaviour
{
    [Header("Module1 Scene Names")]
    [SerializeField] private string signUpSceneName;

    public void GoBackButtonClickedFromTerms()
    {
        StartCoroutine(LoadSceneAsync(signUpSceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }

}
