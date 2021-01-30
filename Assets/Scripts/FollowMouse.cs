using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    public Rigidbody body;
    public float sensitivity = 10;
    public Camera main;
    public BoxCollider plane;
    public float planeDistance;

    void FixedUpdate() {
        /*float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        body.velocity += new Vector3(inputX, 0, inputY) * 0.01f; */

        /*

        Vector3 mousePos = Input.mousePosition;
        Vector3 destination = main.ScreenToWorldPoint(new Vector3(mousePos.x, body.transform.position.y, mousePos.y));

        Debug.Log("mouse=" + mousePos);
        Debug.Log("dest=" + destination);

        body.velocity = (destination - body.transform.position) * sensitivity;*/

        Ray ray = main.ScreenPointToRay(Input.mousePosition);
        if (plane.Raycast(ray, out RaycastHit pointHit, planeDistance)) {
            body.velocity = (pointHit.point - body.transform.position) * sensitivity;
        }
    }
}
