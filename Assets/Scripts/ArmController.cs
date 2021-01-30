using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {
    public ArticulationBody body;
    public float sensitivity = 10;
    public Camera main;
    public BoxCollider plane;
    public float planeDistance;
    public float scalingDest = 4;

    void FixedUpdate() {
        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out RaycastHit pointHit, planeDistance)) {
            Vector3 destination = new Vector3(pointHit.point.x * scalingDest, body.transform.position.y, pointHit.point.z * scalingDest);
            Vector3 direction = (destination - body.transform.position) * sensitivity;

            body.velocity = direction;
            /*body.angularVelocity = new Vector3 ((-direction.x) / body.inertiaTensor.x,
                                                (-direction.y) / body.inertiaTensor.y,
                                                (-direction.z) / body.inertiaTensor.z);*/
        }
    }
}
