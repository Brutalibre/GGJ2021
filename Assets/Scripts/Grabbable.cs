using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private bool isGrabbed = false;
    private HandGrab grabbedBy = null;
    private Rigidbody rb;

    private AudioSource audioSource;
    public AudioClip bounce;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        if(audioSource) {
            audioSource.playOnAwake = false;
            audioSource.clip = bounce;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed && grabbedBy != null) {
            // transform.position = Vector3.Lerp(transform.position, grabbedBy.position, Time.deltaTime);
            transform.position = grabbedBy.GrabbedObjectDestination.position;
        }
    }

    void OnCollisionEnter() {
        if (audioSource) {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.Play();
        }
    }

    public void SetGrabbedBy(HandGrab hand) {
        isGrabbed = true;
        grabbedBy = hand;
        rb.useGravity = false;
        gameObject.layer = 7; // IgnoreArm layer

        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void Ungrab() {
        isGrabbed = false;
        grabbedBy = null;
        rb.useGravity = true;
        gameObject.layer = 0; // Default layer

        rb.constraints = RigidbodyConstraints.None;
    }
}
