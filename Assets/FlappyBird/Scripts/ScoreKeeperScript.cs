using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeperScript : MonoBehaviour
{
    [Header("ScoreText")]
    [SerializeField] private TextMeshProUGUI Score_TMP;
    [SerializeField] private TextMeshProUGUI Score_TMP_Endgame;
    [SerializeField] private TextMeshProUGUI HighestScore_TMP;

    [Header("Private Variables")]
    private Int16 score;
    private Int16 highestScore;

    [Header("Scripts")]
    [SerializeField] BirdScript birdScript;

    private void Start() {
        GetHighestScore();
        score = 0;
        Score_TMP.text = score.ToString();
    }

    // Function to change score text by adding +1
    internal void AddScore() {
        score++;
        Score_TMP.text = score.ToString();
    }
    // Function for transfering score to EndCanvas (not complete)
    // Subscriber of BirdDied
    internal void ScoreForEndgame() {
        Score_TMP_Endgame.text = Score_TMP.text;
        if (score > highestScore)
        {
            highestScore = score;

            // Change hihest score on the server...
            SendHighestScore();
        }
        HighestScore_TMP.text = score.ToString();
        Score_TMP.text = "" + 0;
        SendScore();
        score = 0;
    }
    // Function to get highest score (not written)
    internal void GetHighestScore() {
        highestScore = 0;
    }

    // Function to send point to DB
    internal void SendScore() {
        Debug.Log(score);
    }

    internal void SendHighestScore() {
        Debug.Log(highestScore);
    }
}