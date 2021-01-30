using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrab : MonoBehaviour
{
    public Collider handTrigger;
    private Grabbable grabbed = null;
    private Collider inRange = null;
    public Transform GrabbedObjectDestination;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            // Is the hand holding something ?
            // Let go of the grabbed object.
            if (grabbed != null) {
                grabbed.Ungrab();
                grabbed = null;
            }
            // If not, is there anything in range?
            else {
                // Grab what is in range
                if (inRange != null) {
                    grabbed = inRange.GetComponent<Grabbable>();
                    grabbed.SetGrabbedBy(this);
                }
                // Close/open the hand.
                else {

                }
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Grabbable")
            inRange = other;
    }

    void OnTriggerExit(Collider other) {
        if (inRange == other)
            inRange = null;
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetMouseButtonUp(0))
            isGrabbing = false;

        if(Input.GetMouseButton(0) && !isGrabbing) {
            if (grabbed != null) {
                Debug.Log("ungrab !");
                grabbed.Ungrab();
                grabbed = null;

                handTrigger.enabled = true;
            }
        }
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("stay");
        if (Input.GetMouseButton(0) && !isGrabbing) {
            isGrabbing = true;

            if (other.tag == "Grabbable" && grabbed == null) {
                Debug.Log("Grab !");

                grabbed = other.GetComponent<Grabbable>();
                grabbed.SetGrabbedBy(transform);

                handTrigger.enabled = false;
                return;
            }
        }
    }*/
}
