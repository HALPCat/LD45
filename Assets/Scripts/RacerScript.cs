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
    public bool kartMode = false;
    public bool canFinish = false;

    public CharacterController characterController;

    private int finishLayer;
    private int halfwayLayer;
    private int playerLayer;

    public bool dead = false;

    public ParticleSystem ps;

    void Start() {
        finishLayer = LayerMask.NameToLayer("RaceLineFinish");
        halfwayLayer = LayerMask.NameToLayer("RaceLineHalfway");
        playerLayer = LayerMask.NameToLayer("Player");
        characterController = GetComponent<CharacterController>();
        ps = GetComponentInChildren<ParticleSystem>();
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
            if(points >= 3)
            {
                points = 3;
                maxSpeed = 4;
                kartMode = true;
                GetComponentInChildren<Animator>().SetBool("KartMode", true);

                foreach(SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>())
                {
                    if(!rend.name.Contains("Player"))
                    {
                        rend.enabled = false;
                    }
                }
            }else{
                maxSpeed = maxSpeed / 1.5f;
                speed = 0;
                if(GetComponentInChildren<FaceCamera>().isPlayer)
                {
                    GetComponent<AudioSource>().Play();
                }
                foreach(SpriteRenderer rend in GetComponentsInChildren<SpriteRenderer>())
                {
                    if(rend.name.Equals("Part1"))
                    {
                        if(points == 1){
                            rend.enabled = true;
                        }
                    }else if(rend.name.Equals("Part2")){
                        if(points == 2){
                            rend.enabled = true;
                        }
                    }
                }
            }
        }
        if(other.gameObject.layer == playerLayer)
        {
            Debug.Log("Collision");
            if(kartMode & other.gameObject.GetComponentInChildren<RacerScript>() != this)
            {
                other.gameObject.GetComponentInParent<RacerScript>().Die();
            }
        }
    }

    public void Die()
    {
        if(!dead)
        {
            dead = true;
            speed = 0;
            maxSpeed = 0;
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GetComponentInChildren<AudioSource>().enabled = false;
            ps.Play();
            if(GetComponentInChildren<FaceCamera>().isPlayer){
                GameManager.instance.LoseGame();
            }else{
                GameManager.instance.botsDead++;
                if(GameManager.instance.botsDead >= 3)
                {
                    GameManager.instance.WinGame();
                }
            }
        }
    }
}
