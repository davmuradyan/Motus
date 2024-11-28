using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField usernameField;
    [SerializeField] private TMP_InputField passwordField;

    [Header("Buttons")]
    [SerializeField] private Toggle rememberMe;
    [SerializeField] private Button logInBtn;
    [SerializeField] private Button signUpBtn;

    private void Start()
    {
        // Initialize button states
        logInBtn.interactable = false;

        // Add listeners to input fields
        usernameField.onValueChanged.AddListener(_ => ValidateInput());
        passwordField.onValueChanged.AddListener(_ => ValidateInput());
    }

    private void ValidateInput()
    {
        // Enable login button only if both fields are filled
        logInBtn.interactable = !string.IsNullOrEmpty(usernameField.text) && !string.IsNullOrEmpty(passwordField.text);
    }

    public void SignUpButtonClickedFromLogin()
    {
        StartCoroutine(LoadSceneAsync(signUpSceneName));
    }

    public void LoginButtonClickedFromLogin()
    {
        string username = usernameField.text;
        string password = passwordField.text;

        if (logInBtn.interactable)
        {
            Debug.Log($"Logging in with Username: {username}, Password: {password}");

            /* Check Remember Me toggle
            if (rememberMe.isOn)
            {
                Debug.Log("Remember Me is ON: Save credentials locally.");
                // Save credentials logic (e.g., PlayerPrefs or a secure method)
            }*/

            // Navigate to the main menu
            StartCoroutine(LoadSceneAsync(mainMenuSceneName));
        }
        else
        {
            Debug.LogWarning("Cannot log in, fields are incomplete.");
        }
    }

    public void ForgotPassButtonClickedFromLogin()
    {
        StartCoroutine(LoadSceneAsync(recoverPasswordSceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}

