using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacerScript : MonoBehaviour {
    public float speed;
    public float maxSpeed;
    public float accelerationSpeed;
    public float deAccelerationSpeed;

    public float rotationSpeed;
    public int points;
    public int parts;
    public bool canFinish = false;

    public CharacterController characterController;

    private int finishLayer;
    private int halfwayLayer;

    void Start() {
        finishLayer = LayerMask.NameToLayer("RaceLineFinish");
        halfwayLayer = LayerMask.NameToLayer("RaceLineHalfway");
        characterController = GetComponent<CharacterController>();
    }

    public void Accelerate() {
        if (speed < maxSpeed) {
            speed += accelerationSpeed * Time.deltaTime;
            if (speed > maxSpeed) {
                speed = maxSpeed;
            }
        }
    }

    public void Deaccelerate() {
        if (speed > 0) {
            speed -= deAccelerationSpeed * Time.deltaTime;
            if (speed < 0) {
                speed = 0;
            }
        }
    }

    public void DeaccelerateUntil(float minimumSpeed)
    {
        if (speed > minimumSpeed) {
            speed -= deAccelerationSpeed * Time.deltaTime;
            if (speed < minimumSpeed) {
                speed = minimumSpeed;
            }
        }
    }

    public void TurnRight() {
        transform.Rotate(0, rotationSpeed*Time.deltaTime, 0);
    }

    public void TurnLeft() {
        transform.Rotate(0, -rotationSpeed*Time.deltaTime, 0);
    }

    public void TurnTowards(Vector3 target)
    {
        Vector3 targetDir = target - transform.position;
        float step = rotationSpeed * Mathf.Deg2Rad * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);
        transform.rotation = Quaternion.LookRotation(newDir);
    }

    public void Update() {
        if(characterController != null)
        {
            characterController.Move(transform.forward*Time.deltaTime*speed);
        }
        else{
            transform.Translate(transform.forward*Time.deltaTime*speed , Space.World);
        }

        Vector3 newPos = new Vector3(transform.position.x, 0, transform.position.z);
        transform.position = newPos;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == halfwayLayer)
        {
            canFinish = true;
        }
        if(other.gameObject.layer == finishLayer & canFinish)
        {
            points++;
            canFinish = false;
        }
    }
}
