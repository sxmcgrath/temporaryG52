/* 
    References:
    - https://www.youtube.com/watch?v=_QajrabyTJc
    - https://www.youtube.com/watch?v=1o-Gawy3D48
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    // Make player speed and jump adjustable in inspector
    [SerializeField] private float walkSpeed = 4.0f, jumpForce = 4.0f;

    // Max distance ray can shoot. Shoots from center of player straight down to check if bottom of player is
    // touching ground. (E.g. if center of player to bottom of player is 1.5f, then add .1f to account for slopes)
    [SerializeField] private float jumpRaycastDistance = 1.1f;
    public float fallMult = 2.0f, maxVelocity = 80.0f;
    public float swingMult = 1.5f;
    private Rigidbody rb;
    private bool jumpBuffered;


    public float getSpeed() {
        return walkSpeed;
    }
    public void setSpeed(float newSpeed) {
        walkSpeed = newSpeed;
    }

    // Set up initial references
    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            jumpBuffered = true;
        }

        // If you are falling, increase gravity by fallMult, if you're swinging and going down, increase gravity by swingMult
        if (rb.velocity.y < 0 && !Input.GetMouseButton(0)) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMult - 1) * Time.deltaTime;
        } else if (rb.velocity.y < 0 && Input.GetMouseButton(0)) {
            rb.velocity += Vector3.up * Physics.gravity.y * (swingMult - 1) * Time.deltaTime;
        }
    }

    private void FixedUpdate() {
        Move();
        Jump();
    }

    private void Move() {
        // Store Raw Directional input and normalize so diagonal movement is not faster
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();

        // Get player velocity based on direction and speed
        Vector3 velocity = new Vector3(inputDir.x, 0.0f, inputDir.y) * walkSpeed;

        // If player is swinging, player movement will be based on velocity to make swinging smoother.
        // If player is not swinging, and not on ground, move by addinig force. Limit to a max velocity.
        // If player is on ground, then simply update position.
        // Note: TransformDirection is to ensure direction is oriented to where player is facing.
        if (Input.GetMouseButton(0)) {
            rb.velocity += rb.transform.TransformDirection(velocity * Time.fixedDeltaTime);
        } else if (!isGrounded()) {
            // if (rb.velocity.x < 1.0f) {
            //     rb.AddForce(rb.transform.TransformDirection(velocity * 5.0f));
            // } else 
            if (rb.velocity.magnitude < maxVelocity) {
                rb.AddForce(rb.transform.TransformDirection(velocity * 2.0f));
            }
        } else {
            Vector3 newPosition = rb.position + rb.transform.TransformDirection(velocity * Time.fixedDeltaTime);
            rb.MovePosition(newPosition);
        }
    }

    // This implementation may have difficulties with double jumps (mid air jumps). Makes player jump if on ground.
    // If player is on ground when they jump, and is moving forward or backward, then give them some forward momentum.
    // Otherwise if player is not moving forward/back and just jumps, go vertically up only.
    private void Jump() {
        if (jumpBuffered) {
            if (isGrounded() && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1) {
            Vector3 forwardJump = new Vector3(0.0f, jumpForce, walkSpeed * 1.5f);
            rb.AddForce(rb.transform.TransformDirection(forwardJump), ForceMode.Impulse);
            } else if (isGrounded()) {
                rb.AddForce(0.0f, jumpForce, 0.0f, ForceMode.Impulse);
            }
            jumpBuffered = false;
        }
        
    }

    // Use SphereCast to check if there is ground below player. Better than raycast as it account for slopes better
    private bool isGrounded() {
        RaycastHit hitInfo;
        return Physics.SphereCast(transform.position, 1.0f/2.0f, Vector3.down, out hitInfo, jumpRaycastDistance);
    }

}
