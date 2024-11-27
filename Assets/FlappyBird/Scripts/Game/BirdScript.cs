using System;
using System.Collections;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ScoreKeeperScript scoreKeeper;
    [SerializeField] private SignalGenerator signalGenerator;
    [SerializeField] private PipeSpawnerScript pipeSpawner;
    [SerializeField] private LocalManager localManager;

    [Header("Private variables")]
    private bool isDead;
    [SerializeField] private float minAllowedHeight;
    [SerializeField] private float maxAllowedHeight;
    [SerializeField] private float initialX;
    [SerializeField] private float initialY;
    [SerializeField] private float timeToDie;

    // The event when bird dies
    public event Action BirdDied;

    private void Awake() {
        SubscribeFunctionsToBirdDied();
    }
    private void Start() {
        InitializeBird();
    }

    // Subscribes all necessary functions to BirdDied event
    private void SubscribeFunctionsToBirdDied() {
        BirdDied += pipeSpawner.BirdDied;
        BirdDied += scoreKeeper.ScoreForEndgame;
        BirdDied += signalGenerator.BirdDied;
    }

    private void Update() {
        // MoveBird();
    }

    // Function to initialize the bird
    internal void InitializeBird() {
        transform.position = new Vector3(initialX, initialY, 0);
        isDead = false;
        gameObject.SetActive(true);
        pipeSpawner.isBirdDied = isDead;
        transform.GetComponent<Rigidbody2D>().gravityScale = 0;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Function to add score when passes pipes
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Pass" && !isDead)
        {
            scoreKeeper.AddScore();
        }
    }

    // Function to kill bird when it collides with pipes
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Pipe" && !isDead) {
            StartCoroutine(Die());
        }
    }

    // Function to kill the bird
    private IEnumerator Die() {
        isDead = true;
        transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        BirdDied?.Invoke();

        yield return new WaitForSeconds(timeToDie);
        gameObject.SetActive(false);
        localManager.BirdDied();
    }

    // Function to convert signal into height of bird
    private float ConvertSignal(float signal) {
        if (signal < 0 || signal > 1)
        {
            throw new Exception("Signal should be a float number from [0;1] interval");
        }

        float newHeight = (maxAllowedHeight - minAllowedHeight) * signal + minAllowedHeight;

        return newHeight;
    }

    // Function to move the bird
    private void MoveBird() {
        if (!isDead) {
            float signal = signalGenerator.GetSignal();
            float height = ConvertSignal(signal);
            transform.position = new Vector3(transform.position.x, height, 0);
        }
    }
}