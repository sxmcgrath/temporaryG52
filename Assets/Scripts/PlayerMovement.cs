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
    [SerializeField] private float jumpRaycastDistance = 1.6f;
    public float fallMult = 2.0f;
    public float swingMult = 1.5f;
    private Rigidbody rb;

    // Set up initial references
    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        Jump();

        // If you are falling, increase gravity by fallMult, if you're swinging and going down, increase gravity by swingMult
        if (rb.velocity.y < 0 && !Input.GetMouseButton(0)) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMult - 1) * Time.deltaTime;
        } else if (rb.velocity.y < 0 && Input.GetMouseButton(0)) {
            rb.velocity += Vector3.up * Physics.gravity.y * (swingMult - 1) * Time.deltaTime;
        }
    }

    // Same as Update but works on every physics step
    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        // Store Raw Directional input and normalize so diagonal movement is not faster
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();

        // Get player velocity based on direction and speed
        Vector3 velocity = new Vector3(inputDir.x, 0.0f, inputDir.y) * walkSpeed * Time.fixedDeltaTime;

        // If player is swinging, player movement will be based on velocity to make swinging smoother.
        // If player is not swinging, then simply update position.
        // Note: TransformDirection is to ensure direction is oriented to where player is facing.
        if (Input.GetMouseButton(0)) {
            rb.velocity += rb.transform.TransformDirection(velocity);
        } else {
            Vector3 newPosition = rb.position + rb.transform.TransformDirection(velocity);

            rb.MovePosition(newPosition);
        }
        
        
    }

    // This implementation may have difficulties with double jumps (mid air jumps). Makes player jump if on ground.
    private void Jump() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (isGrounded()) {
                rb.AddForce(0.0f, jumpForce, 0.0f, ForceMode.Impulse);
            }
        }
    }

    // Use raycast to check if there is ground below player.
    private bool isGrounded() {
        return Physics.Raycast(transform.position, Vector3.down, jumpRaycastDistance);
    }
}
