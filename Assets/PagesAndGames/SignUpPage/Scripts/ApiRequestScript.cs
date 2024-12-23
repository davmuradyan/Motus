using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class APIRequestSender : MonoBehaviour {

    private string BASE_URL = "https://localhost:7245/";
    private bool signUpSent;
    private bool logingSent;
    private string response;

    [Header("Scripts")]
    [SerializeField] private LocalManagerLogIn loginManager;
    public void SendAddUserRequest(AddUserRequest user) {
        if (signUpSent)
        {
            Debug.Log("Already sent");
            return;
        }
        string url = $"https://localhost:7245/AddUser?firstname={UnityWebRequest.EscapeURL(user.firstname)}&lastname={UnityWebRequest.EscapeURL(user.lastname)}&email={UnityWebRequest.EscapeURL(user.email)}&data={UnityWebRequest.EscapeURL(user.data)}&phonenumber={UnityWebRequest.EscapeURL(user.phonenumber)}&password={UnityWebRequest.EscapeURL(user.password)}";

        signUpSent = true;
        // Start the coroutine to send the request
        StartCoroutine(SendPostRequest(url, true));
    }

    public void SendLoginRequest(string email, string password) {
        if (logingSent)
        {
            Debug.Log("Already sent");
            return;
        }
        string url = BASE_URL + $"Login?email={UnityWebRequest.EscapeURL(email)}&password={UnityWebRequest.EscapeURL(password)}";

        logingSent = true;
        // Start the coroutine to send the request
        StartCoroutine(SendPostRequest(url, false));
    }
    private IEnumerator SendPostRequest(string url, bool page) {
        using (UnityWebRequest webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)) {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError) {
                Debug.LogError($"Error: {webRequest.error}");
            } else {
                string resopnse = webRequest.downloadHandler.text;
                Debug.Log($"Response: {response}");
                if (logingSent)
                {
                    bool valid = JsonUtility.FromJson<ResponseLoginData>(webRequest.downloadHandler.text).success;
                    Debug.Log(valid);
                    loginManager.Enter(valid);
                }
            }
            if (page)
            {
                signUpSent = false;
            } else {
                logingSent = false;
            }
        }
    }

    public class ResponseLoginData {
        public bool success;
    }
}