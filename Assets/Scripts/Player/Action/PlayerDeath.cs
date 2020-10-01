using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour {
    public static PlayerDeath instance;

    private bool isPlayerAlive = true;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void KillThePlayer() {
        isPlayerAlive = false;
    }

    public bool GetDieOrAlive() {
        return isPlayerAlive;
    }
    
}
