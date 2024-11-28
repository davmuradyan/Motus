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

    [Header("Variables")]
    private bool isDead;
    [SerializeField] private float minAllowedHeight;
    [SerializeField] private float maxAllowedHeight;
    [SerializeField] private float initialX;
    [SerializeField] private float initialY;
    [SerializeField] private float timeToDie;
    internal bool handFound;

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
    }

    private void Update() {
        MoveBird();
    }

    // Function to initialize the bird
    internal void InitializeBird() {
        transform.position = new Vector3(initialX, initialY, 0);
        isDead = false;
        handFound = false;
        gameObject.SetActive(true);
        pipeSpawner.isBirdDied = isDead;
        transform.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
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
        Debug.Log(collision.gameObject.tag);
        if((collision.gameObject.tag == "Pipe" || collision.gameObject.tag == "Ground") && !isDead) {
            StartCoroutine(Die());
        }
    }

    // Function to kill the bird
    private IEnumerator Die() {
        isDead = true;
        BirdDied?.Invoke();

        yield return new WaitForSeconds(timeToDie);
        gameObject.SetActive(false);
        localManager.BirdDied();
    }


    // Function to move the bird
    private void MoveBird()
    {
        if (!isDead && handFound)
        {
            float signal = signalGenerator.GetSignal();

            Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();

            // Check if signal is 1 (tap detected).
            if (signal == 1)
            {
                // Apply upward force.
                rb.velocity = new Vector2(rb.velocity.x, 3f); // Adjust "5f" for the tap force strength.
                transform.rotation = Quaternion.Euler(0, 0, 35); // Rotate upwards briefly.
            }

            // Smoothly rotate downward as the bird falls.
            if (rb.velocity.y < 0)
            {
                float downwardAngle = Mathf.Lerp(0, -90, -rb.velocity.y / 5f); // Adjust "10f" for smoothing.
                transform.rotation = Quaternion.Euler(0, 0, downwardAngle);
            }

            // Clamp the bird's position to stay within allowed bounds.
            float clampedY = Mathf.Clamp(transform.position.y, minAllowedHeight, maxAllowedHeight);
            transform.position = new Vector3(transform.position.x, clampedY, 0);
        }
    }
}