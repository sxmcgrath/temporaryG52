using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float walkSpeed = 4.0f, jumpForce = 4.0f;

    // Max distance ray can shoot. Shoots from center of player straight down to check if bottom of player is
    // touching ground. (E.g. if center of player to bottom of player is 1.5f, then add .1f to account for slopes)
    [SerializeField] private float jumpRaycastDistance = 1.6f;

    private Rigidbody rb;

    // Set up initial references
    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        Jump();
    }

    // Same as Update but works on every physics step
    private void FixedUpdate() {
        Move();
    }

    private void Move() {
        // Store Raw Directional input and normalize so diagonal movement is not faster
        Vector2 inputDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        inputDir.Normalize();

        // Move Player by velocity
        Vector3 velocity = new Vector3(inputDir.x, 0.0f, inputDir.y) * walkSpeed * Time.fixedDeltaTime;
        // Ensure direction is oriented to where player is facing
        Vector3 newPosition = rb.position + rb.transform.TransformDirection(velocity);

        rb.MovePosition(newPosition);
    }

    // This implementation may have difficulties with double jumps (mid air jumps)
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
