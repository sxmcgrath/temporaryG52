using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupsTaken : MonoBehaviour {
    private List<PowerUpsAvailable> powerUps = new List<PowerUpsAvailable>();
    public void AddPowerUp(PowerUpsAvailable powerup) {
        powerUps.Add(powerup);
    }

    public void RemovePowerUp(PowerUpsAvailable powerup) {
        powerUps.Remove(powerup);
    }

    public List<PowerUpsAvailable> GetPowerUps() {
        return powerUps;
    }
}
