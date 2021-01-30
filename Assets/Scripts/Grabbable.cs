using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private bool isGrabbed = false;
    private HandGrab grabbedBy = null;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed && grabbedBy != null) {
            // transform.position = Vector3.Lerp(transform.position, grabbedBy.position, Time.deltaTime);
            transform.position = grabbedBy.GrabbedObjectDestination.position;
        }
    }

    public void SetGrabbedBy(HandGrab hand) {
        isGrabbed = true;
        grabbedBy = hand;
        rb.useGravity = false;
        gameObject.layer = 7; // IgnoreArm layer
    }

    public void Ungrab() {
        isGrabbed = false;
        grabbedBy = null;
        rb.useGravity = true;
        gameObject.layer = 7; // Default layer
    }
}
