using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderBullet : MonoBehaviour {


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            PlayerDeath.instance.KillThePlayer();
            RunAnimation.instance.RunningAnimation();
            Invoke("LoadGameOverPanel", 1.0f);
            gameObject.SetActive(false);
            Invoke("DestroyObject", 2f);
        }

        if (collision.tag == "Ground") {
            gameObject.SetActive(false);
            Invoke("DestroyObject", 2f);
        }
    }

    private void LoadGameOverPanel() {
        GameplayController.instance.PlayerDied();
    }

    private void DestroyObject() {
        Destroy(gameObject);
    }
}
