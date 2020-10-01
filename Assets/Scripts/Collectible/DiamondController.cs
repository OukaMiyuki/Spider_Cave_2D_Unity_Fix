using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour {
    void Start() {
        if (DoorScriptController.instance != null) {
            DoorScriptController.instance.collctiblesDiamondCount++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            if (PlayerDeath.instance.GetDieOrAlive()) {
                if (DoorScriptController.instance != null) {
                    Destroy(gameObject);
                    DoorScriptController.instance.DecrementCollectibleDiamonds();
                }
            }
        }
    }
}
