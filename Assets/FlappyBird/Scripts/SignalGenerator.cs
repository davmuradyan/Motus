using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SignalGenerator : MonoBehaviour {
    [Header("Mediapipe objects")]
    [SerializeField] private GameObject MultiHandLandmarkList;

    [Header("The main array")]
    private GameObject[] points = new GameObject[21];

    [Header("Private variables")]
    private bool hasFoundTheHand = false;

    [Header("Scripts")]
    [SerializeField] PipeSpawnerScript pipeSpawner;

    private void Update() {
        if (!hasFoundTheHand) {
            TryToFindHand();
        }
    }

    // This function should return a float value from [0;1] interval
    // 0 means the hand is fully closed, and bird is on the bottom
    // 1 means the hand is fully opened, and vird is on the top
    // Note: Don't change the function name. Function is already linked to the bird
    internal float GetSignal() {
        // Write your code here...
        return 0;
    }

    // Write your code here...



    // No need to touch these functions

    // This function will try to find the hand
    private void TryToFindHand() {
        if (MultiHandLandmarkList.transform.childCount >= 1) {
            Initializer();
            hasFoundTheHand = true;
            pipeSpawner.HandIsFound(true);
        }
    }
    // This function retrieves 21 points
    // This function names 21 points
    // This function adds 21 points to our array
    private void Initializer() {
        GameObject LandMarkAnnotation = MultiHandLandmarkList.transform.GetChild(0).gameObject;
        GameObject Points = LandMarkAnnotation.transform.GetChild(0).gameObject;
        for (int i = 0; i < 21; i++) {
            GameObject Child = Points.transform.GetChild(i).gameObject;
            Child.name = $"Point {i}";
            points[i] = Child;
        }
    }
}