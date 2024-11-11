using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private ScoreKeeperScript scoreKeeper;

    [Header("Private variables")]
    private bool isDead;
    [SerializeField] private float minAllowedHeight;
    [SerializeField] private float maxAllowedHeight;

    float variable = 0;

    // The event when bird dies
    public event Action BirdDied;

    private void Start() {
        isDead = false;
    }

    private void Update() {
        MoveBird();
    }

    // Function to add score when passes pipes
    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Pass")
        {
            scoreKeeper.AddScore();
        }
    }

    // Function to kill bird when it collides with pipes
    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Pipe") {
            StartCoroutine(Die());
        }
    }

    // Function to kill the bird
    private IEnumerator Die() {
        isDead = true;
        transform.GetComponent<Rigidbody2D>().gravityScale = 1;
        BirdDied?.Invoke();

        yield return new WaitForSeconds(2);
        Destroy(gameObject);
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
        float signal = GetSignal();
        float height = ConvertSignal(signal);
        transform.position = new Vector3(transform.position.x, height, 0);
    }
    private float GetSignal() {
         variable += 0.2f * Time.deltaTime;
         return variable;
    }
}
