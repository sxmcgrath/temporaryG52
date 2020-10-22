/*
    References:
    - https://stackoverflow.com/questions/59653787/unity-3d-spring-joint-textures
    - https://www.reddit.com/r/Unity3D/comments/a77tcl/physics_based_grappling_hook_using_configurable/
    - https://www.youtube.com/watch?v=Xgh4v1w5DxU&t=1s
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueSwing : MonoBehaviour {
    
    // Assign GameObjects from Inspector
    public Transform tongueTip, frogViewCam, player;
    public LayerMask grappleableLayers;
    public GameObject untargettedCursor, targettedCursor;
    public float maxTongueLength = 30.0f;

    // Prepare tongue variables
    private LineRenderer lr;
    private Vector3 grapplePoint, originGrapplePosition;
    private SpringJoint joint;
    private float tongueLength;
    private RaycastHit grappleable;
    private bool intersecting = false, strongTongue = false;
    private GameObject curAttachedObject;

    public float getMaxTongueLength() {
        return maxTongueLength;
    }
    
    public void setMaxTongueLength(float newTongueLength) {
        maxTongueLength = newTongueLength;
    }

    public bool getStrongTongue() {
        return strongTongue;
    }
    
    public void setStrongTongue(bool isStrongTongue) {
        strongTongue = isStrongTongue;
    }

    // Set up initial references
    private void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    // Shoot tongue on mouse click and retract tongue on mouse up. (Tongue remains when holding)
    private void Update() {
        ChangeCursorIfGrappleable();

        if (!joint && Input.GetMouseButtonDown(0)) {
            ShootTongue();
        } else if (joint && Input.GetMouseButton(0)) {
            IntersectTongue();
        } else if (joint && Input.GetMouseButtonUp(0)) {
            RetractTongue();
        }
    }

    // Late update as we want to draw the tongue after the positions are taken.
    private void LateUpdate() {
        // If a joint exists (tongue is stuck to something) draw out the tongue using line renderer
        if (joint) {
            DrawTongue();
        }
    }

    // Use Raycast to determine info about which object to shoot tongue at.
    private void ShootTongue() {
        RaycastHit hit;
        // Check if there is an object with a layer that the tongue can stick to.
        if (Physics.Raycast(frogViewCam.position, frogViewCam.forward, out hit, maxTongueLength, grappleableLayers)) {
            if (hit.collider != null) {
                curAttachedObject = hit.collider.gameObject;
                grapplePoint = hit.point;

                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grapplePoint;

                float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

                // The distance range that tongue will stay between
                joint.maxDistance = distanceFromPoint * 0.4f;
                joint.minDistance = distanceFromPoint * 0.2f;

                // Important properties for feel of grapple
                joint.spring = 6.0f;
                joint.damper = 10.0f;
                joint.massScale = 4.5f;

                lr.positionCount = 2;
                originGrapplePosition = tongueTip.position;
            }
        } 
    }

    // If aiming at object within maxTongueLength range and is a grappleable layer, then change cursor to show.
    private void ChangeCursorIfGrappleable() {
        if (Physics.Raycast(frogViewCam.position, frogViewCam.forward, out grappleable, maxTongueLength, grappleableLayers)) {
            if (grappleable.collider != null) {
                untargettedCursor.SetActive(false);
                targettedCursor.SetActive(true);
            }
        } else {
            untargettedCursor.SetActive(true);
            targettedCursor.SetActive(false);
        }
    }

    // While swinging, if tongue intersects with an object, move tongue position to intersection point. Only works if on the same object.
    // If intersecting with a different object than what the tongue is currently stuck to, then break the tongue.
    private void IntersectTongue() {
        RaycastHit intersect;
        tongueLength = Vector3.Distance(frogViewCam.position, grapplePoint) - 1.0f;
        Vector3 lineDir = (grapplePoint - tongueTip.position);
        
        if (Physics.Raycast(frogViewCam.position, lineDir.normalized, out intersect, tongueLength, grappleableLayers)) {
            if (intersect.collider != null) {
                if (strongTongue || curAttachedObject == intersect.collider.gameObject) {
                    grapplePoint = intersect.point;
                    joint.connectedAnchor = grapplePoint;

                    intersecting = true;
                    originGrapplePosition = grapplePoint;
                } else {
                    RetractTongue();
                }
            }
        }
    }

    // Draw the tongue from tongueTip to grapplePoint
    private void DrawTongue() {

        if (!intersecting) {
            originGrapplePosition = Vector3.Lerp(originGrapplePosition, grapplePoint, Time.deltaTime * 6.0f);
        }

        lr.SetPosition(0, tongueTip.position);
        lr.SetPosition(1, originGrapplePosition);

        
    }

    // Stop sticking tongue to object
    private void RetractTongue() {
        intersecting = false;
        lr.positionCount = 0;
        Destroy(joint);
    }
}
