using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedVFX : MonoBehaviour {

    public Transform speedParticle;
    public Rigidbody rb;

    // Update is called once per frame
    void Update() {
        // Debug.Log(rb.velocity);
        // Debug.Log(rb.velocity.magnitude);
        // Debug.Log(rb.velocity);
        // Debug.Log(rb.gameObject.transform.rotation);
        // Debug.Log(transform.rotation);

        if (rb.velocity.magnitude > 12.0f && rb.velocity.y < -14.0f) {
            speedParticle.gameObject.SetActive(true);
            speedParticle.position = transform.position;
            // speedParticle.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.0f, transform.rotation.w);
            speedParticle.rotation = Quaternion.LookRotation(rb.velocity);
            speedParticle.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        } else if (rb.velocity.magnitude > 12.0f) {
            speedParticle.gameObject.SetActive(true);
            speedParticle.position = transform.position + transform.forward * 24.0f;
            // speedParticle.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0.0f, transform.rotation.w);
            speedParticle.rotation = Quaternion.LookRotation(new Vector3(rb.velocity.x, 0.0f, rb.velocity.z));
            speedParticle.Rotate(0.0f, 180.0f, 0.0f, Space.Self);
        }else {
            speedParticle.gameObject.SetActive(false);
        }
    }
}
