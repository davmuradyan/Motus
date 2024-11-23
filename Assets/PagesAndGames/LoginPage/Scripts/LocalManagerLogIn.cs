using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalManagerLogIn : MonoBehaviour
{
    [Header("Module1 Scene Names")]
    [SerializeField] private string signUpSceneName;
    [SerializeField] private string recoverPasswordSceneName;

    [Header("Module2 Scene Names")]
    [SerializeField] private string mainMenuSceneName;

    public void SignUpButtonClickedFromLogin()
    {
        StartCoroutine(LoadSceneAsync(signUpSceneName));
    }

    public void LoginButtonClickedFromLogin()
    {
        //API request for login
        StartCoroutine(LoadSceneAsync(mainMenuSceneName));
    }

    public void ForgotPassButtonClickedFromLogin()
    {
        StartCoroutine(LoadSceneAsync(recoverPasswordSceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }
}
