using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SignalGenerator : MonoBehaviour
{
    [Header("Mediapipe objects")]
    [SerializeField] private GameObject MultiHandLandmarkList;

    [Header("The main array")]
    private GameObject[] points = new GameObject[21];

    [Header("Private variables")]
    private bool hasFoundTheHand = false;

    [Header("Scripts")]
    [SerializeField] PipeSpawnerScript pipeSpawner;
    [SerializeField] BirdScript birdScript;

    private void Update()
    {
        if (!hasFoundTheHand)
        {
            TryToFindHand();
        }
    }

    internal float GetSignal()
    {

        List<int> closedFingers = GetClosedFingers();

        // Define the patterns that simulate a "tap" gesture.
        List<List<int>> tapPatterns = new List<List<int>> {
        new List<int> { 1, 2, 3, 4 },
        new List<int> { 1, 2, 3 },
        new List<int> { 2, 3, 4 }
    };

        
        foreach (var pattern in tapPatterns)
        {
            if (IsSubset(closedFingers, pattern))
            {
                Debug.Log("Tap gesture detected!");
                return 1;
            }
        }

        // No tap detected.
        return 0;
    }
    
    private List<int> GetClosedFingers()
    {
        List<int> closedFingers = new List<int>();
        if (points.Length < 21) return closedFingers;

        // Calculate distances for each finger.
        Vector3 wrist = points[0].transform.position;
        Debug.Log(wrist.x);

        // Index finger (landmarks 8, 7)
        if (IsFingerClosed(wrist, points[8].transform.position, points[7].transform.position))
        {
            closedFingers.Add(1);
        }
        // Middle finger (landmarks 12, 11)
        if (IsFingerClosed(wrist, points[12].transform.position, points[11].transform.position))
        {
            closedFingers.Add(2);
        }
        // Ring finger (landmarks 16, 15)
        if (IsFingerClosed(wrist, points[16].transform.position, points[15].transform.position))
        {
            closedFingers.Add(3);
        }
        // Pinky finger (landmarks 20, 19)
        if (IsFingerClosed(wrist, points[20].transform.position, points[19].transform.position))
        {
            closedFingers.Add(4);
        }

        return closedFingers;
    }

    
    private bool IsFingerClosed(Vector3 wrist, Vector3 fingertip, Vector3 midJoint)
    {
        float distToFingertip = Vector3.Distance(wrist, fingertip);
        float distToMidJoint = Vector3.Distance(wrist, midJoint);
        return distToFingertip < distToMidJoint;
    }


    private bool IsSubset(List<int> mainList, List<int> subset)
    {
        foreach (int item in subset)
        {
            if (!mainList.Contains(item))
            {
                return false;
            }
        }
        return true;
    }


    private void TryToFindHand()
    {
        if (MultiHandLandmarkList.transform.childCount >= 1)
        {
            ArrayInitializer();
            birdScript.handFound = true;
            hasFoundTheHand = true;
            pipeSpawner.HandIsFound(true);
        }
    }

    private void ArrayInitializer()
    {
        GameObject LandMarkAnnotation = MultiHandLandmarkList.transform.GetChild(0).gameObject;
        GameObject Points = LandMarkAnnotation.transform.GetChild(0).gameObject;
        for (int i = 0; i < 21; i++)
        {
            GameObject Child = Points.transform.GetChild(i).gameObject;
            Child.name = $"Point {i}";
            points[i] = Child;
        }
    }
}