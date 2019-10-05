using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerScript : MonoBehaviour {
    public float speed;
    public float maxSpeed;
    public float accelerationSpeed;
    public int points;
    public int parts;

    // Start is called before the first frame update
    void Start() {
        
    }

    public void Accelerate() {
        if (speed < maxSpeed) {
            speed += accelerationSpeed * Time.deltaTime;
        }
    }

    public void Deaccelerate() {
        if (speed > 0) {
            speed -= accelerationSpeed * Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update() {
        
    }
}
