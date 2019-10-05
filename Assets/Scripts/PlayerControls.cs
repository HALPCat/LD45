using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {

    public RacerScript racerScript;
    // Start is called before the first frame update
    void Start() {
    
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetAxisRaw("Jump") > 0) {
            racerScript.Accelerate();
        } else {
            racerScript.Deaccelerate();
        }
    }
}
