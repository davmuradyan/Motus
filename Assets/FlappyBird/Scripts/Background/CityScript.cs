using UnityEngine;
using UnityEngine.UI;

public class CityScript : MonoBehaviour, ISpawnable
{
    [SerializeField] float speed = 1;
    [SerializeField] float tresholdX = -3;
    [SerializeField] private Sprite CityDay;
    [SerializeField] private Sprite CityNight;
    public GameObject gameObject { get; set; }
    public bool isAvailable { get; set; }
    public bool isDay;

    private void Start() {
        isDay = true;
        isAvailable = false;
        gameObject = transform.gameObject;
    }

    private void Update() {
        if (tresholdX <= transform.position.x) {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, 3);
        } else {
            isAvailable = true;
        }

        if (isDay)
        {
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = CityDay;
        }
        else
        {
           
            transform.gameObject.GetComponent<SpriteRenderer>().sprite = CityNight;
        }
    }

    public bool IsAvailable() {
        return isAvailable;
    }

    internal void SetDay(bool val) {
        isDay=val;
    }

    internal bool IsDay() { return isDay; }
}
