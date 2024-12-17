using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalManagerPass : MonoBehaviour
{
    [Header("Module1 Scene Names")]
    [SerializeField] private string loginSceneName;

    [Header("Input Fields")]
    [SerializeField] private TMP_InputField emailField;

    [Header("Buttons")]
    [SerializeField] private Button SubmitBtn;
    [SerializeField] private Button GoBackBtn;

    [Header("Error Messages")]
    [SerializeField] private TextMeshProUGUI errorMessage;

    [Header("Canvas")]
    [SerializeField] private GameObject Canvas;
    [SerializeField] private GameObject Message;


    private void Start()
    {
        // Initialize UI state
        SubmitBtn.interactable = false;
        errorMessage.text = string.Empty;
        Canvas.SetActive(true);
        Message.SetActive(false);

    }

    private void Update()
    {
        if (!string.IsNullOrEmpty(emailField.text))
        {
            SubmitBtn.interactable = true;
        }
    }


    public void ErrorOkBtnClicked()
    {
        Message.SetActive(false);
    }
    public void SubmitButtonClickedFromRecoverPassword()
    {
        if (!IsValidEmail(emailField.text))
        {
            errorMessage.color = Color.red;
            errorMessage.text = "Invalid email format.";
            Message.SetActive(true);
        }
        else
        {
            errorMessage.text = "Recovery email sent successfully. Go check your email";
            errorMessage.color = Color.white;
            Message.SetActive(true);
            SubmitBtn.interactable = false;
        }
    }

    private bool IsValidEmail(string email)
    {
        // Basic email validation logic
        return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$");
    }

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
