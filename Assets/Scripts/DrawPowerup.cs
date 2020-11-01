using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawPowerup : MonoBehaviour {

    public GameObject lengthPowerUp, speedPowerUp, player;
    private List<PowerUpsAvailable> powerUps = new List<PowerUpsAvailable>();
    private Vector2 canvasWidthHeight;
    private float UIBuffer = 5.0f, powerupWidth;

    void Start() {
        RectTransform canvasRect = GetComponent<RectTransform>();
        canvasWidthHeight = new Vector2(canvasRect.rect.width, canvasRect.rect.height);

        RectTransform speedRect = speedPowerUp.GetComponent<RectTransform>();
        RectTransform lengthRect = lengthPowerUp.GetComponent<RectTransform>();
        speedRect.sizeDelta = new Vector2(canvasRect.rect.width/12.5f,canvasRect.rect.width/12.5f);
        lengthRect.sizeDelta = new Vector2(canvasRect.rect.width/12.5f,canvasRect.rect.width/12.5f);

        powerupWidth = canvasRect.rect.width/12.5f;
        
    }

    // Update is called once per frame
    void Update() {
        powerUps = player.GetComponent<PowerupsTaken>().GetPowerUps();

        if (powerUps.Count > 0) {
            for (int i = 0; i < powerUps.Count; i++) {

                if (powerUps[i] == PowerUpsAvailable.MaxTongueLength) {
                    lengthPowerUp.SetActive(true);
                    lengthPowerUp.transform.position = new Vector2((transform.position.x - (canvasWidthHeight.x / 2) + (powerupWidth / 2) + UIBuffer) + ((powerupWidth + UIBuffer) * i), 
                                                                  transform.position.y + (canvasWidthHeight.y / 2) - (powerupWidth / 2) - UIBuffer);
                } else if (powerUps[i] == PowerUpsAvailable.Speed) {
                    speedPowerUp.SetActive(true);
                    speedPowerUp.transform.position = new Vector2((transform.position.x - (canvasWidthHeight.x / 2) + (powerupWidth / 2) + UIBuffer) + ((powerupWidth + UIBuffer) * i), 
                                                                  transform.position.y + (canvasWidthHeight.y / 2) - (powerupWidth / 2) - UIBuffer);
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
