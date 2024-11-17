using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeperScript : MonoBehaviour
{
    [Header("ScoreText")]
    [SerializeField] private TextMeshProUGUI Score_TMP;

    [Header("Private Variables")]
    private Int16 score;

    private void Start() {
        score = 0;
        Score_TMP.text = score.ToString();
    }

    // Function to change score text by adding +1
    internal void AddScore() {
        score++;
        Score_TMP.text = score.ToString();
    }
}
