using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

    public Transform target;
    public bool debug;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public RacerScript racerScript;
    public ParticleSystem ps;

    public enum SpriteAngle
    {
        front = 0,
        right = 1,
        left = 2,
        behind = 3
    }
    public SpriteAngle spriteAngle;

    // Start is called before the first frame update
    void Start() {
        target = Camera.main.transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        racerScript = transform.parent.GetComponent<RacerScript>();
    }

    // Update is called once per frame
    void Update() {
        float angleToCamera = Vector3.SignedAngle(transform.parent.forward, Camera.main.transform.forward,Vector3.up);
        
        transform.rotation = target.rotation;


        if(debug)
        {
            Debug.Log(angleToCamera);
        }

        if(angleToCamera > 45 & angleToCamera < 135)
        {
            spriteAngle = SpriteAngle.left;
        }else if(angleToCamera < -45 & angleToCamera > -135)
        {
            spriteAngle = SpriteAngle.right;
        }
        else if(angleToCamera >= 135 | angleToCamera <= -135)
        {
            spriteAngle = SpriteAngle.front;
        }
        else{
            spriteAngle = SpriteAngle.behind;
        }

        if(spriteAngle == SpriteAngle.left){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }

        animator.SetInteger("SpriteAngle", (int)spriteAngle);
        animator.speed = racerScript.speed;
    }

    public void Kick()
    {
        //ps.Play();
        Debug.Log("kick");
    }
}
