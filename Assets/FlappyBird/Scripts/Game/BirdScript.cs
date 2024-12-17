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
    [SerializeField] private BackgroundSpawnerScript backgroundSpawner;

    [Header("Variables")]
    internal bool isDead;
    [SerializeField] private float minAllowedHeight;
    [SerializeField] private float maxAllowedHeight;
    [SerializeField] private float initialX;
    [SerializeField] private float initialY;
    [SerializeField] private float timeToDie;
    internal bool handFound;

    // The event when bird dies
    public event Action BirdDied;

    private void Awake()
    {
        SubscribeFunctionsToBirdDied();
    }
    private void Start()
    {
        InitializeBird();
    }

    // Subscribes all necessary functions to BirdDied event
    private void SubscribeFunctionsToBirdDied()
    {
        BirdDied += pipeSpawner.BirdDied;
        BirdDied += scoreKeeper.ScoreForEndgame;
        BirdDied += backgroundSpawner.BirdDied;
        BirdDied += () => pipeSpawner.Pipes.Freeze();
        BirdDied += () => backgroundSpawner.Bases.Freeze();
        BirdDied += () => backgroundSpawner.Cities.Freeze();

    }

    /*    private void Update()
        {
            MoveBird();
        }*/

    private void Update()
    {
        if (!isDead)
        {
            MoveBird();
        }
        else
        {
            // Ensure the bird's position remains constant
            Vector3 fixedPosition = transform.position;
            transform.position = new Vector3(fixedPosition.x, fixedPosition.y, fixedPosition.z);
        }
    }


    // Function to initialize the bird
    internal void InitializeBird()
    {
        transform.position = new Vector3(initialX, initialY, 0);
        isDead = false;
        handFound = false;
        gameObject.SetActive(true);
        pipeSpawner.isBirdDied = isDead;
        transform.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    // Function to add score when passes pipes
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pass" && !isDead)
        {
            scoreKeeper.AddScore();
        }
    }

    // Function to kill bird when it collides with pipes
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (((collision.gameObject.tag == "Pipe") || (collision.gameObject.tag == "Ground")) && !isDead)
        {
            Debug.Log(collision.gameObject.tag);
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        BirdDied?.Invoke();

        //Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();
        //rb.velocity = Vector2.zero; // Stop any motion
        //rb.gravityScale = 0; // Disable gravity for manual control
        //transform.rotation = Quaternion.Euler(0, 0, -90); // Rotate bird to face downward

        //// Continue moving down until the bird hits the ground
        //while (transform.position.y > minAllowedHeight)
        //{
        //    transform.position = new Vector3(transform.position.x, transform.position.y - (5f * Time.deltaTime), 0); // Adjust fall speed
        //    yield return null;
        //}

        //// Lay still on the ground
        //transform.position = new Vector3(transform.position.x, minAllowedHeight, 0);
        //rb.velocity = Vector2.zero; // Ensure no residual velocity
        //rb.gravityScale = 0; // Ensure gravity is fully off

        transform.gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        transform.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        // Signal game manager (or similar logic)
        localManager.BirdDied();
    }

    // Main function for moving the bird
    private void MoveBird()
    {
        if (isDead)
        {
            return; // Skip movement logic when the bird is dead
        }

        Rigidbody2D rb = transform.GetComponent<Rigidbody2D>();

        // Check if a hand is detected
        if (signalGenerator.hasFoundTheHand)
        {
            rb.gravityScale = 0.5f;

            float signal = signalGenerator.GetSignal();

            if (signal == 1)
            {
                float targetVelocityY = 3f;
                rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(rb.velocity.y, targetVelocityY, 0.5f));
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 25), Time.deltaTime * 5f);
            }
            else if (rb.velocity.y < 0)
            {
                float downwardAngle = Mathf.Lerp(0, -90, Mathf.Abs(rb.velocity.y) / 5f);
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, downwardAngle), Time.deltaTime * 2f);
            }

            float clampedY = Mathf.Clamp(transform.position.y, minAllowedHeight, maxAllowedHeight);
            transform.position = new Vector3(transform.position.x, clampedY, 0);
        }
        else
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.deltaTime * 5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5f);
        }
    }
}