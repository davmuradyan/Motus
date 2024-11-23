using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LocalManager : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private BirdScript birdScript;
    [SerializeField] private PipeSpawnerScript pipeSpawner;
    [SerializeField] private SignalGenerator signalGenerator;

    [Header("Canvases")]
    [SerializeField] private GameObject MainGame_Canvas;
    [SerializeField] private GameObject EndGame_Canvas;

    [Header("Scenes")]
    [SerializeField] private string FlappyBird_GameScene;
    [SerializeField] private string Games_PageScene;

    // Event of restarting game
    internal event Action RestartGame;

    private void Awake() {
        SubscribeFunctionsToRestartGame();
    }
    private void Start() {
        Initialize();
    }

    private void SubscribeFunctionsToRestartGame() {
        RestartGame += Initialize;
        RestartGame += birdScript.InitializeBird;
        RestartGame += pipeSpawner.StartAgain;
        RestartGame += signalGenerator.Initialize;
    }

    // Function to initialize canvases
    private void Initialize() {
        MainGame_Canvas.SetActive(true);
        EndGame_Canvas.SetActive(false);
    }


    // Function to switch to EndGame_Canvas when bird dies
    internal void BirdDied() {
        MainGame_Canvas.SetActive(false);
        EndGame_Canvas.SetActive(true);
    }

    // Funtion for StartAgainBtn
    public void OnPlayAgainClicked() {
        // RestartGame?.Invoke();
        SceneManager.LoadScene(FlappyBird_GameScene);
    }

    public void OnLeaveGameClicked() {
        // Send points to API asyncronously
        Debug.Log("Loading");
        pipeSpawner.leavingTheGame = true;
        SceneManager.LoadScene(Games_PageScene);
        Debug.Log("Loaded");
    }
}
