using Mono.Unix.Native;
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

    [Header("Error Messages")]
    [SerializeField] private TextMeshProUGUI errorMessage;

    [Header("Canvas")]
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject Message;

    [Header("Scripts")]
    [SerializeField] APIRequestSender requestSender;

    private void Start()
    {
        // Initialize button states and error messages
        logInBtn.interactable = false;
        errorMessage.text = string.Empty;
        Message.SetActive(false);
        Canvas.SetActive(true);

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
            requestSender.SendLoginRequest(username, password);
        }
        else
        {
            Message.SetActive(true);
            errorMessage.text = "Fields cannot be empty.";
            Debug.LogWarning("Cannot log in: Fields are incomplete.");
        }
    }

    public void Enter(bool valid) {
        if (valid) {
            StartCoroutine(LoadSceneAsync(mainMenuSceneName));
        } else {
            errorMessage.text = "Invalid username or password.";
            Message.SetActive(true);
            Debug.LogWarning("Login failed: Invalid credentials.");
        }
    }

    public void ErrorOkBtnClicked()
    {
        Message.SetActive(false);
    }

    public void ForgotPassButtonClickedFromLogin()
    {
        StartCoroutine(LoadSceneAsync(recoverPasswordSceneName));
    }

    private bool ValidateLoginCredentials(string username, string password)
    {
        // Placeholder for actual validation logic, such as checking a database or API
        requestSender.SendLoginRequest(username, password);

        // Replace with real authentication logic
        return false;
    }

    private void SaveCredentials(string username, string password)
    {
        // Placeholder for saving credentials locally
        // Use PlayerPrefs or a secure storage method
        PlayerPrefs.SetString("SavedUsername", username);
        PlayerPrefs.SetString("SavedPassword", password);
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
