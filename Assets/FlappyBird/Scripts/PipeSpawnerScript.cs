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
    private bool isBirdDied;
    private bool isArrayNull;
    private CustomArray<PipePrefabScript> Pipes;

    private void Start() {
        isBirdDied = false;
        isArrayNull = true;
        StartCoroutine(PipeTimer());
        birdScript.BirdDied += BirdDied;
    }

    // Spawn pipes every 3 seconds
    IEnumerator PipeTimer() {
        while (!isBirdDied)
        {
            yield return new WaitForSeconds(timerForPipes);
            InitializePipe();
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
        }
        else
        {
            newPipe = CheckArray();

            if (newPipe == null)
            {
                newPipe = Instantiate(PipePrefab);
                newPipe.transform.position = new Vector3(transform.position.x, DecideHeight(), 0);
                Pipes.Add(newPipe.transform.GetComponent<PipePrefabScript>());
            }
            else
            {
                newPipe.transform.position = new Vector3(transform.position.x, DecideHeight(), 0);
            }
        }
    }
    // Checks if there are avaiable pipes
    private GameObject CheckArray() {
        GameObject newPipe = null;

        foreach (var pipe in Pipes) {
            if (pipe.IsAvailabe()) {
                newPipe = pipe.gameObject;
                pipe.isAvaiable = false;
                break;
            }
        }

        return newPipe;
    } 
    // Decides random height of pipes for spawning
    private float DecideHeight() {
        return Random.Range(minHeightPipes, maxHeightPipes);
    }
    // Function for bird die event
    private void BirdDied() {
        isBirdDied = true;
    }
}
