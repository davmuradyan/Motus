using System;
using System.Collections;
using System.Linq;
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

    [Header("Canvas")]
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject Message;

    private bool isOkay;

    private void Start()
    {
        // Initialize UI state
        termsCheckBox.isOn = false;
        signUpButton.interactable = false;
        errorMessage.text = string.Empty;
        Canvas.SetActive(true);
        Message.SetActive(false);
        isOkay = false;
        

        LoadUserData(); // Load data from PlayerPrefs if available
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
        Message.SetActive(false);
        errorMessage.text = string.Empty;
        isOkay = ValidateInputs();
        
        if (isOkay)
        {
            ClearUserData(); // Clear PlayerPrefs data upon successful signup
            Message.SetActive(true);
            errorMessage.text ="Confirm your email, then you will be able to sign in successfully";
        
;        }
    }

    private bool ValidateInputs()
    {   
        Message.SetActive(true);
        errorMessage.color = Color.red;
        // First Name validation
        if (string.IsNullOrEmpty(FirstName.text))
        {
            errorMessage.text = "First Name is required.";
            return false;
        }

        if (!isValidName(FirstName.text))
        {
            errorMessage.text = "First name cannot be longeer than 35 characters";
            return false;
        }


        // Last Name validation
        if (string.IsNullOrEmpty(LastName.text))
        {
            errorMessage.text = "Last Name is required.";
            return false;
        }

        if (!isValidSurname(LastName.text))
        {
            errorMessage.text = "Last name cannot be longer than 35 characters";
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

        errorMessage.color= Color.white;
        return true; // All validations passed
    }

    public void ErrorOkBtnClicked()
    {
        if (isOkay)
        {
            StartCoroutine(LoadSceneAsync(loginSceneName));           
        }
        else
        {
            Message.SetActive(false);
        }
    }

    private bool isValidName(string name)
    {
        return name.Length <= 35;
    }

    private bool isValidSurname(string surname)
    {
        return surname.Length <= 35;
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
        return password.Length >= 8 &&
               ContainsUppercase(password) &&
               ContainsLowercase(password) &&
               ContainsDigit(password) &&
               ContainsSpecialCharacter(password);
    }

    private bool ContainsUppercase(string input) => input.Any(char.IsUpper);
    private bool ContainsLowercase(string input) => input.Any(char.IsLower);
    private bool ContainsDigit(string input) => input.Any(char.IsDigit);
    private bool ContainsSpecialCharacter(string input) => input.Any(c => !char.IsLetterOrDigit(c));

    public void LoginButtonClickedFromSignUp()
    {
        SaveUserData(); // Save data before navigating to login
        StartCoroutine(LoadSceneAsync(loginSceneName));
    }

    public void AgreeWithTermsButtonClickedFromSignUp()
    {
        SaveUserData(); // Save data before navigating to terms
        StartCoroutine(LoadSceneAsync(termsSceneName));
    }

    private void SaveUserData()
    {
        PlayerPrefs.SetString("FirstName", FirstName.text);
        PlayerPrefs.SetString("LastName", LastName.text);
        PlayerPrefs.SetString("Email", Email.text);
        PlayerPrefs.SetString("BirthDate", BirthDate.text);
        PlayerPrefs.SetString("PhoneNumber", PhoneNumber.text);
        PlayerPrefs.SetString("Password", Password.text);
        PlayerPrefs.SetString("ConfirmPassword", ConfirmPassword.text);
        PlayerPrefs.SetInt("TermsAccepted", termsCheckBox.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadUserData()
    {
        FirstName.text = PlayerPrefs.GetString("FirstName", "");
        LastName.text = PlayerPrefs.GetString("LastName", "");
        Email.text = PlayerPrefs.GetString("Email", "");
        BirthDate.text = PlayerPrefs.GetString("BirthDate", "");
        PhoneNumber.text = PlayerPrefs.GetString("PhoneNumber", "");
        Password.text = PlayerPrefs.GetString("Password", "");
        ConfirmPassword.text = PlayerPrefs.GetString("ConfirmPassword", "");
        termsCheckBox.isOn = PlayerPrefs.GetInt("TermsAccepted", 0) == 1;
    }

    private void ClearUserData()
    {
        PlayerPrefs.DeleteKey("FirstName");
        PlayerPrefs.DeleteKey("LastName");
        PlayerPrefs.DeleteKey("Email");
        PlayerPrefs.DeleteKey("BirthDate");
        PlayerPrefs.DeleteKey("PhoneNumber");
        PlayerPrefs.DeleteKey("Password");
        PlayerPrefs.DeleteKey("ConfirmPassword");
        PlayerPrefs.DeleteKey("TermsAccepted");
        PlayerPrefs.Save();
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        yield return null;
    }
}
