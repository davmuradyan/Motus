using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScript : MonoBehaviour, ISpawnable
{
    [Header("Variables")]
    [SerializeField] float speed = 1;
    [SerializeField] float tresholdX = -3;
    public bool isAvailable {  get; set; }
    public GameObject gameObject { get; set; }
    internal event Action NewBase;

    [Header("Background Spawner")]
    [SerializeField] private BackgroundSpawnerScript backgroundSpawner;
    private void Start() {
        gameObject = transform.gameObject;
    }

    private void Update() {
        if (tresholdX <= transform.position.x) {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, 0);
        } else {
            isAvailable = true;
        }
    }

    public bool IsAvailable() {
        return isAvailable;
    }
}
