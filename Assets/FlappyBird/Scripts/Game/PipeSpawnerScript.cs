using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PipeSpawnerScript : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject PipePrefab;

    [Header("Variables")]
    [SerializeField] private float timerForPipes = 3;
    [SerializeField] private float maxHeightPipes = 3;
    [SerializeField] private float minHeightPipes = -3;
    [SerializeField] private float levelTimerDelta = 0.1f;

    [Header("Scripts")]
    [SerializeField] private BirdScript birdScript;

    [Header("Private Variables")]
    internal bool isBirdDied;
    private bool isArrayNull;
    internal CustomArray<PipePrefabScript> Pipes;
    private bool isHandFound = false;
    public bool leavingTheGame = false;

    private void Start()
    {
        StartCanvas();
    }

    // Function to start the canvas
    internal void StartCanvas()
    {
        isHandFound = false;
        isArrayNull = true;
        StartCoroutine(PipeTimer());
    }

    // Coroutine to spawn pipes
    private IEnumerator PipeTimer()
    {
        yield return new WaitForSeconds(timerForPipes);
        while (!leavingTheGame)
        {
            // Check if the bird is alive and hand is found
            if (isBirdDied || !isHandFound)
            {
                yield return null; // Wait and skip spawning if the bird is dead or no hand is detected
                continue;
            }

            yield return new WaitForSeconds(timerForPipes);
            InitializePipe();
        }
    }

    // Initializes or reuses pipes for spawning
    private void InitializePipe()
    {
        if (isBirdDied) return; // Ensure no pipes are initialized when the bird is dead

        GameObject newPipe = null;

        // Check if the array is null
        if (isArrayNull)
        {
            newPipe = Instantiate(PipePrefab);
            newPipe.transform.position = new Vector3(transform.position.x, DecideHeight(), 0);
            Pipes = new CustomArray<PipePrefabScript>(newPipe.GetComponent<PipePrefabScript>());
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
                Pipes.Add(newPipe.GetComponent<PipePrefabScript>());
                Debug.Log(Pipes.Length());
            }
            else
            {
                newPipe.transform.position = new Vector3(transform.position.x, DecideHeight(), 0);
            }
        }
    }

    // Decides random height of pipes for spawning
    private float DecideHeight()
    {
        return Random.Range(minHeightPipes, maxHeightPipes);
    }

    // Function for bird death event
    internal void BirdDied()
    {
        isBirdDied = true;
    }

    // Resets the spawner for a new game
    internal void StartAgain()
    {
        isBirdDied = false;
        isHandFound = false;
        StartCanvas();
    }

    // Sets the value of isHandFound
    internal void HandIsFound(bool isFound)
    {
        isHandFound = isFound;
    }

    // Reduces the pipe spawn timer
    internal void ReducePipeTime()
    {
        if (timerForPipes > 1.5f)
        {
            timerForPipes -= levelTimerDelta;
        }
    }
}
