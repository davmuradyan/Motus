using UnityEngine;

internal interface ISpawnable {
    public bool isAvailable { get; set; }
    public bool IsAvailable();
    public float speed { get; set; }
    public GameObject gameObject {  get; set; }
}