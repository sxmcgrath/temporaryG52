using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueSwing : MonoBehaviour {
    
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask grappleableLayers;
    public Transform tongueTip, frogViewCam, player;
    private SpringJoint joint;

    private float maxDistance = 100.0f;
    private Vector3 currentGrapplePosition;

    void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            StartGrapple();
            
        } else if (Input.GetMouseButtonUp(0)) {
            StopGrapple();
        }
    }

    void LateUpdate() {
        DrawTongue();
    }

    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(frogViewCam.position, frogViewCam.forward, out hit, maxDistance, grappleableLayers)) {
            if (hit.collider != null) {
                grapplePoint = hit.point;
                joint = player.gameObject.AddComponent<SpringJoint>();
                joint.autoConfigureConnectedAnchor = false;
                joint.connectedAnchor = grapplePoint;

                float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

                // The distance range that tongue will stay between
                joint.maxDistance = distanceFromPoint * 0.4f;
                joint.minDistance = distanceFromPoint * 0.20f;

                // Important properties for feel of grapple
                joint.spring = 4.5f;
                joint.damper = 7.0f;
                joint.massScale = 4.5f;

                lr.positionCount = 2;
                currentGrapplePosition = tongueTip.position;
            }
        }
    }

    void DrawTongue() {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, tongueTip.position);
        lr.SetPosition(1, grapplePoint);
    }

    void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
    }
}
