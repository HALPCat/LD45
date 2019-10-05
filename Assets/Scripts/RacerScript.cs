using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerScript : MonoBehaviour {
    public float speed;
    public float maxSpeed;
    public float accelerationSpeed;
    public int points;
    public int parts;

    public void Accelerate() {
        if (speed < maxSpeed) {
            speed += accelerationSpeed * Time.deltaTime;
        } else if (speed > maxSpeed) {
            speed = maxSpeed;
        }
    }

    public void Deaccelerate() {
        if (speed > 0) {
            speed -= accelerationSpeed * Time.deltaTime;
        } else if (speed < 0) {
            speed = 0;
        }
    }

    public void Update() {
        transform.Translate(transform.forward*Time.deltaTime*speed);
    }
}
