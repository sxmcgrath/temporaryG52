using System.IO.Pipes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TongueSwing : MonoBehaviour {
    
    // Assign GameObjects from Inspector
    public Transform tongueTip, frogViewCam, player;
    public LayerMask grappleableLayers;

    // Prepare tongue variables
    private LineRenderer lr;
    private Vector3 grapplePoint, currentGrapplePosition;
    private SpringJoint joint;
    private float maxDistance = 100.0f;

    public GameObject salivaPrefab;

    // Set up initial references
    private void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    // Shoot tongue on mouse click and retract tongue on mouse up. (Tongue remains when holding)
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            ShootTongue();
            
        } else if (Input.GetMouseButtonUp(0)) {
            RetractTongue();
        }
    }

    // Late update as we want to draw the tongue after the positions are taken.
    private void LateUpdate() {
        DrawTongue();
    }

    // Use Raycast to determine info about which object to shoot tongue at.
    private void ShootTongue() {
        RaycastHit hit;
        // Check if there is an object with a layer that the tongue can stick to.
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
                createParticles(hit);
            }
        }
    }

    // Draw the tongue from tongueTip to grapplePoint
    private void DrawTongue() {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, tongueTip.position);
        lr.SetPosition(1, grapplePoint);
        
    }

    // Stop sticking tongue to object
    private void RetractTongue() {
        lr.positionCount = 0;
        Destroy(joint);
    }

    // Create saliva particles at grapple point
    private void createParticles(RaycastHit hit) {  
        GameObject saliva  = Instantiate(salivaPrefab, hit.point, Quaternion.identity);
        saliva.transform.LookAt(hit.point + hit.normal);
        var ps = saliva.GetComponent<ParticleSystem>();
        ps.Play(); 
    }   
}
