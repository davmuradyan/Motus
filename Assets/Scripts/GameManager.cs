using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [Header("Scene Names")]
    [SerializeField] private Scene Login_Scene;
    [SerializeField] private Scene SignUp_Scene;
    private string loginSceneName;
    private string signUpSceneName;

    private void Start() {
        signUpSceneName = SignUp_Scene.name;
        loginSceneName = Login_Scene.name;
    }
        
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