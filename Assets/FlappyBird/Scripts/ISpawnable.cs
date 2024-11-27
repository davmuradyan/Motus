using UnityEngine;

internal interface ISpawnable {
    public bool isAvailable { get; set; }
    public bool IsAvailable();
    public GameObject gameObject {  get; set; }
}