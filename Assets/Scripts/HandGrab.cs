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
    List<bool> fingersClosed = null;

    private AudioSource audioSource;

    private void Awake() {
        fingersQuat = new List<Quaternion>();
        for (int i = 0; i < fingers.Count; i++) {
            fingersQuat.Add(fingers[i].transform.localRotation);
        }

        fingersClosed = new List<bool>();
        for (int i = 0; i < fingers.Count/2; i++) {
            fingersClosed.Add(false);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource) {
            audioSource.playOnAwake = false;
        }
    }

    float tThumbs, tIndex, tMiddle, tRing, tPinkie;

    private void Update() {
        if (Input.GetKey(KeyCode.Space)) { fingersClosed[0] = true;  tThumbs += Time.deltaTime * 2; }
                                    else { fingersClosed[0] = false; tThumbs -= Time.deltaTime * 4; }
        if (Input.GetKey(KeyCode.R)) { fingersClosed[1] = true;  tIndex += Time.deltaTime * 2; }
                                else { fingersClosed[1] = false; tIndex -= Time.deltaTime * 4; }
        if (Input.GetKey(KeyCode.E)) { fingersClosed[2] = true;  tMiddle += Time.deltaTime * 2; }
                                else { fingersClosed[2] = false; tMiddle -= Time.deltaTime * 4; }
        if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.W)) { fingersClosed[3] = true;  tRing += Time.deltaTime * 2; }
                                else { fingersClosed[3] = false; tRing -= Time.deltaTime * 4; }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q)) { fingersClosed[4] = true;  tPinkie += Time.deltaTime * 2; }
                                else { fingersClosed[4] = false; tPinkie -= Time.deltaTime * 4; }
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

        // Is hand fully closed?
        if (AreAllFingersClosed()) {
            // If it is, does it already hold something ?
            // If not, is there something in range?
            if (grabbed == null && inRange != null) {
                // If it is, grab this thing !
                grabbed = inRange.GetComponent<Grabbable>();
                grabbed.SetGrabbedBy(this);
            }
            // If it is, DO NOTHING
        }
        else {
            // If not, does it already hold something ?
            if (grabbed != null) {
                // If it is, ungrab !
                grabbed.Ungrab();
                grabbed = null;
            }
            // If not, DO NOTHING
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

    public bool AreAllFingersClosed() {
        bool closed = true;
        foreach (bool finger in fingersClosed) {
            closed = closed && finger;
        }
        return closed;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Grabbable")
            inRange = other;

        if (audioSource) {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
        }
    }

    void OnTriggerExit(Collider other) {
        if (inRange == other)
            inRange = null;
    }
}
