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
    public bool isPlayer;
    public AudioSource audioSource;
    public AudioClip carSound;

    public Vector3 localForward;

    public enum SpriteAngle
    {
        N = 0,
        E = 1,
        S = 2,
        W = 3
    }
    public SpriteAngle spriteAngle;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        target = Camera.main.transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        racerScript = transform.parent.GetComponent<RacerScript>();

        if(!GetComponentInParent<PlayerControls>())
        {
            isPlayer = false;
        }else
        {
            if(GetComponentInParent<PlayerControls>().enabled)
            {
                isPlayer = true;
            }else{
                isPlayer = false;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        float angleToCamera = Vector3.SignedAngle(transform.parent.forward, Camera.main.transform.forward,Vector3.up);
        
        transform.rotation = target.rotation;


        if(!isPlayer)
        {
            localForward = Camera.main.transform.rotation * transform.parent.forward;
        }else
        {
            localForward = Vector3.forward;
        }
        animator.SetFloat("Horizontal", localForward.x);
        animator.SetFloat("Vertical", localForward.z);

        Debug.DrawRay(transform.position, localForward, Color.cyan);

        if(localForward.x >= 0)
        {
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }

        if(debug)
        {
            Debug.Log(Vector3.Dot(transform.parent.forward, Camera.main.transform.forward));
        }

        /*
        //Left side
        if(angleToCamera > 45 & angleToCamera < 135)
        {
            spriteAngle = SpriteAngle.W;
        }
        //Right side
        else if(angleToCamera < -45 & angleToCamera > -135)
        {
            spriteAngle = SpriteAngle.E;
        }
        //Forward
        else if(angleToCamera >= 135 | angleToCamera <= -135)
        {
            spriteAngle = SpriteAngle.S;
        }
        //Behind
        else{
            spriteAngle = SpriteAngle.N;
        }

        if(spriteAngle == SpriteAngle.W){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }

        */

        //animator.SetInteger("SpriteAngle", (int)spriteAngle);
        animator.speed = racerScript.speed;
    }

    public void Kick()
    {
        if(!racerScript.kartMode){
            audioSource.PlayOneShot(audioSource.clip);
        }else
        {
            audioSource.clip = carSound;
            audioSource.PlayOneShot(carSound);
        }
    }
}
