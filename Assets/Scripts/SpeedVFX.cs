using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedVFX : MonoBehaviour {

    public Transform speedParticle;
    public Rigidbody rb;
    private Camera cam;

    void Awake() {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update() {
        addSpeedLines();
    }

    // Add Speed lines when falling fast enough. If you are moving at sufficient speed but not vertically down, then add
    // speed lines from direction you are going and increase FOV to make it feel like you are going faster.
    private void addSpeedLines() {
        if (rb.velocity.magnitude > 12.0f && rb.velocity.y < -14.0f) {
            speedParticle.gameObject.SetActive(true);
            speedParticle.position = transform.position;

            speedParticle.rotation = Quaternion.LookRotation(rb.velocity);
            speedParticle.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        } else if (rb.velocity.magnitude > 12.0f && Mathf.Abs(rb.velocity.x) > 5.0f) {
            speedParticle.gameObject.SetActive(true);
            speedParticle.position = transform.position + transform.forward * 24.0f;
            speedParticle.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.0f, transform.rotation.w);
            // speedParticle.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, 0.0f, rb.velocity.z));
            // speedParticle.rotation = Quaternion.LookRotation(rb.velocity);
            speedParticle.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        } else {
            speedParticle.gameObject.SetActive(false);
        }
    }
}
