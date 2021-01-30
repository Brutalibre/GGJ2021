using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrab : MonoBehaviour
{
    public Collider handTrigger;
    private Grabbable grabbed = null;
    private Collider inRange = null;
    public Transform GrabbedObjectDestination;

    public List<GameObject> fingers;
    List<Quaternion> fingersQuat = null;

    private void Awake() {
        fingersQuat = new List<Quaternion>();
        for (int i = 0; i < fingers.Count; i++) {
            fingersQuat.Add(fingers[i].transform.localRotation);
        }
    }

    float tThumbs, tIndex, tMiddle, tRing, tPinkie;

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) tThumbs += Time.deltaTime * 2; else tThumbs -= Time.deltaTime * 4;
        if (Input.GetKey(KeyCode.R)) tIndex += Time.deltaTime * 2; else tIndex -= Time.deltaTime * 4;
        if (Input.GetKey(KeyCode.E)) tMiddle += Time.deltaTime * 2; else tMiddle -= Time.deltaTime * 4;
        if (Input.GetKey(KeyCode.Z)) tRing += Time.deltaTime * 2; else tRing -= Time.deltaTime * 4;
        if (Input.GetKey(KeyCode.A)) tPinkie += Time.deltaTime * 2; else tPinkie -= Time.deltaTime * 4;
        tThumbs = Mathf.Clamp(tThumbs, 0, 1);
        tIndex = Mathf.Clamp(tIndex, 0, 1);
        tMiddle = Mathf.Clamp(tMiddle, 0, 1);
        tRing = Mathf.Clamp(tRing, 0, 1);
        tPinkie = Mathf.Clamp(tPinkie, 0, 1);
        CloseThumbs(tThumbs);
        CloseFinger(1, tIndex);
        CloseFinger(2, tMiddle);
        CloseFinger(3, tRing);
        CloseFinger(4, tPinkie);

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

    public void CloseThumbs(float t) {
        fingers[0].transform.localRotation
            = Quaternion.Lerp(fingersQuat[0], fingersQuat[0] * Quaternion.Euler(50, 0, 0), t);
        fingers[1].transform.localRotation
            = Quaternion.Lerp(fingersQuat[1], fingersQuat[1] * Quaternion.Euler(50, 0, 0), t);
    }

    public void CloseFinger(int i, float t) {
        fingers[i * 2].transform.localRotation
            = Quaternion.Lerp(fingersQuat[i * 2], fingersQuat[i * 2] * Quaternion.Euler(0, 0, 95), t);
        fingers[i * 2+ 1].transform.localRotation
            = Quaternion.Lerp(fingersQuat[i*2+1], fingersQuat[i*2+1] * Quaternion.Euler(0, 0, -75), t);
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
