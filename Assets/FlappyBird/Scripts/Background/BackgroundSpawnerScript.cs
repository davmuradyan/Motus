using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawnerScript : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject Base_Prefab;
    [SerializeField] private GameObject City_Prefab;

    [Header("Variables")]
    bool isBaseArrayNull = true;
    bool isCityArrayNull = true;
    [SerializeField] float baseHeight = -4.9f;
    internal CustomArray<BaseScript> Bases;
    internal CustomArray<CityScript> Cities;
    private bool leavingTheGame;
    [SerializeField] private float timerForBase;
    [SerializeField] private float timerForCity;

    internal bool birdDied;

    private void Start() {
        leavingTheGame = false;
        birdDied = false;
        StartCoroutine(BaseTimer());
        StartCoroutine(CityTimer());
    }

    // This function initializes the ground
    // This function creates or reuses available ground from Bases array
    internal void InitializeBase() {
        GameObject Base = null;

        // Check if the array is null
        if (isBaseArrayNull) {
            Base = Instantiate(Base_Prefab);
            Base.transform.position = new Vector3(transform.position.x, baseHeight, 0);
            Bases = new CustomArray<BaseScript>(Base.transform.GetComponent<BaseScript>());
            isBaseArrayNull = false;
        } else {
            Base = Bases.CheckArray();

            if (Base == null) {
                Base = Instantiate(Base_Prefab);
                Base.transform.position = new Vector3(transform.position.x, baseHeight, 0);
                Bases.Add(Base.transform.GetComponent<BaseScript>());
            } else {
                Base.transform.position = new Vector3(transform.position.x, baseHeight, 0);
            }
        }
    }

    // This function initializes the background city
    // This function creates or reuses available city from Cities array
    internal void InitializeCity() {
        GameObject City = null;

        // Check if the array is null
        if (isCityArrayNull) {
            City = Instantiate(City_Prefab);
            City.transform.position = new Vector3(transform.position.x, 0, 0);
            Cities = new CustomArray<CityScript>(City.transform.GetComponent<CityScript>());
            isCityArrayNull = false;
        } else {
            City = Cities.CheckArray();

            if (City == null) {
                City = Instantiate(City_Prefab);
                City.transform.position = new Vector3(transform.position.x, 0, 0);
                Cities.Add(City.transform.GetComponent<CityScript>());
            } else {
                City.transform.position = new Vector3(transform.position.x, 0, 0);
            }
        }
    }

    // This function calls InitializeBase() every {timerForBase} seconds
    // This function terminates when player leaves the game
    IEnumerator BaseTimer() {
        while (!leavingTheGame) {
            if (!birdDied)
            {
                yield return new WaitForSeconds(timerForBase);
                if (!birdDied) {
                    InitializeBase();
                }
            }
            else
            {
                yield break;
            }
        }
    }

    // This function calls InitializeCity() every {timerForCity} seconds
    // This function terminates when player leaves the game
    IEnumerator CityTimer() {
        while (!leavingTheGame) {
            if (!birdDied)
            {
                yield return new WaitForSeconds(timerForCity);
                if (!birdDied)
                {
                    InitializeCity();
                }
            }
            else
            {
                yield break;   
            }
        }
    }

    // This function changes Day-Night
    internal void ChangeDayNight() {
        foreach (var city in Cities)
        {
            city.SetDay(!city.IsDay());
        }
    }

    // This function is subscribed to BirdDied event
    internal void BirdDied() {
        birdDied = true;
    }
}