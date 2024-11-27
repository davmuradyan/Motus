using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject PipePrefab;

    [Header("Variables")]
    [SerializeField] float timerForPipes = 3;
    [SerializeField] float maxHeightPipes = 3;
    [SerializeField] float minHeightPipes = -3;

    [Header("Scripts")]
    [SerializeField] BirdScript birdScript;

    [Header("Private Variables")]
    internal bool isBirdDied;
    private bool isArrayNull;
    private CustomArray<PipePrefabScript> Pipes;
    private bool isHandFound = false;
    public bool leavingTheGame = false;

    private void Start() {
        StartCanvas();
    }
    // Function to start the canvas
    internal void StartCanvas() {
        isHandFound = false;
        isArrayNull = true;
        StartCoroutine(PipeTimer());
    }

    // Spawn pipes every 3 seconds
    IEnumerator PipeTimer() {
        while (!leavingTheGame) {
            if (!isBirdDied && isHandFound) {
                yield return new WaitForSeconds(timerForPipes);
                InitializePipe();
            } else {
                yield return null;
            }
        }
    }

    // Makes pipes or reuses avaiable ones
    void InitializePipe() {
        GameObject newPipe = null;

        // Check if the array is null
        if (isArrayNull)
        {
            newPipe = Instantiate(PipePrefab);
            newPipe.transform.position = new Vector3(transform.position.x, DecideHeight(), 0);
            Pipes = new CustomArray<PipePrefabScript>(newPipe.transform.GetComponent<PipePrefabScript>());
            isArrayNull = false;
            Debug.Log(Pipes.Length());
        }
        else
        {
            newPipe = Pipes.CheckArray();

            if (newPipe == null)
            {
                newPipe = Instantiate(PipePrefab);
                newPipe.transform.position = new Vector3(transform.position.x, DecideHeight(), 0);
                Pipes.Add(newPipe.transform.GetComponent<PipePrefabScript>());
                Debug.Log(Pipes.Length());
            }
            else
            {
                newPipe.transform.position = new Vector3(transform.position.x, DecideHeight(), 0);
            }
        }
    }
    // Checks if there are avaiable pipes
    //private GameObject CheckArray() {
    //    GameObject newPipe = null;

    //    foreach (var pipe in Pipes) {
    //        if (pipe.IsAvailable()) {
    //            newPipe = pipe.gameObject;
    //            pipe.isAvailable = false;
    //            break;
    //        }
    //    }

    //    return newPipe;
    //}
    // Decides random height of pipes for spawning
    private float DecideHeight() {
        return Random.Range(minHeightPipes, maxHeightPipes);
    }

    // Function for bird die event
    internal void BirdDied() {
        isBirdDied = true;
    }
    // Function to start again
    internal void StartAgain() {
        isBirdDied = false;
        isHandFound = false;
        StartCoroutine(PipeTimer());
    }
    // Function to change the value of isHandFound bool variable
    internal void HandIsFound(bool isFound) {
        isHandFound = isFound;
    }
}
