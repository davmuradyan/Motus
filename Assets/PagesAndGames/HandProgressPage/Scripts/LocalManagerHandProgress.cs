using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LocalManagerHandProgress : MonoBehaviour
{
   
        [Header("Module2 Scene Names")]     
        [SerializeField] private string progressSceneName;

    public void GoBackButtonClickedFromHandPointHistory()
    {
        StartCoroutine(LoadSceneAsync(progressSceneName));

    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        yield return null;
    }

}
