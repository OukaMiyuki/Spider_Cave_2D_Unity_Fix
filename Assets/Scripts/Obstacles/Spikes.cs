using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerDeath.instance.KillThePlayer();
            RunAnimation.instance.RunningAnimation();
            Invoke("LoadGameOverPanel", 2.0f);
        }
    }

    private void LoadGameOverPanel() {
        GameplayController.instance.PlayerDied();
    }
}
