using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipePrefabScript : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] float tresholdX = -3;

    public bool isAvaiable;

    private void Update() {
        if (tresholdX <= transform.position.x) {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, 0);
        } else {
            isAvaiable = true;
        }
    }
}
