using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
    public ArticulationBody body;
    public float sensitivity = 10;
    public Camera main;
    // public BoxCollider planeold;
    public float planeDistance;
    public float scalingDest = 4;
    Plane plane;

    private void Start() {
        plane = new Plane(main.transform.position - new Vector3(0, planeDistance-1, 0), -main.transform.forward);
    }

    void FixedUpdate() {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out float distance)) {
            Vector3 pointHit = ray.GetPoint(distance);
            Vector3 destination = new Vector3(pointHit.x * scalingDest, body.transform.position.y, pointHit.z * scalingDest);
            Vector3 direction = (destination - body.transform.position) * sensitivity;

            body.velocity = direction;
        }
    }
}
