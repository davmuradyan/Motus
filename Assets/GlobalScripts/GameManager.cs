using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [Header("Scene Names")]
    [SerializeField] private string loginSceneName;
    [SerializeField] private string signUpSceneName;
    [SerializeField] private string recoverPassword;
        
    public void SignUpButtonClickedFromLogin() {
        StartCoroutine(LoadSceneAsync(signUpSceneName));
    }

    public void LoginButtonClickedFromSignUp() {
        StartCoroutine(LoadSceneAsync(loginSceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName) {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }
}