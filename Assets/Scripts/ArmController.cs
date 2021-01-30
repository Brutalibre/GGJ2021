using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
    public ArticulationBody body;
    
    void FixedUpdate() {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        body.velocity = new Vector3(inputX, body.velocity.y, inputY); 
    }
}
