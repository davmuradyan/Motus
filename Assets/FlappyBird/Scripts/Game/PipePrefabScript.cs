using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePrefabScript : MonoBehaviour, ISpawnable
{
    [SerializeField] float speed = 1;
    [SerializeField] float tresholdX = -3;

    public bool isAvailable {  get; set; }
    public GameObject gameObject { get; set; }
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
