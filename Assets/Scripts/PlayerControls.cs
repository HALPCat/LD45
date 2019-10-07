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
        if (Input.GetAxisRaw("Vertical") == 1) {
            racerScript.Accelerate();
        } else {
            racerScript.Deaccelerate();
        }

        if (Input.GetAxisRaw("Vertical") == -1) {
            racerScript.Deaccelerate();
        }

        if (Input.GetAxisRaw("Horizontal") == 1) {
            racerScript.TurnRight();
        } else if (Input.GetAxisRaw("Horizontal") == -1) {
            racerScript.TurnLeft();
        }
    }
}
