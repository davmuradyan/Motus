using System;
using System.Collections;
using TMPro;
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

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField FirstName;
    [SerializeField] private TMP_InputField LastName;
    [SerializeField] private TMP_InputField Email;
    [SerializeField] private TMP_InputField BirthDate;
    [SerializeField] private TMP_InputField PhoneNumber;
    [SerializeField] private TMP_InputField Password;
    [SerializeField] private TMP_InputField ConfirmPassword;

    [Header("Buttons")]
    [SerializeField] private Toggle termsCheckBox;
    [SerializeField] private Button signUpButton;

    [Header("Error Messages")]
    [SerializeField] private TextMeshProUGUI errorMessage;

    private void Start()
    {
        // Initialize UI state
        termsCheckBox.isOn = false;
        signUpButton.interactable = false;
        errorMessage.text = string.Empty;

        // Add listeners to check input fields
        AddInputFieldListeners();
    }

    private void AddInputFieldListeners()
    {
        FirstName.onValueChanged.AddListener(_ => CheckSignUpButtonState());
        LastName.onValueChanged.AddListener(_ => CheckSignUpButtonState());
        Email.onValueChanged.AddListener(_ => CheckSignUpButtonState());
        BirthDate.onValueChanged.AddListener(_ => CheckSignUpButtonState());
        PhoneNumber.onValueChanged.AddListener(_ => CheckSignUpButtonState());
        Password.onValueChanged.AddListener(_ => CheckSignUpButtonState());
        ConfirmPassword.onValueChanged.AddListener(_ => CheckSignUpButtonState());
        termsCheckBox.onValueChanged.AddListener(_ => CheckSignUpButtonState());
    }

    private void CheckSignUpButtonState()
    {
        // Check if all fields are filled and terms checkbox is checked
        if (!string.IsNullOrEmpty(FirstName.text) &&
            !string.IsNullOrEmpty(LastName.text) &&
            !string.IsNullOrEmpty(Email.text) &&
            !string.IsNullOrEmpty(BirthDate.text) &&
            !string.IsNullOrEmpty(PhoneNumber.text) &&
            !string.IsNullOrEmpty(Password.text) &&
            !string.IsNullOrEmpty(ConfirmPassword.text) &&
            termsCheckBox.isOn)
        {
            signUpButton.interactable = true;
        }
        else
        {
            signUpButton.interactable = false;
        }
    }

    public void SignUpButtonClickedFromSignUp()
    {
        errorMessage.text = string.Empty; // Clear previous errors
        if (ValidateInputs())
        {
            Debug.Log("Sign-up process successful!");
            StartCoroutine(LoadSceneAsync(mainMenuSceneName));
        }
    }

    private bool ValidateInputs()
    {

        // First Name validation
        if (string.IsNullOrEmpty(FirstName.text))
        {
            errorMessage.text = "First Name is required.";
            return false;
        }

        // Last Name validation
        if (string.IsNullOrEmpty(LastName.text))
        {
            errorMessage.text = "Last Name is required.";
            return false;
        }

        // Email validation
        if (!IsValidEmail(Email.text))
        {
            errorMessage.text = "Invalid Email address.";
            return false;
        }

        // Birth Date validation
        if (!IsValidBirthDate(BirthDate.text))
        {
            errorMessage.text = "Invalid Birth Date. Format: YYYY-MM-DD.";
            return false;
        }

        // Phone Number validation
        if (!IsValidPhoneNumber(PhoneNumber.text))
        {
            errorMessage.text = "Phone Number must be at least 10 digits.";
            return false;
        }

        // Password validation
        if (!IsValidPassword(Password.text))
        {
            errorMessage.text = "Password must be at least 8 characters long, with uppercase, lowercase, digit, and special character.";
            return false;
        }

        // Confirm Password validation
        if (Password.text != ConfirmPassword.text)
        {
            errorMessage.text = "Passwords do not match.";
            return false;
        }

        return true; // All validations passed
    }

    private bool IsValidEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
    }

    private bool IsValidBirthDate(string birthDate)
    {
        if (DateTime.TryParse(birthDate, out var parsedDate))
        {
            return parsedDate <= DateTime.Now;
        }
        return false;
    }

    private bool IsValidPhoneNumber(string phoneNumber)
    {
        return phoneNumber.Length >= 10;
    }

    private bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8)
            return false;
        if (!ContainsUppercase(password))
            return false;
        if (!ContainsLowercase(password))
            return false;
        if (!ContainsDigit(password))
            return false;
        if (!ContainsSpecialCharacter(password))
            return false;

        return true;
    }

    private bool ContainsUppercase(string input)
    {
        foreach (char c in input)
        {
            if (char.IsUpper(c))
                return true;
        }
        return false;
    }

    private bool ContainsLowercase(string input)
    {
        foreach (char c in input)
        {
            if (char.IsLower(c))
                return true;
        }
        return false;
    }

    private bool ContainsDigit(string input)
    {
        foreach (char c in input)
        {
            if (char.IsDigit(c))
                return true;
        }
        return false;
    }

    private bool ContainsSpecialCharacter(string input)
    {
        foreach (char c in input)
        {
            if (!char.IsLetterOrDigit(c))
                return true;
        }
        return false;
    }

    public void LoginButtonClickedFromSignUp()
    {
        StartCoroutine(LoadSceneAsync(loginSceneName));
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
