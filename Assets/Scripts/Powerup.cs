using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PowerUpsAvailable {
    Speed,
    MaxTongueLength,
    StrongTongue
}

public class Powerup : MonoBehaviour {
    public PowerUpsAvailable powerUpType;
    public float speedBuff = 4.0f, tongueLengthMultiplier = 2.0f;
    public float buffDuration = 10.0f, amplitude = 0.5f, frequency = 1f;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    public float degreesPerSecond = 20.0f;
    // public GameObject pickupEffect;

    void Start() {
        posOffset = transform.position;
    }
    void Update() {
        transform.Rotate(Vector3.up*degreesPerSecond*Time.deltaTime);

        tempPos = posOffset;
        tempPos.y += Mathf.Sin(frequency * Mathf.PI * Time.fixedTime) * amplitude;

        transform.position = tempPos;
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            StartCoroutine(Pickup(other));
        }
    }

    IEnumerator Pickup(Collider player) {
        // Spawn cool effect
        // Instantiate(pickupEffect, transform.position, transform.rotation);

        PlayerMovement movement = player.gameObject.GetComponent<PlayerMovement>();
        TongueSwing tongue = player.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<TongueSwing>();
        // Apply effect to player
        if (powerUpType == PowerUpsAvailable.Speed) {
            movement.setSpeed(movement.getSpeed() + speedBuff);
        } else if (powerUpType == PowerUpsAvailable.MaxTongueLength) {
            tongue.setMaxTongueLength(tongue.getMaxTongueLength() * tongueLengthMultiplier);
        } else if (powerUpType == PowerUpsAvailable.StrongTongue) {
            tongue.setStrongTongue(true);
        }
        
        MeshRenderer[] meshes = GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer mesh in meshes) {
            mesh.enabled = false;
        }
        Collider[] colliders = GetComponentsInChildren<Collider>();
        foreach(Collider collide in colliders) {
            collide.enabled = false;
        }

        // Wait for powerup to run out
        yield return new WaitForSeconds(buffDuration);

        // Revert player speed.
        if (powerUpType == PowerUpsAvailable.Speed) {
            movement.setSpeed(movement.getSpeed() - speedBuff);
        } else if (powerUpType == PowerUpsAvailable.MaxTongueLength) {
            tongue.setMaxTongueLength(tongue.getMaxTongueLength() / tongueLengthMultiplier);
        } else if (powerUpType == PowerUpsAvailable.StrongTongue) {
            tongue.setStrongTongue(false);
        }

        // Remove power up object
        Destroy(gameObject);
    }
}
