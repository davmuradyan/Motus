using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    [Header("Module1 Scene Names")]
    [SerializeField] private string loginSceneName;
    [SerializeField] private string signUpSceneName;
    [SerializeField] private string recoverPasswordSceneName;
    [SerializeField] private string termsSceneName;

    [Header("Module2 Scene Names")]
    [SerializeField] private string gameProgressHistorySceneName;
    [SerializeField] private string gamesSceneName;
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string progressSceneName;
    [SerializeField] private string flappyBirdProgressSceneName;
    [SerializeField] private string handProgressHistorySceneName;

    [Header("Games")]
    [SerializeField] private string flappyBirdGameScene;

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
        if (termsCheckBox.isOn)
        {
            signUpButton.interactable = true;
        }
    }
    public void SignUpButtonClickedFromLogin() {
        StartCoroutine(LoadSceneAsync(signUpSceneName));
    }

    public void LoginButtonClickedFromSignUp() {
        StartCoroutine(LoadSceneAsync(loginSceneName));
    }

    public void SignUpButtonClickedFromSignUp()
    {
        //API request for signing up
        StartCoroutine (LoadSceneAsync(mainMenuSceneName));
    }

    public void LoginButtonClickedFromLogin()
    {
        //API request for login
        StartCoroutine(LoadSceneAsync(mainMenuSceneName));
    }

    public void AgreeWithTermsButtonClickedFromSignUp()
    {
        StartCoroutine(LoadSceneAsync(termsSceneName));
    }

    public void GoBackButtonClickedFromTerms()
    {
        StartCoroutine(LoadSceneAsync(signUpSceneName));
    }

    public void ForgotPassButtonClickedFromLogin()
    {
        StartCoroutine(LoadSceneAsync(recoverPasswordSceneName));

    }

    public void GoBackButtonClickedFromRecoverPassword()
    {
        StartCoroutine(LoadSceneAsync(loginSceneName));

    }

    public void ProgressButtonClickedFromMainMenu()
    {
        StartCoroutine(LoadSceneAsync(progressSceneName));

    }

    public void GamesButtonClickedFromMainMenu()
    {
        StartCoroutine(LoadSceneAsync(gamesSceneName));

    }

    public void LogOutButtonClickedFromMainMenu()
    {
        StartCoroutine(LoadSceneAsync(loginSceneName));

    }

    public void GameProgressButtonClickedFromProgress()
    {
        StartCoroutine(LoadSceneAsync(gameProgressHistorySceneName));

    }

    public void HandProgressButtonClickedFromProgress()
    {
        StartCoroutine(LoadSceneAsync(handProgressHistorySceneName));

    }

    public void GoBackButtonClickedFromHandPointHistory()
    {
        StartCoroutine(LoadSceneAsync(progressSceneName));

    }
    public void GoBackButtonClickedFromProgress()
    {
        StartCoroutine(LoadSceneAsync(mainMenuSceneName));

    }

    public void FlappyBirdButtonClickedFromGamePointHistory()
    {
        StartCoroutine(LoadSceneAsync(flappyBirdProgressSceneName));

    }

    public void GoBackButtonClickedFromGamePointHistory()
    {
        StartCoroutine(LoadSceneAsync(progressSceneName));

    }

    public void GoBackButtonClickedFromFlappyBirdHistory()
    {
        StartCoroutine(LoadSceneAsync(gameProgressHistorySceneName));

    }

    public void FlappyBirdButtonClickedFromGames()
    {
        StartCoroutine(LoadSceneAsync(flappyBirdGameScene));
    }
    public void GoBackButtonClickedFromGames()
    {
        StartCoroutine(LoadSceneAsync(mainMenuSceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }
}