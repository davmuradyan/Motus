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

    private void Start()
    {
        // Initialize button states and error messages
        logInBtn.interactable = false;
        errorMessage.text = string.Empty;

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
            if (ValidateLoginCredentials(username, password))
            {
                Debug.Log($"Logging in with Username: {username}");

                // Check Remember Me toggle
                if (rememberMe.isOn)
                {
                    Debug.Log("Remember Me is ON: Save credentials locally.");
                    SaveCredentials(username, password);
                }

                // Navigate to the main menu
                StartCoroutine(LoadSceneAsync(mainMenuSceneName));
            }
            else
            {
                errorMessage.text = "Invalid username or password.";
                Debug.LogWarning("Login failed: Invalid credentials.");
            }
        }
        else
        {
            errorMessage.text = "Fields cannot be empty.";
            Debug.LogWarning("Cannot log in: Fields are incomplete.");
        }
    }

    public void ForgotPassButtonClickedFromLogin()
    {
        StartCoroutine(LoadSceneAsync(recoverPasswordSceneName));
    }

    private bool ValidateLoginCredentials(string username, string password)
    {
        // Placeholder for actual validation logic, such as checking a database or API
        // Replace with real authentication logic
        return username == "testUser" && password == "testPassword";
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
