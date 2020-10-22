/*
    References:
    - https://www.youtube.com/watch?v=1o-Gawy3D48
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCam : MonoBehaviour
{
    // Store Serializable fields
    [SerializeField] private Transform playerCamera = null;
    [SerializeField] private float mouseSensitivity = 3.5f;
    [SerializeField] private bool lockCursor = true;

    // Set Camera pitch to look forward at start
    private float cameraPitch = 0.0f;
    private bool isPlaying = true;

    // Start is called before the first frame update
    private void Start()
    {
        // Lock cursor to middle of screen and make invisible if lockCursor set to true
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }



    public void gamePausing()
    {
        isPlaying = false;
        Time.timeScale = 0;
    }

    public void gameResuming()
    {
        isPlaying = true;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isPlaying)
        {
            UpdateCameraView();
        }
    }

    // Rotate player around Y axis when looking horizontally, but rotate camera around X axis when
    // looking vertically.
    private void UpdateCameraView()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // Vector3.up is shorthand for (0, 1, 0).
        transform.Rotate(Vector3.up * mouseDelta.x * mouseSensitivity);

        // The vertical mouseDelta is -ve when movining down and +ve when moving up.
        // However the camera's x-rotation increases to pitch down and decreases to pitch up.
        // This means the values are inverted. So do -= to apply the inverse of the mouseDelta.
        cameraPitch -= mouseDelta.y * mouseSensitivity;

        // Camera pitch is -90 when looking directly up, 0 when looking forward and 90 when
        // looking directly down. Clamp the values so camera cannot go further than this.
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        // Rotate playerCamera around x axis by the cameraPitch
        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
    }

}
