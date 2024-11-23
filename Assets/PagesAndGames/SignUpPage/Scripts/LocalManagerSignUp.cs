using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LocalManagerSignUp : MonoBehaviour
{
    [Header("Module1 Scene Names")]
    [SerializeField] private string loginSceneName;
    [SerializeField] private string termsSceneName;

    [Header("Module2 Scene Names")]
    [SerializeField] private string mainMenuSceneName;

    [Header("Buttons")]
    [SerializeField] private Toggle termsCheckBox;
    [SerializeField] private Button signUpButton;

    private void Start()
    {
        termsCheckBox.isOn = false;
        signUpButton.interactable = false;
    }

    private void Update()
    {
        SignUpState();
    }

    private void SignUpState()
    {
        if (termsCheckBox.isOn)
        {
            signUpButton.interactable = true;
        }
        else
        {
            signUpButton.interactable = false;
        }
    }
    public void LoginButtonClickedFromSignUp()
    {
        StartCoroutine(LoadSceneAsync(loginSceneName));
    }

    public void SignUpButtonClickedFromSignUp()
    {
        //API request for signing up
        StartCoroutine(LoadSceneAsync(mainMenuSceneName));
    }

    public void AgreeWithTermsButtonClickedFromSignUp()
    {
        StartCoroutine(LoadSceneAsync(termsSceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }
}
