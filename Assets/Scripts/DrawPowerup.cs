using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPowerup : MonoBehaviour {

    public GameObject lengthPowerUp, speedPowerUp, player;
    private List<PowerUpsAvailable> powerUps = new List<PowerUpsAvailable>();
    private Vector2 canvasWidthHeight, powerupWidthHeight;
    private float UIBuffer = 5.0f;

    void Start() {
        RectTransform canvasRect = GetComponent<RectTransform>();
        canvasWidthHeight = new Vector2(canvasRect.rect.width, canvasRect.rect.height);

        RectTransform powerupRect = speedPowerUp.GetComponent<RectTransform>();
        powerupWidthHeight = new Vector2(powerupRect.rect.width, powerupRect.rect.height);
        
    }

    // Update is called once per frame
    void Update() {
        powerUps = player.GetComponent<PowerupsTaken>().GetPowerUps();

        if (powerUps.Count > 0) {
            for (int i = 0; i < powerUps.Count; i++) {

                if (powerUps[i] == PowerUpsAvailable.MaxTongueLength) {
                    lengthPowerUp.SetActive(true);
                    lengthPowerUp.transform.position = new Vector2((transform.position.x - (canvasWidthHeight.x / 2) + (powerupWidthHeight.x / 2) + UIBuffer) + (powerupWidthHeight.x * i), 
                                                                  transform.position.y + (canvasWidthHeight.y / 2) - (powerupWidthHeight.y / 2) - UIBuffer);
                } else if (powerUps[i] == PowerUpsAvailable.Speed) {
                    speedPowerUp.SetActive(true);
                    speedPowerUp.transform.position = new Vector2((transform.position.x - (canvasWidthHeight.x / 2) + (powerupWidthHeight.x / 2) + UIBuffer) + (powerupWidthHeight.x * i), 
                                                                  transform.position.y + (canvasWidthHeight.y / 2) - (powerupWidthHeight.y / 2) - UIBuffer);
                }
                if (!powerUps.Contains(PowerUpsAvailable.MaxTongueLength)) {
                    lengthPowerUp.SetActive(false);
                }
                if (!powerUps.Contains(PowerUpsAvailable.Speed)) {
                    speedPowerUp.SetActive(false);
                }
            }
        } else {
            speedPowerUp.SetActive(false);
            lengthPowerUp.SetActive(false);
        }
    }

   
}
